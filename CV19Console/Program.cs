using System.Threading;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

bool _threadUpdate = false;

Thread.CurrentThread.Name = "Main thread";

var clockThread = new Thread(ThreadMethod);
clockThread.Name = "Other thread";
clockThread.IsBackground = true;
clockThread.Priority = ThreadPriority.Normal;

clockThread.Start(42);

//var count = 5;
//var msg = "Hello, word";
//var timeout = 150;

//new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();


//CheckThread();

//for(var i=0; i < 100; i++)
//{
//    Thread.Sleep(100);
//    Console.WriteLine(i);
//}

//var values = new List<int>();

//var threads = new Thread[10];
//object lockObj = new object();

//for (int i = 0; i < threads.Length; i++)
//{
//    threads[i] = new Thread(() =>
//    {
//        for (int j = 0; j < 10; j++)
//        {
//            lock (lockObj)
//                values.Add(Thread.CurrentThread.ManagedThreadId);
//            Thread.Sleep(1);
//        }
//    });    
//}

//foreach (var thread in threads)
//    thread.Start();

//if(!clockThread.Join(200))
//{
//    //clockThread.Abort();        // Превывает поток в любой точке процесса
//    clockThread.Interrupt();
//}

//Mutex mutex = new Mutex();
//Semaphore semaphore = new Semaphore(0, 10);
//semaphore.WaitOne();
//// действия в крит. секции
//semaphore.Release();

ManualResetEvent manualResetEvent = new ManualResetEvent(false);
AutoResetEvent autoResetEvent = new AutoResetEvent(false);

EventWaitHandle threadguidance = manualResetEvent;

var threads = new Thread[10];
for (int i = 0; i < threads.Length; i++)
{
    var local_i = i;
    threads[i] = new Thread(() =>
    {
        Console.WriteLine($"Поток id: {Thread.CurrentThread.ManagedThreadId} стартовал");

        threadguidance.WaitOne();

        Console.WriteLine($"Value: {local_i}");
        Console.WriteLine($"Поток id: {Thread.CurrentThread.ManagedThreadId} - завершился");
    });
    threads[i].Start(); 
}
threadguidance.Set();


Console.ReadLine();
//Console.WriteLine(string.Join(",", values));
//Console.ReadLine();

[MethodImpl(MethodImplOptions.Synchronized)]
static void PrintMethod(string message, int count, int timeout)
{
    for(var i = 0; i < count; i++)
    {
        Console.WriteLine(message); 
        Thread.Sleep(timeout);
    }
}

void ThreadMethod(object? obj)
{
    var value = (int)obj;
    Console.WriteLine(value);

    CheckThread();
    while(_threadUpdate)
    {
        Thread.Sleep(100);
        Console.WriteLine(DateTime.Now);
    }
        
};

static void CheckThread()
{
    var thread = Thread.CurrentThread;
    Console.WriteLine($"{thread.ManagedThreadId} : {thread.Name}");
}