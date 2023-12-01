using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VideoToSM
{
    public class VideoCutter
    {
        public void Cut(string path, DateTime startTime, DateTime endTime)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            FindSongTitle(fileName);
        }

        private void FindSongTitle(string fileName)
        {
            fileName = fileName.Substring(13);
            fileName = fileName.Substring(0, fileName.IndexOf("(") - 1);
            G.SongTitle = fileName;
        }
    }
}
