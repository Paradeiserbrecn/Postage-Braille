using System;
using System.Collections.Generic;
using Braille;

namespace Data
{
    [Serializable]
    public class BrailleLanguage
    {
        public List<BrailleConversion> brailleConversions;
    }

    [Serializable]
    public class BrailleConversion
    {
        public string printCharacter;
        public List<bool> brailleCharacter;
    }
}
