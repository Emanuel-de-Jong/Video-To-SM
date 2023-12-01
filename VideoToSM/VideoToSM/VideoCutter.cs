using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public void Cut(string videoPath, string audioPath,
            DateTimeOffset videoStartTime, DateTimeOffset videoEndTime,
            DateTimeOffset audioStartTime, DateTimeOffset audioEndTime)
        {
            FindSongTitle(System.IO.Path.GetFileNameWithoutExtension(videoPath));

            string outName = "- " + G.SongTitle;
            ExecuteFfmpeg(videoPath, outName + " [video]", videoStartTime, videoEndTime);
            ExecuteFfmpeg(audioPath, outName, audioStartTime, audioEndTime);
        }

        private void FindSongTitle(string fileName)
        {
            fileName = fileName.Substring(13);
            fileName = fileName.Substring(0, fileName.IndexOf("(") - 1);
            G.SongTitle = fileName;
        }

        private void ExecuteFfmpeg(string path, string outName, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            string outPath = path.Substring(0, path.LastIndexOf('\\') + 1);
            outPath += outName + System.IO.Path.GetExtension(path);

            string command = $"ffmpeg " +
                $"-ss {startTime.ToString("HH:mm:ss.ff")} " +
                $"-i \"{path}\" " +
                $"-c copy " +
                $"-avoid_negative_ts make_zero " +
                $"-fflags +genpts " +
                $"-copyts " +
                $"-y " +
                $"-to {endTime.ToString("HH:mm:ss.ff")} " +
                $"\"{outPath}\"";

            ProcessStartInfo startInfo = new()
            {
                FileName = "cmd.exe",
                Arguments = "/c " + command, // /c flag to run the command and exit
                RedirectStandardOutput = true, // Redirect output for capturing
                UseShellExecute = false, // Ensure we can redirect output
                CreateNoWindow = true
            };

            Process process = new()
            {
                StartInfo = startInfo
            };

            process.Start();
            process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
    }
}
