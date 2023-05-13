using System.Threading;
using System.Collections.Concurrent;

bool __threadUpdate;

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

var values = new List<int>();

var threads = new Thread[10];
object lockObj = new object();

for (int i = 0; i < threads.Length; i++)
{
    threads[i] = new Thread(() =>
    {
        for (int j = 0; j < 10; j++)
        {
            lock (lockObj)
                values.Add(Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1);
        }
    });    
}

foreach (var thread in threads)
    thread.Start();

if(!clockThread.Join(200))
{
    //clockThread.Abort();        // Превывает поток в любой точке процесса
    clockThread.Interrupt();
}

Console.ReadLine();
Console.WriteLine(string.Join(",", values));
Console.ReadLine();

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
    while(__threadUpdate)
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