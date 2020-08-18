using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrototypeDesignPattern
{
    /// <summary>
    /// 原型模式
    /// </summary>
    class Program
    {
        static readonly Service _service = new Service();
        /// <summary>
        /// <para>原型模式（Prototype Pattern）</para>
        /// <para>是用于创建重复的对象，同时又能保证性能。</para>
        /// <para>这种类型的设计模式属于创建型模式，它提供了一种创建对象的最佳方式。</para>
        /// <para>这种模式是实现了一个原型接口，该接口用于创建当前对象的克隆。</para>
        /// <para>当直接创建对象的代价比较大时，则采用这种模式。</para>
        /// <para>例如，一个对象需要在一个高代价的数据库操作之后被创建。</para>
        /// <para>我们可以缓存该对象，在下一个请求时返回它的克隆，在需要的时候更新数据库，以此来减少数据库调用。</para>
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 1.顺序执行 + 浅层拷贝
            CarDesignDrawings carDesign = new CarDesignDrawings(1) { Value = ">-汽车图纸保存-<" };
            RunStopwatch(carDesign.Flag, "顺序执行", () =>
            {
                PlainSequenceDone(carDesign);
            });

            // 2.执行的时候线程不安全，对象被提前释放，值被其它进程篡改
            TrainDesignDrawings trainDesign = new TrainDesignDrawings(2) { Value = "(-火车图纸保存-)" };
            RunStopwatch(trainDesign.Flag, "不安全执行", () =>
            {
                UnsafeDone(trainDesign);
            });

            // 3.安全执行 + 深度复制（安全执行 + 快速反馈，就是原型模式希望解决的问题）
            AircraftDesignDrawings aircraftDesign = new AircraftDesignDrawings(3)
            {
                Value = "<-飞机图纸保存->",
                Commander = new AircraftDesignDrawings.AircraftCommander()
                {
                    Name = "A083机长-田蓝"
                }
            };
            RunStopwatch(aircraftDesign.Flag, "安全执行", () =>
            {
                SafeDone(aircraftDesign);
            });

            Console.Read();
        }

        /// <summary>
        /// <para>顺序处理方式</para>
        /// <para>特点：等待保存完了再进行打印，打印完成后再将对象回收</para>
        /// <para>缺点：打印输出慢，如果急需看图纸就没办法了</para>
        /// </summary>
        /// <param name="design"></param>
        static void PlainSequenceDone(DesignDrawingsPrototype design)
        {
            _service.SaveAsync(design).Wait();
            _service.PrintAsync(design).Wait();
            design.Dispose();
        }

        /// <summary>
        /// <para>安全的处理方式</para>
        /// <para>1.使用复制保证线程安全</para>
        /// <para>2.快速将结果反馈给图纸打印线程</para>
        /// </summary>
        /// <param name="design"></param>
        static void SafeDone(DesignDrawingsPrototype design)
        {
            var designCopy = design.Clone();
            _service.SaveAsync(design).ContinueWith((ts) => 
            {
                design.Dispose();
            });
            
            _service.PrintAsync(designCopy).ContinueWith((ts) => 
            {
                designCopy.Dispose();
            });
        }

        /// <summary>
        /// <para>不安全的处理方式</para>
        /// <para>打印时篡改了design.Value的值，不是按原来期望的进行保存的</para>
        /// </summary>
        /// <param name="design"></param>
        static void UnsafeDone(DesignDrawingsPrototype design)
        {
            _ = _service.SaveAsync(design);
            _service.PrintAsync(design).ContinueWith((ts) => 
            {
                design.Dispose();
            });
            
        }

        /// <summary>
        /// 监视器进行监视
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="title"></param>
        /// <param name="action"></param>
        static void RunStopwatch(string flag, string title, Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            action();

            sw.Stop();

            Console.WriteLine($"{flag}{title} 运行总共耗时:{sw.ElapsedMilliseconds}毫秒");
        }
    }

    public class Service
    {
        /// <summary>
        /// 模拟一个耗时的保存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> SaveAsync<T>(T entity)
            where T : DesignDrawingsPrototype
        {
            Console.WriteLine($"{entity.Flag}{entity.Name}SaveAsync 对象{entity.GetObjectInfo()}开始进入保存处理...");

            // 循环
            int i = 0;
            int limit = 7;
            while (i < limit)
            {
                i += 3;
                if (i > limit) i = limit;
                Console.WriteLine($"{entity.Flag}{entity.Name}SaveAsync 保存前处理进度{i}/{limit}.对象{entity.GetObjectInfo()}");
                await Task.Delay(3000);
            }

            return await Task.Run(() =>
            {
                // 模拟延时保存、保存失败的情形
                Console.WriteLine($"{entity.Flag}{entity.Name}SaveAsync 正在执行{entity.GetObjectInfo()}的保存...");
                Task.Delay(3000);
                Console.WriteLine($"{entity.Flag}{entity.Name}SaveAsync {entity.GetObjectInfo()}保存成功...");
                return entity;
            });
        }

        /// <summary>
        /// 模拟打印
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task PrintAsync<T>(T entity)
            where T : DesignDrawingsPrototype
        {
            Console.WriteLine($"{entity.Flag}{entity.Name}PrintAsync 开始进入打印处理，{entity.GetObjectInfo()}...");

            // 循环
            int i = 0;
            var limit = 3;
            while (i < limit)
            {
                i++;
                Console.WriteLine($"{entity.Flag}{entity.Name}PrintAsync 正在打印{i}/{limit}");
                await Task.Delay(1000);
            }

            entity.Value = "v.. 已经打印完成的汽车图纸 ..v";
        }
    }

    /// <summary>
    /// 原型基类（模拟一个设计图纸）
    /// </summary>
    public abstract class DesignDrawingsPrototype : IDisposable
    {
        public DesignDrawingsPrototype(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public object Value { get; set; }

        public abstract string Flag { get; }

        public abstract DesignDrawingsPrototype Clone();

        /// <summary>
        /// 这里不是真的释放，只是模拟对象被释放
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine($"{Flag}{Name}.Dispose，当前对象{GetObjectInfo()}已被释放");
            Id = 0;
            Name = null;
        }

        /// <summary>
        /// 获取当前对象信息
        /// </summary>
        /// <returns></returns>
        public virtual string GetObjectInfo()
        {
            return Id == 0 ? "{已被释放的对象}" : $"{{name:{Name}，id:{Id},value:{Value},hashcode:{GetHashCode()}}}";
        }
    }

    /// <summary>
    /// 汽车模型图
    /// </summary>
    public class CarDesignDrawings : DesignDrawingsPrototype
    {
        public CarDesignDrawings(long id)
            : base(id, "汽车模型图")
        {
        }

        public override string Flag => ">>> ";

        public override DesignDrawingsPrototype Clone()
        {
            return (DesignDrawingsPrototype)MemberwiseClone();
        }
    }

    /// <summary>
    /// 火车模型图
    /// </summary>
    public class TrainDesignDrawings : DesignDrawingsPrototype
    {
        public TrainDesignDrawings(long id)
            : base(id, "火车模型图")
        {
        }

        public override string Flag => "--》";

        public override DesignDrawingsPrototype Clone()
        {
            return (DesignDrawingsPrototype)MemberwiseClone();
        }
    }

    /// <summary>
    /// 飞机模型图
    /// </summary>
    public class AircraftDesignDrawings : DesignDrawingsPrototype
    {
        public AircraftDesignDrawings(long id)
            : base(id, "飞机模型图")
        {
        }

        public class AircraftCommander
        {
            public string Name { get; set; }
        }

        /// <summary>
        /// 机长对象
        /// </summary>
        public AircraftCommander Commander { get; set; }

        public override string Flag => "*********:)";

        /// <summary>
        /// 使用
        /// </summary>
        /// <returns></returns>
        public override DesignDrawingsPrototype Clone()
        {
            return JsonConvert.DeserializeObject<AircraftDesignDrawings>(JsonConvert.SerializeObject(this));
        }

        public override string GetObjectInfo()
        {
            return Id == 0 ? "<已被释放的对象>" : $"{{name:{Name}，id:{Id},hashcode:{GetHashCode()},value:{Value},commander.name:{Commander.Name}}}";
        }
    }
}
