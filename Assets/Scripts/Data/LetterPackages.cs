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
                        "e", "n", "i", "s", "t"
                    },

                    Words = new List<string>
                    {
                        "ist",
                        "ein",
                        "seite",
                        "essen",
                        "see",
                        "tee",
                        "nest",
                        "seine",
                        "nein",
                        "seit"
                    }
                }
            },

            {
                2,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "a", "r", "d", "h", "l"
                    },

                    Words = new List<string>
                    {
                        "der",
                        "die",
                        "lernen",
                        "lesen",
                        "leider",
                        "laden",
                        "land",
                        "halle",
                        "reden",
                        "alle"
                    }
                }
            },

            {
                3,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "m", "u", "o", "g", "b"
                    },

                    Words = new List<string>
                    {
                        "hund",
                        "haus",
                        "baum",
                        "morgen",
                        "haben",
                        "boot",
                        "boden",
                        "gut",
                        "bauen",
                        "oben"
                    }
                }
            },

            {
                4,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "w", "f", "k", "z", "p"
                    },

                    Words = new List<string>
                    {
                        "wasser",
                        "katze",
                        "kopf",
                        "platz",
                        "weg",
                        "paket",
                        "zug",
                        "fenster",
                        "wolke",
                        "pflanze"
                    }
                }
            },

            {
                5,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "ä", "ö", "ü", "ß", "äu"
                    },

                    Words = new List<string>
                    {
                        "für",
                        "fünf",
                        "groß",
                        "schön",
                        "häuser",
                        "bär",
                        "grüßen",
                        "mäßig",
                        "löwe",
                        "bäume"
                    }
                }
            },

            {
                6,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "c", "j", "v", "y", "x"
                    },

                    Words = new List<string>
                    {
                        "clown",
                        "jacke",
                        "vase",
                        "yoga",
                        "taxi",
                        "video",
                        "computer",
                        "xylophon",
                        "januar",
                        "café"
                    }
                }
            },

            {
                7,
                new LetterUnit
                {
                    Letters = new List<string>
                    {
                        "ch", "sch", "ei", "ie", "eu"
                    },

                    Words = new List<string>
                    {
                        "schule",
                        "schreiben",
                        "eis",
                        "liebe",
                        "freund",
                        "deutsch",
                        "eule",
                        "schiff",
                        "heiß",
                        "schokolade"
                    }
                }
            }
        };
    }
}