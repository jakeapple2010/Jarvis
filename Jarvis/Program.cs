using System;
using System.IO;
using System.Net;
using Google.Cloud.Speech.V1;

namespace Jarvis
{
    class Program
    {
        static void Main(string[] args)
        {
            var speech = SpeechClient.Create();
            var response = speech.Recognize(new RecognitionConfig()
            {
                Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
                SampleRateHertz = 44100,
                LanguageCode = "en",
            }, RecognitionAudio.FromFile("good-morning-google.flac"));
            foreach (var result in response.Results)
            {
                foreach (var alternative in result.Alternatives)
                {
                    Console.WriteLine(alternative.Transcript);
                }
            }

            //try
            //{

            //    var response = GoogleRequest(fileBytes, 44100);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}

            Console.ReadLine();
        }

        public static string GoogleRequest(byte[] bytes, int sampleRate)
        {
            Stream stream = null;
            StreamReader sr = null;
            WebResponse response = null;
            string respFromServer;
            try
            {
                var request = WebRequest.Create("https://speech.googleapis.com/v1/speech:recognize?key=AIzaSyDnVTLpLnS1o-JaiQEIFoDeHE5QQu_uViM");
                request.Method = "POST";
                request.ContentType = "audio/x-flac; rate=" + sampleRate;
                request.ContentLength = bytes.Length;

                stream = request.GetRequestStream();

                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                response = request.GetResponse();

                stream = response.GetResponseStream();
                if (stream == null)
                {
                    throw new Exception("Can't get a response from server. Response stream is null.");
                }
                sr = new StreamReader(stream);

                //Get response in JSON format
                respFromServer = sr.ReadToEnd();
            }
            finally
            {
                stream?.Close();

                sr?.Close();

                response?.Close();
            }

            return respFromServer;
        }
    }
}
