using System;
using System.Collections;
using System.Collections.Generic;

namespace Jarvis.SpeechRecognition
{
    public class VoiceRecognizedEvent : EventArgs
    {
        public IList<string> Transcripts { get; set; }
    }
}