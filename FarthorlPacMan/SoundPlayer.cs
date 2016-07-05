﻿using System;
using System.Collections.Generic;
using System.Media;

namespace FarthorlPacMan
{
    public class PlayerSound : IDisposable
    {
        public SoundPlayer soundPlayer { get; private set; }
        public Dictionary<string, string> Sounds { get; }

        public PlayerSound()
        {
            this.soundPlayer = new SoundPlayer();
            this.Sounds = new Dictionary<string, string>
            {
                {"begining", "DataFiles/Sounds/pacman_beginning.wav" },
                {"eatfruit", "DataFiles/Sounds/pacman_eatfruit.wav" },
                {"pause", "DataFiles/Sounds/pause.wav" }
            };
        }

        public void Play(string sound)
        {
            if (!Sounds.ContainsKey(sound))
            {
                throw new KeyNotFoundException("The current sound is not added in the playlist");
            }
            soundPlayer = new SoundPlayer(Sounds[sound]);
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