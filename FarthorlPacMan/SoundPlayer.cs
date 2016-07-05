using System;
using System.Collections.Generic;
using System.Media;

namespace FarthorlPacMan
{
    public class SoundPlayer : IDisposable
    {
        private static System.Media.SoundPlayer soundPlayer { get; set; }
        private static Dictionary<string, string> Sounds = new Dictionary<string, string>
        {
            {"begining", "DataFiles/Sounds/pacman_beginning.wav" },
            {"eatfruit", "DataFiles/Sounds/pacman_eatfruit.wav" },
            {"pause", "DataFiles/Sounds/pause.wav" }
        };

        public static void Play(string sound)
        {
            if (!Sounds.ContainsKey(sound))
            {
                throw new KeyNotFoundException("The current sound is not added in the playlist");
            }
            soundPlayer = new System.Media.SoundPlayer(Sounds[sound]);
            soundPlayer.Play();
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
                if (soundPlayer != null)
                {
                    soundPlayer.Dispose();
                }
            }
        }
    }
}