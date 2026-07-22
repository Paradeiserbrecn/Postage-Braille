using System;
using System.Collections.Generic;
using Data;

namespace Serialization
{
    [Serializable]
    public class LetterPackagesSaveData
    {
        public SupportedLanguage currentLanguage;
        public int currentUnit;

        public List<LetterUnitProgress> progress = new();
    }

    [Serializable]
    public class LetterUnitProgress
    {
        public int attempts;
        public int successes;
    }

    [Serializable]
    public class LetterUnitData
    {
        public List<string> letters = new();
        public List<string> words = new();
    }

    [Serializable]
    public class LetterPackageData
    {
        public SupportedLanguage language;
        public List<LetterUnitData> units = new();
    }
}
