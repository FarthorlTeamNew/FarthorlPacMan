namespace FarthorlPacMan
{
    using System;
    using System.Collections.Generic;
    using System.Media;

    class PlayerSound : IDisposable
    {
        private SoundPlayer player = new SoundPlayer();
        Dictionary<string, string> sounds = new Dictionary<string, string>();

        public PlayerSound()
        {

            sounds.Add("begining", "DataFiles/Sounds/pacman_beginning.wav");
            sounds.Add("eatfruit", "DataFiles/Sounds/pacman_eatfruit.wav");
        }

        public void Play(string sound)
        {
            if (sounds.ContainsKey(sound))
            {
                player = new SoundPlayer(sounds[sound]);
                player.Play();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (player != null)
                {
                    player.Dispose();
                }
            }
        }
    }
}