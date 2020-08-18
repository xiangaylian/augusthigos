using System;

namespace AdapterDesignPattern
{
    /// <summary>
    /// <para>适配器模式</para>
    /// <para>单词：</para>
    /// <para>Target - 目标（需要接入两孔插座）</para>
    /// <para>Adapter - 适配器（三孔到两孔的插座转换器）</para>
    /// <para>Adaptee - 适配者（三孔插座）</para>
    /// <para>Client - 客户端对象</para>
    /// <para>Socket - 插座</para>
    /// <para>Plug - 插头</para>
    /// </summary>
    class Program
    {
        /// <summary>
        /// 只有一个三孔插座，如何让手机也能充电呢？
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 1.空调可以直接接入三孔插座，开始工作
            AirConditioner airConditioner = new AirConditioner();
            IThreeHoleSocketTarget threeHoleSocket = new ThreeHoleSocketAdaptee();
            airConditioner.Working(threeHoleSocket);

            // 2.手机通过两孔适配器，也可以接入三孔插座进行充电
            Phone phone1 = new Phone("IPhone X");
            Phone phone2 = new Phone("华为Mete30");
            // 2.1.手机IPhone X接入对象适配器(Adapter对象定义在适配器对象里面)开始充电
            ITwoHoleSocketTarget twoHoleObjectSocket = new ThreeToTwoObjectAdapter();
            phone1.Charging(twoHoleObjectSocket);
            // 2.2.手机华为Mete30接入类适配器(类适配器直接继承于Adapter对象)开始充电
            ITwoHoleSocketTarget twoHoleClassSocket = new ThreeToTwoClassAdapter();
            phone2.Charging(twoHoleClassSocket);

            Console.ReadLine();
        }
    }

    #region Client object
    /// <summary>
    /// 手机
    /// </summary>
    public class Phone
    {
        public Phone(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        /// <summary>
        /// 充电
        /// </summary>
        public void Charging(ITwoHoleSocketTarget socket)
        {
            Console.WriteLine($"手机{Name}接入两孔插座正在充电中...");
        }
    }

    /// <summary>
    /// 空调
    /// </summary>
    public class AirConditioner
    {
        /// <summary>
        /// 工作
        /// </summary>
        /// <param name="socket"></param>
        public void Working(IThreeHoleSocketTarget socket)
        {
            Console.WriteLine("空调接入三孔插座正在工作中...");
        }
    }
    #endregion

    #region Target、Adaptee、Adapter
    /// <summary>
    /// 两孔插座接口
    /// </summary>
    public interface ITwoHoleSocketTarget
    {
        void Request();
    }

    /// <summary>
    /// 三孔插座接口
    /// </summary>
    public interface IThreeHoleSocketTarget
    {
        void SpecificRequest();
    }

    /// <summary>
    /// 2孔插座
    /// </summary>
    public class TwoHoleSocketTarget : ITwoHoleSocketTarget
    {
        // 客户端需要的方法
        public virtual void Request()
        {
            Console.WriteLine("两孔插座已准备好");
        }
    }

    /// <summary>
    /// 3孔插座
    /// </summary>
    public class ThreeHoleSocketAdaptee : IThreeHoleSocketTarget
    {
        public void SpecificRequest()
        {
            Console.WriteLine("三孔插座已准备好");
        }
    }

    /// <summary>
    /// 三孔转两孔的适配器类
    /// </summary>
    public class ThreeToTwoObjectAdapter : TwoHoleSocketTarget
    {
        // 引用两个孔插头的实例,从而将客户端与TwoHole联系起来
        private ThreeHoleSocketAdaptee threeHoleAdaptee = new ThreeHoleSocketAdaptee();
        //这里可以继续增加适配的对象。。

        /// <summary>
        /// 实现2个孔插头接口方法
        /// </summary>
        public override void Request()
        {
            //可以做具体的转换工作
            threeHoleAdaptee.SpecificRequest();
            //可以做具体的转换工作
        }
    }

    /// <summary>
    /// 类适配器
    /// </summary>
    public class ThreeToTwoClassAdapter : ThreeHoleSocketAdaptee, ITwoHoleSocketTarget
    {
        /// <summary>
        /// 实现2个孔插头接口方法
        /// </summary>
        public void Request()
        {
            // 调用3个孔插头方法
            SpecificRequest();
        }
    }

    #endregion
}
