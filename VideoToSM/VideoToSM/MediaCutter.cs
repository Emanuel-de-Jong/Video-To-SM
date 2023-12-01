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
    public class MediaCutter
    {
        public void Cut(string path, string outName, DateTimeOffset startTime, DateTimeOffset endTime, bool isVideo)
        {
            string outPath = path.Substring(0, path.LastIndexOf('\\') + 1);
            outPath += outName + System.IO.Path.GetExtension(path);

            string command = $"ffmpeg " +
                $"-i \"{path}\" " +
                $"-ss {startTime.ToString("HH:mm:ss.ff")} " +
                $"-to {endTime.ToString("HH:mm:ss.ff")} " +
                (isVideo ? $"-c:v libx264 " : "") +
                $"-x264opts " +
                $"keyint=60:no-scenecut " +
                $"-c:a libvorbis " +
                $"-strict experimental " +
                (isVideo ? $"-b:v 2M " : "") +
                $"-b:a 192k " +
                $"-y " +
                $"\"{outPath}\"";

            G.MessageTextBoxHelper.WriteLine(command);

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
