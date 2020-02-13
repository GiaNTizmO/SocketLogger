using System.IO;

namespace ConsoleSocketServer
{
    public class FileLogSaver : ILogSaver
    {
        private StreamWriter streamWriter;

        public FileLogSaver(string filename)
        {
            var stream = File.OpenWrite(filename);

            streamWriter = new StreamWriter(stream);
            streamWriter.AutoFlush = true;
        }

        public void Save(string line)
        {
            streamWriter.WriteLine(line);
        }
    }
}
