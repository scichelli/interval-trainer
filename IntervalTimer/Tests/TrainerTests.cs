using IntervalTimer;

namespace Tests
{
    public class TrainerTests
    {
        private Trainer _trainer;

        public TrainerTests()
        {
            var notifier = new ConsoleNotifier();
            _trainer = new Trainer(new FakeSounder(notifier), notifier);
        }

        public void RunSequence()
        {
            _trainer.WarmUpDuration = 1;
            _trainer.RunDuration = 1;
            _trainer.WalkDuration = 2;
            _trainer.CoolDownDuration = 1;
            _trainer.IntervalsGoal = 3;

            _trainer.Run();
        }

    }

    internal class FakeSounder : Sounder
    {
        private Notifier _notifier;

        public FakeSounder(Notifier notifier)
        {
            _notifier = notifier;
        }

        public void Play()
        {
            _notifier.Info("Played a sound.");
        }
    }
}
