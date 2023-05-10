using System.Threading;


Thread.CurrentThread.Name = "Main thread";

var thread = new Thread(ThreadMethod);
thread.Name = "Other thread";
thread.IsBackground = true;
thread.Priority = ThreadPriority.Normal;

thread.Start(42);

var count = 5;
var msg = "Hello, word";
var timeout = 150;

new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();


CheckThread();
for(var i=0; i < 100; i++)
{
    Thread.Sleep(100);
    Console.WriteLine(i);
}
    
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
    for(var i = 0; i < 100; i++)
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