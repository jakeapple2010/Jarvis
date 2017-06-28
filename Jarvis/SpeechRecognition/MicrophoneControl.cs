using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Jarvis.SpeechRecognition
{
    public class MicrophoneControl : IMicrophoneControl
    {
        public static WaveInEvent WaveSource;
        public static WaveFileWriter WaveFile;
        private static string _waveFilePath;

        public string Record(string message)
        {
            StartRecording();
            Console.WriteLine(message + " Press enter to stop.");

            Console.ReadLine();
            StopRecording();

            return _waveFilePath;
        }

        void StartRecording()
        {
            WaveSource = new WaveInEvent { WaveFormat = new WaveFormat(44100, 1) };

            WaveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            WaveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            _waveFilePath = $@"{Directory.GetCurrentDirectory()}\Test0001.wav";

            if (!File.Exists(_waveFilePath))
            {
                File.Create(_waveFilePath).Close();
            }
            WaveFile = new WaveFileWriter(_waveFilePath, WaveSource.WaveFormat);

            WaveSource.StartRecording();
        }

        void StopRecording()
        {
            WaveSource.StopRecording();
            WaveFile.Close();
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (WaveFile != null)
            {
                WaveFile.Write(e.Buffer, 0, e.BytesRecorded);
                WaveFile.Flush();
            }
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (WaveSource != null)
            {
                WaveSource.Dispose();
                WaveSource = null;
            }

            if (WaveFile != null)
            {
                WaveFile.Dispose();
                WaveFile = null;
            }
        }
    }
}
