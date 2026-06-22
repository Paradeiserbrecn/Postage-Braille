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
                        "IST",
                        "EIN",
                        "SEITE",
                        "ESSEN",
                        "SEE",
                        "TEE",
                        "NEST",
                        "SEINE",
                        "NEIN",
                        "SEIT"
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
                        "DER",
                        "DIE",
                        "LERNEN",
                        "LESEN",
                        "LEIDER",
                        "LADEN",
                        "LAND",
                        "HALLE",
                        "REDEN",
                        "ALLE"
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
                        "HUND",
                        "HAUS",
                        "BAUM",
                        "MORGEN",
                        "HABEN",
                        "BOOT",
                        "BODEN",
                        "GUT",
                        "BAUEN",
                        "OBEN"
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
                        "WASSER",
                        "KATZE",
                        "KOPF",
                        "PLATZ",
                        "WEG",
                        "PAKET",
                        "ZUG",
                        "FENSTER",
                        "WOLKE",
                        "PFLANZE"
                    }
                }
            },

            {
                5,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "Ä", "Ö", "Ü", "ß", "ÄU"
                    },

                    Words = new List<string>
                    {
                        "FÜR",
                        "FÜNF",
                        "GROß",
                        "SCHÖN",
                        "HÄUSER",
                        "BÄR",
                        "GRÜßEN",
                        "MÄßIG",
                        "LÖWE",
                        "BÄUME"
                    }
                }
            },

            {
                6,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "C", "J", "V", "Y", "X"
                    },

                    Words = new List<string>
                    {
                        "CLOWN",
                        "JACKE",
                        "VASE",
                        "YOGA",
                        "TAXI",
                        "VIDEO",
                        "COMPUTER",
                        "XYLOPHON",
                        "JANUAR",
                        "CAFÉ"
                    }
                }
            },

            {
                7,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "CH", "SCH", "EI", "IE", "EU"
                    },

                    Words = new List<string>
                    {
                        "SCHULE",
                        "SCHREIBEN",
                        "EIS",
                        "LIEBE",
                        "FREUND",
                        "DEUTSCH",
                        "EULE",
                        "SCHIFF",
                        "HEIß",
                        "SCHOKOLADE"
                    }
                }
            }
        };
    }
}