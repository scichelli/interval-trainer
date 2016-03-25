using System.Media;
using System.Reflection;

namespace IntervalTimer
{
    public interface Sounder
    {
        void Play();
    }

    public class SystemSounder : Sounder
    {
        private SoundPlayer _player;

        public SystemSounder(string soundFile)
        {
            _player = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream(soundFile));
        }

        public void Play()
        {
            _player.Play();
        }
    }
}
