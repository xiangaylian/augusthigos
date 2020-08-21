using System;

namespace FactoryDesignPattern
{
    class Program
    {
        const string _line = "----------------{0}---------------";
        static void Main(string[] args)
        {
            // 简单工厂模式
            Console.WriteLine(string.Format(_line, "简单工厂模式 ↓↓↓"));
            SimpleFactoryTest();

            // 工厂方法模式
            Console.WriteLine();
            Console.WriteLine(string.Format(_line, "工厂方法模式 ↓↓↓"));
            MethodFactoryTest();

            // 抽象工厂模式
            Console.WriteLine();
            Console.WriteLine(string.Format(_line, "抽象工厂模式 ↓↓↓"));
            AbstractFactoryTest();

            Console.Read();
        }

        /// <summary>
        /// 简单工厂模式测试
        /// </summary>
        static void SimpleFactoryTest()
        {
            var paperPlane = SimplePaperFoldingFactory.CreateInstance(OrigamiProducts.PaperPlane);
            paperPlane.Flexagon();
            paperPlane.Playing();

            Console.WriteLine();

            var paperCrocodile = SimplePaperFoldingFactory.CreateInstance(OrigamiProducts.PaperCrocodile);
            paperCrocodile.Flexagon();
            paperCrocodile.Playing();
        }

        /// <summary>
        /// 工厂方法模式测试
        /// </summary>
        static void MethodFactoryTest()
        {
            var toyFactory = new ToyFactory();
            var toyProduct = toyFactory.CreateInstance();
            toyProduct.Show();

            Console.WriteLine();

            var clothFactory = new ClothFactory();
            var clothProduct = clothFactory.CreateInstance();
            clothProduct.Show();
        }

        /// <summary>
        /// 抽象工厂模式测试
        /// </summary>
        static void AbstractFactoryTest()
        {
            IFactory factory = new HaierFactory();
            var tv = factory.ProduceTelevision();
            tv.Play();
            var ac = factory.ProduceAirConditioner();
            ac.ChangeTemperature();
        }
    }

    #region 简单工厂模式（模拟折纸游戏）
    /// <summary>
    /// 简单工厂模式（模拟折纸游戏）
    /// </summary>
    public class SimplePaperFoldingFactory
    {
        public static IPaperFolding CreateInstance(OrigamiProducts origamiProduct)
        {
            IPaperFolding instance;
            switch (origamiProduct)
            {
                case OrigamiProducts.PaperCrocodile:
                    instance = new PaperCrocodile();
                    break;
                case OrigamiProducts.PaperClothes:
                    instance = new PaperClothes();
                    break;
                default:
                    instance = new PaperPlane();
                    break;

            }


            return instance;
        }
    }

    /// <summary>
    /// 枚举：折纸的制品
    /// </summary>
    public enum OrigamiProducts
    {
        PaperPlane = 0,
        PaperCrocodile = 1,
        PaperClothes = 2
    }

    public interface IPaperFolding
    {
        void Flexagon();
        void Playing();
    }

    /// <summary>
    /// 纸飞机
    /// </summary>
    public class PaperPlane : IPaperFolding
    {
        public void Flexagon()
        {
            Console.WriteLine("纸飞机：对折 -> 翻角 => 翻边");
        }

        public void Playing()
        {
            Console.WriteLine("玩纸飞机：拎起飞机扔出去");
        }
    }

    /// <summary>
    /// 纸鳄鱼
    /// </summary>
    public class PaperCrocodile : IPaperFolding
    {
        public void Flexagon()
        {
            Console.WriteLine("纸鳄鱼：对折 -> 翻角 => 画牙齿 => 贴浇水");
        }

        public void Playing()
        {
            Console.WriteLine("玩纸鳄鱼：用手指套住，模仿鳄鱼张大嘴巴，吓唬邻居家小孩");
        }
    }

