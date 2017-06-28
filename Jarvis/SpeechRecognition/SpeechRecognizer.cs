using System;
using System.IO;
using System.Linq;
using Google.Cloud.Speech.V1;

namespace Jarvis.SpeechRecognition
{
    public class SpeechRecognizer : ISpeechRecognizer
    {
        public string ConvertSpeechToText(string waveFilePath)
        {
            //var fileDetails = new FileInfo(waveFilePath);
            //if (fileDetails.Length > )

                var speech = SpeechClient.Create();
            var response = speech.Recognize(new RecognitionConfig
            {
                SampleRateHertz = 44100,
                LanguageCode = "en"
            }, RecognitionAudio.FromFile(waveFilePath));

            var speechRecognitionAlternative = response.Results.FirstOrDefault()?.Alternatives.OrderBy(x => x.Confidence).FirstOrDefault();
            return speechRecognitionAlternative != null ? speechRecognitionAlternative.Transcript : string.Empty;
        }
    }
}