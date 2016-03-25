using System;
using System.Diagnostics;
using System.Media;
using System.Reflection;

namespace IntervalTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            var trainer = new Trainer(InitializePlayer(), new ConsoleNotifier());
            trainer.Run();
            Console.ReadLine();
        }

        private static SoundPlayer InitializePlayer()
        {
            return new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("IntervalTimer.tada.wav"));
        }
    }

    public enum IntervalState
    {
        WarmingUp,
        Running,
        Walking,
        CoolingDown,
        Done
    }

    public class Trainer
    {
        private SoundPlayer _player;
        private Notifier _notifier;
        private Stopwatch _stopwatch;
        private IntervalState _state;
        private int _intervals;

        public Trainer(SoundPlayer player, Notifier notifier)
        {
            _player = player;
            _notifier = notifier;
            _stopwatch = new Stopwatch();
            _state = IntervalState.WarmingUp;
            _intervals = 0;
        }

        public void Run()
        {
            _stopwatch.Start();
            _notifier.Info("Start warming up");

            while (_state != IntervalState.Done)
            {
                switch (_state)
                {
                    case IntervalState.WarmingUp:
                        Warming();
                        break;
                    case IntervalState.Running:
                        Running();
                        break;
                    case IntervalState.Walking:
                        Walking();
                        break;
                    case IntervalState.CoolingDown:
                        Cooling();
                        break;
                }
            }
        }

        private void Warming()
        {
            if (_stopwatch.ElapsedMilliseconds == 5.Minutes())
            {
                TransitionToRunning();
            }
        }

        private void Running()
        {
            if (_stopwatch.ElapsedMilliseconds == 30.Seconds())
            {
                if (_intervals < 6)
                {
                    TransitionToWalking();
                }
                else
                {
                    TransitionToCooling();
                }
            }
        }

        private void Walking()
        {
            if (_stopwatch.ElapsedMilliseconds == 2.Minutes())
            {
                TransitionToRunning();
            }
        }

        private void Cooling()
        {
            if (_stopwatch.ElapsedMilliseconds == 5.Minutes())
            {
                End();
            }
        }

        private void TransitionToRunning()
        {
            _notifier.Info("Start running!");
            _player.Play();
            _stopwatch.Restart();
            _state = IntervalState.Running;
        }

        private void TransitionToWalking()
        {
            _intervals++;
            _notifier.Info("Walk.");
            _player.Play();
            _stopwatch.Restart();
            _state = IntervalState.Walking;
        }

        private void TransitionToCooling()
        {
            _notifier.Info("Cool down.");
            _player.Play();
            _stopwatch.Restart();
            _state = IntervalState.CoolingDown;
        }

        private void End()
        {
            _notifier.Info("Done!");
            _stopwatch.Reset();
            _state = IntervalState.Done;
        }
    }

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