    /// <summary>
    /// 折纸的衣衫
    /// </summary>
    public class PaperClothes : IPaperFolding
    {
        public void Flexagon()
        {
            Console.WriteLine("纸衣服：对折 -> 翻角 => 斜翻");
        }

        public void Playing()
        {
            Console.WriteLine("纸衣服：画上不同的颜色，摆放在桌面，可以作装饰品");
        }
    }
    #endregion

    #region 工厂方法模式（模拟生产产品）

    /// <summary>
    /// 抽象工厂
    /// </summary>
    public abstract class AbstractMethodFactory
    {
        public abstract AbstractProduct CreateInstance();
    }

    /// <summary>
    /// 玩具厂
    /// </summary>
    public class ToyFactory : AbstractMethodFactory
    {
        public override AbstractProduct CreateInstance()
        {
            return new ToyOfMinions();
        }
    }

    /// <summary>
    /// 布料厂
    /// </summary>
    public class ClothFactory : AbstractMethodFactory
    {
        public override AbstractProduct CreateInstance()
        {
            return new ClothOfPolyester();
        }
    }

    /// <summary>
    /// 抽象产品类
    /// </summary>
    public abstract class AbstractProduct
    {
        public abstract void Show();
    }

    /// <summary>
    /// 小黄人玩具
    /// </summary>
    public class ToyOfMinions : AbstractProduct
    {
        public override void Show()
        {
            Console.WriteLine("我是小黄人毛绒玩具.重0.5KG,长25CM,宽10CM.");
        }
    }

    /// <summary>
    /// 尼龙布料
    /// </summary>
    public class ClothOfNylon : AbstractProduct
    {
        public override void Show()
        {
            Console.WriteLine("我是尼龙布料.45%尼龙,15%棉.");
        }
    }

    /// <summary>
    /// 涤纶布料
    /// </summary>
    public class ClothOfPolyester : AbstractProduct
    {
        public override void Show()
        {
            Console.WriteLine("我是涤纶布料.35%涤纶,5%棉,25%稻草.");
        }
    }
    #endregion

    #region 抽象工厂模式（模拟不同品牌电视）
    /// <summary>
    /// 电视机接口
    /// </summary>
    public interface ITelevision
    {
        void Play();
    }

    /// <summary>
    /// 海尔电视
    /// </summary>
    public class HaierTelevision : ITelevision
    {
        public void Play()
        {
            Console.WriteLine("海尔电视机播放中···");
        }
    }

    /// <summary>
    /// TCL电视
    /// </summary>
    public class TCLTelevision : ITelevision
    {
        public void Play()
        {
            Console.WriteLine("TCL电视机播放中···");
        }
    }

    /// <summary>
    /// 空调
    /// </summary>
    public interface IAirConditioner
    {
        void ChangeTemperature();
    }

    /// <summary>
    /// 海尔空调
    /// </summary>
    public class HaierAirConditioner : IAirConditioner
    {
        public void ChangeTemperature()
        {
            Console.WriteLine("海尔空调温度改变中···");
        }
    }

    /// <summary>
    /// TCL空调
    /// </summary>
    public class TCLAirConditioner : IAirConditioner
    {
        public void ChangeTemperature()
        {
            Console.WriteLine("TCL空调温度改变中···");
        }
    }

    /// <summary>
    /// 工厂类
    /// </summary>
    public interface IFactory
    {
        ITelevision ProduceTelevision();
        IAirConditioner ProduceAirConditioner();
    }

    public class HaierFactory : IFactory
    {
        public ITelevision ProduceTelevision()
        {
            return new HaierTelevision();
        }

        public IAirConditioner ProduceAirConditioner()
        {
            return new HaierAirConditioner();
        }
    }

    public class TCLFactory : IFactory
    {

        public ITelevision ProduceTelevision()
        {
            return new TCLTelevision();
        }

        public IAirConditioner ProduceAirConditioner()
        {

            return new TCLAirConditioner();
        }
    }
    #endregion
}
