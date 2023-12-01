using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace VideoToSM
{
    public class VideoCutter
    {
        public void Cut(string videoPath, string audioPath, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            FindSongTitle(System.IO.Path.GetFileNameWithoutExtension(videoPath));

            DateTimeOffset duration = startTime + (endTime - startTime);
        }

        private void FindSongTitle(string fileName)
        {
            fileName = fileName.Substring(13);
            fileName = fileName.Substring(0, fileName.IndexOf("(") - 1);
            G.SongTitle = fileName;
        }
    }
}
