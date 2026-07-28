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
        public string printCharacters;
        public List<bool> brailleCharacter;
        public int priority;
        public List<ConversionCondition> conversionConditions;
    }

    [Serializable]
    public class ConversionCondition
    {
        public bool direction;
        public GridBrailleConverter.ConditionType type;
    }
}
