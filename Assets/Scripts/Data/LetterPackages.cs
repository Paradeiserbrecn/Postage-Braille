using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    public enum SupportedLanguage
    {
        De
    }

    [Serializable]
    public class LetterUnit
    {
        public List<string> Letters { get; set; } = new();
        public List<string> Words { get; set; } = new();
        public int attempts = 0;
        public int successes = 0;

        public double SuccessPercentage =>
            attempts == 0 ? 0 : Math.Round((double)successes / attempts * 100, 2);
    }

    [Serializable]
    public class LetterPackages : MonoBehaviour
    {
        [SerializeField] private int startingPackageProgress = 1;
        public int PackageProgress => LanguageProgress[currentLanguage];

        [FormerlySerializedAs("CurrentLanguagePackage")]
        public List<LetterUnit> currentLanguagePackage = GermanPackage;

        public LetterUnit CurrentPackageProgresses => currentLanguagePackage[LanguageProgress[currentLanguage]];

        public SupportedLanguage currentLanguage = SupportedLanguage.De;

        public static readonly Dictionary<SupportedLanguage, int> LanguageProgress = new();
        public static LetterPackages Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            LanguageProgress.Add(SupportedLanguage.De, startingPackageProgress);
        }


        public LetterUnit ProgressLetterPackage()
        {
            if (LanguageProgress[currentLanguage] < currentLanguagePackage.Count - 1)
            {
                LanguageProgress[currentLanguage]++;
            }

            return currentLanguagePackage[LanguageProgress[currentLanguage]];
        }

        /// <summary>
        /// Changes the internal CurrentPackage and all according variables
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="UnexpectedEnumValueException{SupportedLanguages}"></exception>
        public List<LetterUnit> ChangePackageLanguage(SupportedLanguage language)
        {
            switch (language)
            {
                case SupportedLanguage.De:
                    currentLanguagePackage = GermanPackage;
                    currentLanguage = SupportedLanguage.De;
                    break;
                default:
                    throw new UnexpectedEnumValueException<SupportedLanguage>(language);
            }

            return currentLanguagePackage;
        }

        private static readonly List<LetterUnit> GermanPackage = new()
        {
            {
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "E", "N", "I", "S", "T"
                    },

                    Words = new List<string>
                    {
                        "EINE",
                        "SEIN",
                        "NEIN",
                        "NEST",
                        "SINN",
                        "SEEN",
                        "TEES",
                        "TEST",
                        "INNE",
                        "SEIT",
                        "TIEN",
                        "SENE",
                        "NIET",
                        "TEIN",
                        "ENST"
                    }
                }
            },

            {
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "A", "R", "D", "H", "L"
                    },

                    Words = new List<string>
                    {
                        "AALD",
                        "ADER",
                        "AHNE",
                        "AHND",
                        "ALTE",
                        "ALER",
                        "ARTE",
                        "DAHL",
                        "DASE",
                        "DEIN",
                        "DIEN",
                        "HALT",
                        "HAND",
                        "HASE",
                        "HASE",
                        "HEIL",
                        "LAND",
                        "LAST",
                        "RATE",
                        "SEIT"
                    }
                }
            },

            {
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "M", "U", "O", "G", "B"
                    },

                    Words = new List<string>
                    {
                        "ABER",
                        "ADER",
                        "AHNE",
                        "AHNT",
                        "ALTE",
                        "ARME",
                        "ARTE",
                        "AUGE",
                        "BADE",
                        "BAHN",
                        "BARE",
                        "BAST",
                        "BAUM",
                        "BERG",
                        "BOTE",
                        "BUND",
                        "DAME",
                        "DANK",
                        "DASE",
                        "DEIN",
                        "DIEN",
                        "DORN",
                        "DRAN",
                        "GABE",
                        "GARN",
                        "GAST",
                        "GELD",
                        "GENAU",
                        "HALT",
                        "HAND",
                        "HAUS",
                        "HEIL",
                        "HUND",
                        "LAND",
                        "LAST",
                        "LESE",
                        "MANN",
                        "MAUS",
                        "MEER",
                        "MUTE"
                    }
                }
            },

            {
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "W", "F", "K", "Z", "P"
                    },

                    Words = new List<string>
                    {
                        "PARK",
                        "PAKT",
                        "POST",
                        "POSE",
                        "PFER",
                        "PFAD",
                        "PFEIL",
                        "PULS",
                        "PUST",
                        "PAAR",
                        "PEIN",
                        "PORE",
                        "PUNK",
                        "PINK",
                        "PFER",
                        "KALT",
                        "KARL",
                        "KERN",
                        "KIES",
                        "KIEL",
                        "KINO",
                        "KIND",
                        "KNIE",
                        "KORN",
                        "KOST",
                        "KURS",
                        "KUHE",
                        "KLEE",
                        "KLAR",
                        "KLAM",
                        "WAND",
                        "WARN",
                        "WARE",
                        "WARE",
                        "WEIN",
                        "WEST",
                        "WORT",
                        "WALD",
                        "WELT",
                        "WEGE",
                        "WIES",
                        "WIND",
                        "WIRT",
                        "WURM",
                        "WOHN",
                        "FALL",
                        "FAST",
                        "FEST",
                        "FERN",
                        "FELS",
                        "FEIN",
                        "FLUR",
                        "FORM",
                        "FRAG",
                        "FRAN",
                        "ZART",
                        "ZEIT",
                        "ZIEL",
                        "ZORN",
                        "ZONE"
                    }
                }
            },

            {
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "C", "J", "V", "Y", "X"
                    },

                    Words = new List<string>
                    {
                        "CITY",
                        "COUP",
                        "CAMP",
                        "CODE",
                        "CHEF",
                        "CHOR",
                        "CLAN",
                        "CLUB",
                        "COCK",
                        "COOL",
                        "COPY",
                        "JAHR",
                        "JADE",
                        "JAZZ",
                        "JEDE",
                        "JENE",
                        "JOJO",
                        "JUDO",
                        "JURY",
                        "VASE",
                        "VETO",
                        "VIER",
                        "VIEL",
                        "VOGT",
                        "VOLT",
                        "VORN",
                        "VOKE",
                        "YOGA",
                        "YETI",
                        "XYLO"
                    }
                }
            },

            {
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "Ä", "Ö", "Ü", "ß", "ÄU"
                    },

                    Words = new List<string>
                    {
                        "ÄSTE",
                        "ÄSER",
                        "ÄUGE",
                        "ÄRME",
                        "ÄHRE",
                        "ÖLEN",
                        "ÖDEM",
                        "ÖFEN",
                        "ÖSES",
                        "ÜBEL",
                        "ÜBEN",
                        "ÜBER",
                        "ÜBER",
                        "ÜBTE",
                        "ÜBLE",
                        "ÜBEN",
                        "SÜDE",
                        "SÜSS",
                        "MAßE",
                        "MAßT",
                        "GRÜN",
                        "FRÜH",
                        "KÜHE",
                        "KÜHL",
                        "KÜRE",
                        "LÖSE",
                        "LÖST",
                        "LÖWE",
                        "MÖGE",
                        "MÖGT",
                        "RÖTE",
                        "RÜBE",
                        "WÄRE",
                        "ZÄHE",
                        "ZÄHL",
                        "BÄRE",
                        "BÄKE",
                        "DÜNE",
                        "FÜGE",
                        "FÜGT"
                    }
                }
            },
        };
    }
}
