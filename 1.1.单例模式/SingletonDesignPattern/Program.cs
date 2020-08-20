#undef UnSafe_Singleton
#define Safe_Singleton
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonDesignPattern
{
    class Program
    {
        const string _line = "------------------------------";
        const string _starLine = "*****>>>>>>>";
        static void Main(string[] args)
        {
            // 模拟一种线程不安全的单例模式
            CreateLayzingDone();
            Console.WriteLine();

            // 饿汉式单例模式
            var hungryInstance = HungrySingleton.GetInstance();
            Console.WriteLine(hungryInstance.About());
            Console.WriteLine(_line);
            Console.WriteLine();

            // 登记式单例模式
            CreateRegisterDone();
            Console.WriteLine();

            Console.Read();
        }

        static void CreateLayzingDone()
        {
#if UnSafe_Singleton
            // 模拟一种线程不安全的单例模式
            UnsafeTest();
#endif

#if Safe_Singleton
            // 模拟线程安全的单例模式的创建
            SafeTest();
#endif
        }

        static void CreateRegisterDone()
        {
#if UnSafe_Singleton
            // 模拟一种线程不安全的单例模式
            UnsafeRegisterTest();
#endif

#if Safe_Singleton
            // 模拟线程安全的单例模式的创建
            SafeRegisterTest();
#endif
        }

        /// <summary>
        /// 不安全线程的实例创建
        /// </summary>
        static void UnsafeTest()
        {
            var startTime = DateTime.Now;
            Console.WriteLine($"开始不安全懒汉式单例对象的创建：{startTime:yyyy-MM-dd HH:mm:ss.fff}");
            
            LayzingSingleton instance1 = null;
            LayzingSingleton instance2 = null;
            var task1 = Task.Run(() =>
            {
                instance1 = LayzingSingleton.GetUnsafeInstance();
                return instance1.GetHashCode();
            });
            var task2 = Task.Run(() =>
            {
                instance2 = LayzingSingleton.GetUnsafeInstance();
                return instance2.GetHashCode();
            });

            var hashCode1 = task1.Result;
            var hashCode2 = task2.Result;

            var endTime = DateTime.Now;
            Console.WriteLine($"结束：{endTime:yyyy-MM-dd HH:mm:ss.fff}");
            Console.WriteLine($"共计耗时：{(endTime - startTime).TotalMilliseconds}毫秒");
            Console.WriteLine(_starLine);
            Console.WriteLine($"task1创建的对象的HashCode是：{hashCode1}");
            Console.WriteLine($"task2创建的对象的HashCode是：{hashCode2}");
            Console.WriteLine($"两个任务获取的对象是{(hashCode1 == hashCode2 ? "同一个对象" : "不同对象")}");
            Console.WriteLine(_line);
        }

        /// <summary>
        /// 安全线程的实例创建
        /// </summary>
        static void SafeTest()
        {
            var startTime = DateTime.Now;
            Console.WriteLine($"开始线程安全懒汉式单例对象的创建：{startTime:yyyy-MM-dd HH:mm:ss.fff}");

            LayzingSingleton instance1 = null;
            LayzingSingleton instance2 = null;
            var task1 = Task.Run(() =>
            {
                instance1 = LayzingSingleton.GetsafeInstance();
                return instance1.GetHashCode();
            });
            var task2 = Task.Run(() =>
            {
                instance2 = LayzingSingleton.GetsafeInstance();
                return instance2.GetHashCode();
            });

            var hashCode1 = task1.Result;
            var hashCode2 = task2.Result;

            var endTime = DateTime.Now;
            Console.WriteLine($"结束：{endTime:yyyy-MM-dd HH:mm:ss.fff}");
            Console.WriteLine($"共计耗时：{(endTime - startTime).TotalMilliseconds}毫秒");
            Console.WriteLine(_starLine);
            Console.WriteLine($"task1创建的对象的HashCode是：{hashCode1}");
            Console.WriteLine($"task2创建的对象的HashCode是：{hashCode2}");
            Console.WriteLine($"两个任务获取的对象是{(hashCode1 == hashCode2 ? "同一个对象" : "不同对象")}");
            Console.WriteLine(_line);
        }

        static void UnsafeRegisterTest()
        {
            var startTime = DateTime.Now;
            Console.WriteLine($"开始登记式单例对象的创建：{startTime:yyyy-MM-dd HH:mm:ss.fff}");

            ChineseRegister instance1 = null;
            ChineseRegister instance2 = null;
            var task1 = Task.Run(() =>
            {
                instance1 = ChineseRegister.GetUnsafeInstance();
                return instance1.GetHashCode();
            });
            var task2 = Task.Run(() =>
            {
                instance2 = ChineseRegister.GetUnsafeInstance();
                return instance2.GetHashCode();
            });

            var hashCode1 = task1.Result;
            var hashCode2 = task2.Result;

            var endTime = DateTime.Now;
            Console.WriteLine($"结束：{endTime:yyyy-MM-dd HH:mm:ss.fff}");
            Console.WriteLine($"共计耗时：{(endTime - startTime).TotalMilliseconds}毫秒");
            Console.WriteLine(_starLine);
            Console.WriteLine($"task1创建的对象的HashCode是：{hashCode1}");
            Console.WriteLine($"task2创建的对象的HashCode是：{hashCode2}");
            Console.WriteLine($"两个任务获取的对象是{(hashCode1 == hashCode2 ? "同一个对象" : "不同对象")}");
            Console.WriteLine(_line);
        }

        static void SafeRegisterTest()
        {
            var startTime = DateTime.Now;
            Console.WriteLine($"开始登记式单例对象的创建：{startTime:yyyy-MM-dd HH:mm:ss.fff}");

            ChineseRegister instance1 = null;
            ChineseRegister instance2 = null;
            var task1 = Task.Run(() =>
            {
                instance1 = ChineseRegister.GetSafeInstance();
                return instance1.GetHashCode();
            });
            var task2 = Task.Run(() =>
            {
                instance2 = ChineseRegister.GetSafeInstance();
                return instance2.GetHashCode();
            });

            var hashCode1 = task1.Result;
            var hashCode2 = task2.Result;

            var endTime = DateTime.Now;
            Console.WriteLine($"结束：{endTime:yyyy-MM-dd HH:mm:ss.fff}");
            Console.WriteLine($"共计耗时：{(endTime - startTime).TotalMilliseconds}毫秒");
            Console.WriteLine(_starLine);
            Console.WriteLine($"task1创建的对象的HashCode是：{hashCode1}");
            Console.WriteLine($"task2创建的对象的HashCode是：{hashCode2}");
            Console.WriteLine($"两个任务获取的对象是{(hashCode1 == hashCode2 ? "同一个对象" : "不同对象")}");
            Console.WriteLine(_line);
        }
    }

    public interface ISingletonObject
    {
        string About();
    }

    /// <summary>
    /// 懒汉式单例模式：单例实例在第一次被使用时构建，延迟初始化。
    /// </summary>
    public class LayzingSingleton : ISingletonObject
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private LayzingSingleton() 
        {
            Thread.Sleep(3000); // 模拟对象的创建时间需要3秒
        }

        private static LayzingSingleton _unsafeInstance = null;
        /// <summary>
        /// 模拟线程不安全的实例创建
        /// </summary>
        /// <returns></returns>
        public static LayzingSingleton GetUnsafeInstance()
        {
            if(_unsafeInstance == null)
            {
                _unsafeInstance = new LayzingSingleton();
            }

            return _unsafeInstance;
        }

        private static readonly object _lockObj = new object();
        private static LayzingSingleton _safeInstance = null;
        /// <summary>
        /// 双检锁：模拟安全的实例创建
        /// </summary>
        /// <returns></returns>
        public static LayzingSingleton GetsafeInstance()
        {
            if (_safeInstance == null)
            {
                lock(_lockObj)
                {
                    if(_safeInstance == null)
                    {
                        _safeInstance = new LayzingSingleton();
                    }
                }
            }

            return _safeInstance;
        }

        public string About()
        {
            return $"我是懒汉式单例模式，我的HashCode是{GetHashCode()}";
        }
    }

    /// <summary>
    /// 饿汉式单例模式：单例实例在类装载时就构建，急切初始化。
    /// </summary>
    public class HungrySingleton : ISingletonObject
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private HungrySingleton() { }

        private static HungrySingleton _instance = new HungrySingleton();

        public static HungrySingleton GetInstance()
        {
            return _instance;
        }

        public string About()
        {
            return $"我是饿汉式单例模式，我的HashCode是{GetHashCode()}";
        }
    }

    /// <summary>
    /// <para>登记式单例模式</para>
    /// <para>维护一组单例类的实例，将这些实例存放在一个Map（登记簿）中，</para>
    /// <para>对于已经登记过的实例，则直接返回，对于没有登记的，则先登记，而后返回。</para>
    /// </summary>
    public abstract class RegisterSingleton<T> : ISingletonObject
        where T : RegisterSingleton<T>
    {
        /// <summary>
        /// 登记簿，用来存放所有已登记的实例
        /// </summary>
        private static Hashtable _regs = new Hashtable();

        /// <summary>
        /// 静态工厂方法，返回指定登记对象唯一实例
        /// </summary>
        /// <param name="regKey"></param>
        /// <returns></returns>
        public static T GetUnsafeInstance()
        {
            string regKey = typeof(T).Name;
            if (!_regs.ContainsKey(regKey))
            {
                // 如果T的构造函数受保护，使用Activator.CreateInstance<T>无法访问构造函数
                // 如果T的构造函数公开的话，又违反了单例模式只能被自己创建一次的约束
                // 可能是基于上述两点，百度上很难搜到C# 使用登记式单例模式的例子
                _regs.Add(regKey, Activator.CreateInstance(typeof(T), true));
            }

            return _regs[regKey] as T;
        }

        private static readonly object _lockObj = new object();
        public static T GetSafeInstance()
        {
            string regKey = typeof(T).Name;
            if (!_regs.ContainsKey(regKey))
            {
                // 如果T的构造函数受保护，使用Activator.CreateInstance<T>无法访问构造函数
                // 如果T的构造函数公开的话，又违反了单例模式只能被自己创建一次的约束
                // 可能是基于上述两点，百度上很难搜到C# 使用登记式单例模式的例子
                lock(_lockObj)
                {
                    if (!_regs.ContainsKey(regKey))
                    {
                        _regs.Add(regKey, Activator.CreateInstance(typeof(T), true));
                    }   
                }
            }

            return _regs[regKey] as T;
        }

        public abstract string About();
    }

    public class ChineseRegister : RegisterSingleton<ChineseRegister>
    {
        /// <summary>
        /// <para>C# 这里的构造函数无法被设置为protected或者是private</para>
        /// <para>所以没法控制别人不去通过构造函数创建全局唯一实例</para>
        /// </summary>
        private ChineseRegister()
        {
            Thread.Sleep(3000); // 模拟3秒才成功创建对象
        }

        public override string About()
        {
            return $"我是在中国登记的唯一实例，我的HashCode是{GetHashCode()}";
        }
    }

    public class AmericanRegister : RegisterSingleton<AmericanRegister>
    {
        /// <summary>
        /// <para>C# 这里的构造函数无法被设置为protected或者是private</para>
        /// <para>所以没法控制别人不去通过构造函数创建全局唯一实例</para>
        /// </summary>
        private AmericanRegister() { }

        public override string About()
        {
            return $"我是在美国登记的唯一实例，我的HashCode是{GetHashCode()}";
        }
    }
}

