using System;
using System.Media;
using System.Reflection;

namespace IntervalTimer
{
    class Program
    {
        static SoundPlayer _player;

        static void Main(string[] args)
        {
            InitializePlayer();
            _player.Play();
            Console.ReadLine();
        }

        private static void InitializePlayer()
        {
            _player = new SoundPlayer(Assembly.GetExecutingAssembly().GetManifestResourceStream("IntervalTimer.tada.wav"));
        }
    }
}
