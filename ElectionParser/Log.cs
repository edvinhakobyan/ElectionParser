using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElectionParser
{
    public class Log : IDisposable
    {
        private readonly StreamWriter _streamWriter;

        public Log(string fileName)
        {
            _streamWriter = new StreamWriter(fileName, append: true);
        }

        public void Write(string text)
        {
            _streamWriter.Write(text);
        }

        public void WriteLine(string text)
        {
            Write(text + Environment.NewLine);
        }

        public void WriteWithDateTime(string text)
        {
            _streamWriter.Write($"{DateTime.Now} -> {text}");
        }

        public void WriteLineWithDateTime(string text)
        {
            WriteWithDateTime(text + Environment.NewLine);
        }

        public void Dispose()
        {
            if (_streamWriter != null)
                _streamWriter.Close();
        }
    }
}
