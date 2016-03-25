using System;

namespace IntervalTimer
{
    public interface Notifier
    {
        void Info(string message);
    }

    public class ConsoleNotifier : Notifier
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }
    }
}
