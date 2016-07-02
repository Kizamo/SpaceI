using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class ThemeSong
    {
        private const int THEMESTARTRUTIME = 33638;
        private Thread themeThread;
        private bool start;
        private SoundPlayer themeStart;
        private SoundPlayer themeLoop;
        public ThemeSong()
        {
            themeStart = new SoundPlayer(Properties.Resources.themeStart);
            themeLoop = new SoundPlayer(Properties.Resources.themeLoop);
            start = true;
        }
        public void Play() //Creates a new thread and starts the theme song playback
        {
            themeThread = new Thread(PlaySound);
            themeThread.Start();
        }
        private void PlaySound() //plays the heme song.
        {
            if (start)
            {
                themeStart.Play();
                start = false;
                Thread.Sleep(THEMESTARTRUTIME);
            }
            themeLoop.PlayLooping();
        }
        public void Stop() //stops the theme song playback and aborts the thread
        {
            themeStart.Stop();
            themeLoop.Stop();
            themeThread.Abort();
        }
    }
}
