using System;

namespace IntervalTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainer = new Trainer(new SystemSounder("IntervalTimer.tada.wav"), new ConsoleNotifier())
            {
                WarmUpDuration = 5.Minutes(),
                RunDuration = 30.Seconds(),
                WalkDuration = 2.Minutes(),
                CoolDownDuration = 5.Minutes(),
                IntervalsGoal = 6
            };
            trainer.Run();
            Console.ReadLine();
        }
    }

    public static class IntervalExtensions
    {
        public static long Seconds(this int intervalInSeconds)
        {
            return intervalInSeconds * 1000;
        }

        public static long Minutes(this int intervalInMinutes)
        {
            return (intervalInMinutes * 60).Seconds();
        }
    }
}
