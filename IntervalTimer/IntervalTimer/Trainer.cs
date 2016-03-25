using System.Diagnostics;

namespace IntervalTimer
{
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
        private Sounder _player;
        private Notifier _notifier;
        private Stopwatch _stopwatch;
        private IntervalState _state;
        private int _intervals;

        public Trainer(Sounder player, Notifier notifier)
        {
            _player = player;
            _notifier = notifier;
            _stopwatch = new Stopwatch();
            _state = IntervalState.WarmingUp;
            _intervals = 0;
        }

        public long WarmUpDuration { get; set; }
        public long RunDuration { get; set; }
        public long WalkDuration { get; set; }
        public long CoolDownDuration { get; set; }
        public int IntervalsGoal { get; set; }

        public void Run()
        {
            _stopwatch.Start();
            _notifier.Info("Get going.");

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
            if (_stopwatch.ElapsedMilliseconds == WarmUpDuration)
            {
                TransitionToRunning();
            }
        }

        private void Running()
        {
            if (_stopwatch.ElapsedMilliseconds == RunDuration)
            {
                if (_intervals < IntervalsGoal)
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
            if (_stopwatch.ElapsedMilliseconds == WalkDuration)
            {
                TransitionToRunning();
            }
        }

        private void Cooling()
        {
            if (_stopwatch.ElapsedMilliseconds == CoolDownDuration)
            {
                End();
            }
        }

        private void TransitionToRunning()
        {
            _intervals++;
            _notifier.Info("Start running!");
            _player.Play();
            _stopwatch.Restart();
            _state = IntervalState.Running;
        }

        private void TransitionToWalking()
        {
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
}
