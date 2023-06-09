using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static Semaphore library = new Semaphore(10, 10);
    static void Main(string[] args)
    {
            Thread readersThread = new Thread(Reader);
            Thread writersThread = new Thread(Writer);
            readersThread.Start();
            writersThread.Start();
        
    }
    //count reader as 1, but writer as 10
    static void Reader()
    {
        Random randomTime = new Random();
        Random randomForLoop = new Random();
        Thread.Sleep(randomTime.Next(50, 200));
        for (int i=1; i <= randomForLoop.Next(15,60);i++) {
            Console.WriteLine("Reader " + i + " is waiting to get in");
            library.WaitOne();
            Console.WriteLine("Reader " + i + " has accessed the library!");
            Thread.Sleep(randomTime.Next(50,1000));
            Console.WriteLine("Reader " + i + " is about to leave");
            library.Release();
        }
    }
    static void Writer()
    {
        Random randomForLoopWriter = new Random();
        for (int i = 1; i <= randomForLoopWriter.Next(1, 10); i++)
        {
            Random randomTimeReader = new Random();
            Thread.Sleep(randomTimeReader.Next(500, 2000));
            Console.WriteLine("Writer " + i + " wants to write a book");
            for (int j = 0; j <= 9; j++)
            {
                library.WaitOne();
            }
            Console.WriteLine("Writer " + i + " started writing");
            Thread.Sleep(randomTimeReader.Next(2000,5000));
            Console.WriteLine("Writer " + i + " is about to leave the library");
            for (int k = 0; k <= 9; k++)
            {
                library.Release();
            }
        }
    }


}
