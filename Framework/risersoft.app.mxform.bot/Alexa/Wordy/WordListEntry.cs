using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CognitiveServiceRsMx.Speech.Wordy
{
    public class WordListEntry
    {
        public string Word { get; set; }
        public int Level { get; set; }

        protected internal bool Hasdata()
        {
            return (this.Word != null);
        }
    }
}