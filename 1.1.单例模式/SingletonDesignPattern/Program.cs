using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonDesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            UnsafeTest();

            Console.Read();
        }

        /// <summary>
        /// 不安全线程的实例创建
        /// </summary>
        static void UnsafeTest()
        {
            Console.WriteLine("开始：");
            var startTime = DateTime.Now;
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
        }
    }

    /// <summary>
    /// 懒汉式单例模式：单例实例在第一次被使用时构建，延迟初始化。
    /// </summary>
    public class LayzingSingleton
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
        /// 模拟安全的实例创建
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
    }

    /// <summary>
    /// 饿汉式单例模式：单例实例在类装载时就构建，急切初始化。
    /// </summary>
    public class HungrySingleton
    {
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private HungrySingleton() { }

        private static HungrySingleton _instance = new HungrySingleton();

        public HungrySingleton GetInstance()
        {
            return _instance;
        }
    }
}
