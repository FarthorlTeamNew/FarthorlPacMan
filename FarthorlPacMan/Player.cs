
using System.Media;

namespace FarthorlPacMan
{
    using System.Collections.Generic;

    class Player
    {
        private SoundPlayer player=new SoundPlayer();
        Dictionary<string, string> sounds = new Dictionary<string, string>();

        public Player() {

            sounds.Add("begining", "DataFiles/Sounds/pacman_beginning.wav");
            sounds.Add("eatfruit", "DataFiles/Sounds/pacman_eatfruit.wav");
        }

        public void Play(string sound)
        {
            if (sounds.ContainsKey(sound))
            {
                player =new SoundPlayer(sounds[sound]);
                player.Play();
            }
        }
    }
}
