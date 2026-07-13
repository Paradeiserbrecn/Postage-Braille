using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Data
{
    public enum SupportedLanguage
    {
        de
    }

    public class LetterUnit
    {
        public List<string> Letters { get; set; } = new();
        public List<string> Words { get; set; } = new();
    }

    public class LetterPackages : MonoBehaviour
    {
        [SerializeField] private int startingPackageProgress = 1;
        public int PackageProgress => LanguageProgress[currentLanguage];

        public Dictionary<int, LetterUnit> CurrentLanguagePackage = GermanPackage;

        public LetterUnit CurrentPackageProgresses => CurrentLanguagePackage[LanguageProgress[currentLanguage]];

        public SupportedLanguage currentLanguage = SupportedLanguage.de;

        public static readonly Dictionary<SupportedLanguage, int> LanguageProgress = new();
        public static LetterPackages Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            LanguageProgress.Add(SupportedLanguage.de, startingPackageProgress);
        }


        public LetterUnit ProgressLetterPackage()
        {
            if (LanguageProgress[currentLanguage] < CurrentLanguagePackage.Count - 1)
            {
                LanguageProgress[currentLanguage]++;
            }

            return CurrentLanguagePackage[LanguageProgress[currentLanguage]];
        }

        /// <summary>
        /// Changes the internal CurrentPackage and all according variables
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        /// <exception cref="UnexpectedEnumValueException{SupportedLanguages}"></exception>
        public Dictionary<int, LetterUnit> ChangePackageLanguage(SupportedLanguage language)
        {
            switch (language)
            {
                case SupportedLanguage.de:
                    CurrentLanguagePackage = GermanPackage;
                    currentLanguage = SupportedLanguage.de;
                    break;
                default:
                    throw new UnexpectedEnumValueException<SupportedLanguage>(language);
            }

            return CurrentLanguagePackage;
        }

        public static readonly Dictionary<int, LetterUnit> GermanPackage = new()
        {
            {
                1,
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
                2,
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
                3,
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
                4,
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
                5,
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
                6,
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