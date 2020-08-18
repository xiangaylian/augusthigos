using System;
using System.Collections.Generic;

namespace BuliderDesignPatterns
{
    /// <summary>
    /// <para>建造者模式，以造车为例子</para>
    /// <para>主要解决在软件系统中，有时候面临着"一个复杂对象"的创建工作，其通常由各个部分的子对象用一定的算法构成；由于需求的变化，这个复杂对象的各个部分经常面临着剧烈的变化，但是将它们组合在一起的算法却相对稳定。</para>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 1.定义建造者和领导者对象
            // 1.1.领导者对象
            Director director = new Director();
            // 1.2.建造者对象
            AbstractBuilder bydCarBuilder = new BuilderBYD();
            AbstractBuilder fordCarBuilder = new BuilderFord();

            // 2.领导指挥建造者进行建造
            // 2.1.建造比亚迪
            director.Construct(bydCarBuilder);
            // 2.2.建造福特
            director.Construct(fordCarBuilder);

            // 3.建造者将建好的成品进行展示
            // 3.1.展示比亚迪轿车
            Car bydCar = bydCarBuilder.GetCar();
            bydCar.Show();

            // 3.2.展示福特轿车
            Car fordCar = fordCarBuilder.GetCar();
            fordCar.Show();

            Console.Read();
        }

        /// <summary>
        /// 这个类型才是组装的，Construct方法里面的实现就是创建复杂对象固定算法的实现，该算法是固定的，或者说是相对稳定的
        /// 这个人当然就是老板了，也就是建造者模式中的指挥者
        /// </summary>
        public class Director
        {
            // 组装汽车
            public void Construct(AbstractBuilder builder)
            {
                builder.BuildCarDoor();
                builder.BuildCarWheel();
                builder.BuildCarEngine();
            }
        }

        public sealed class Car
        {
            // 汽车部件集合
            private IList<string> parts = new List<string>();

            // 把单个部件添加到汽车部件集合中
            public void Add(string part)
            {
                parts.Add(part);
            }

            public void Show()
            {
                Console.WriteLine("汽车开始在组装.......");
                foreach (string part in parts)
                {
                    Console.WriteLine("组件" + part + "已装好");
                }

                Console.WriteLine("汽车组装好了");
            }
        }

        /// <summary>
        /// 比亚迪建造者
        /// </summary>
        public sealed class BuilderBYD : AbstractBuilder
        {
            Car _car = new Car();

            public override void BuildCarWheel()
            {
                _car.Add("BYD's Engine !");
            }

            public override void BuildCarEngine()
            {
                _car.Add("BYD's Wheels !");
            }

            public override void BuildCarDoor()
            {
                _car.Add("BYD's Door !");
            }

            public override Car GetCar()
            {
                return _car;
            }
        }

        /// <summary>
        /// 福特轿车建造者
        /// </summary>
        public sealed class BuilderFord : AbstractBuilder
        {
            Car _car = new Car();
            public override void BuildCarWheel()
            {
                _car.Add("Ford's Engine !");
            }

            public override void BuildCarEngine()
            {
                _car.Add("Ford's Wheels !");
            }

            public override void BuildCarDoor()
            {
                _car.Add("Ford's Door !");
            }

            public override Car GetCar()
            {
                return _car;
            }
        }

        /// <summary>
        /// 建造者方法
        /// </summary>
        public interface IAbstract
        {
            void BuildCarWheel();
            void BuildCarEngine();
            void BuildCarDoor();
            Car GetCar();
        }

        /// <summary>
        /// 抽象的建造工人
        /// </summary>
        public abstract class AbstractBuilder : IAbstract
        {
            /// <summary>
            /// 造引擎
            /// </summary>
            public abstract void BuildCarWheel();

            /// <summary>
            /// 造轮子
            /// </summary>
            public abstract void BuildCarEngine();

            /// <summary>
            /// 造车门
            /// </summary>
            public abstract void BuildCarDoor();

            /// <summary>
            /// 获取建造的汽车
            /// </summary>
            /// <returns></returns>
            public abstract Car GetCar();
        }
    }
}
