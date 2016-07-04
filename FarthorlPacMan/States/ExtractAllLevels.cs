using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarthorlPacMan.States
{
    public class ExtractAllLevels
    {
        private const string levelFolder = @"DataFiles\Levels";
        private List<string> levels;

        public ExtractAllLevels()
        {
            this.levels = new List<string>();
        }

        public List<string> ExctractLevels()
        {
            if (!Directory.Exists(levelFolder))
            {
                throw new DirectoryNotFoundException("Invalid path of level directory!");
            }
            DirectoryInfo directory = new DirectoryInfo(levelFolder);
            FileInfo[] files = directory.GetFiles("*.txt");
            foreach (FileInfo file in files)
            {
                this.levels.Add(levelFolder + @"\" + file.Name);
            }
            return this.levels;
        }
    }
}
