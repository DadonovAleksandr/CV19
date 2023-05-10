using System.Threading;


Thread.CurrentThread.Name = "Main thread";

var thread = new Thread(ThreadMethod);
thread.Name = "Other thread";
thread.IsBackground = true;
thread.Priority = ThreadPriority.Normal;

thread.Start(42);


CheckThread();
for(var i=0; i < 100; i++)
{
    Thread.Sleep(100);
    Console.WriteLine(i);
}
    
Console.ReadLine();



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