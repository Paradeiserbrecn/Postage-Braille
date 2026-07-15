using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    /// <summary>
    /// Represents the languages currently supported by the letter package system.
    /// </summary>
    public enum SupportedLanguage
    {
        De
    }

    /// <summary>
    /// Represents a single learning unit containing a set of letters,
    /// associated practice words, and the player's progress statistics.
    /// </summary>
    [Serializable]
    public class LetterUnit
    {
        private static int nextUnitIndex = 1;

        /// <summary>
        /// Gets the unique index assigned to this letter unit.
        /// </summary>
        public readonly int unitIndex;

        /// <summary>
        /// Gets or sets the letters introduced in this unit.
        /// </summary>
        public List<string> Letters { get; set; } = new();

        /// <summary>
        /// Gets or sets the practice words for this unit.
        /// </summary>
        public List<string> Words { get; set; } = new();

        /// <summary>
        /// The total number of attempts made for this unit.
        /// </summary>
        public int attempts;

        /// <summary>
        /// The total number of successful attempts for this unit.
        /// </summary>
        public int successes;

        /// <summary>
        /// Gets the success rate as a percentage.
        /// Returns 50% if no attempts have been made.
        /// </summary>
        public double SuccessPercentage =>
            attempts == 0 ? 50 : Math.Round((double)successes / attempts * 100, 2);

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterUnit"/> class.
        /// </summary>
        /// <param name="Letters">The letters introduced in this unit.</param>
        /// <param name="Words">The practice words for this unit.</param>
        internal LetterUnit(List<string> Letters, List<string> Words)
        {
            unitIndex = nextUnitIndex;
            nextUnitIndex++;
            this.Letters = Letters;
            this.Words = Words;
            attempts = 0;
            successes = 0;
        }
    }

    /// <summary>
    /// Manages language-specific letter packages and tracks the player's
    /// progression through each learning unit.
    /// </summary>
    public class LetterPackages : MonoBehaviour
    {
        [SerializeField] private int startingPackageProgress = 1;

        /// <summary>
        /// Gets the currently selected unit index for the active language.
        /// </summary>
        public int PackageProgress => CurrentUnitForCurrentLanguage[currentLanguage];

        /// <summary>
        /// Gets the currently loaded package for the active language.
        /// </summary>
        public List<LetterUnit> currentLanguagePackage = GermanPackage;

        /// <summary>
        /// Gets the currently selected letter unit.
        /// </summary>
        public LetterUnit CurrentPackageProgresses =>
            currentLanguagePackage[CurrentUnitForCurrentLanguage[currentLanguage]];

        /// <summary>
        /// Gets or sets the currently selected language.
        /// </summary>
        public SupportedLanguage currentLanguage = SupportedLanguage.De;

        /// <summary>
        /// Stores the current unit index for each supported language.
        /// </summary>
        public static readonly Dictionary<SupportedLanguage, int> CurrentUnitForCurrentLanguage = new();

        /// <summary>
        /// Singleton instance of the <see cref="LetterPackages"/> component.
        /// </summary>
        public static LetterPackages Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            CurrentUnitForCurrentLanguage.Add(SupportedLanguage.De, startingPackageProgress);
        }

        /// <summary>
        /// Advances to the next letter unit if one exists.
        /// </summary>
        /// <returns>The newly selected <see cref="LetterUnit"/>.</returns>
        public LetterUnit ProgressLetterPackage()
        {
            if (CurrentUnitForCurrentLanguage[currentLanguage] < currentLanguagePackage.Count - 1)
            {
                CurrentUnitForCurrentLanguage[currentLanguage]++;
            }

            return currentLanguagePackage[CurrentUnitForCurrentLanguage[currentLanguage]];
        }

        /// <summary>
        /// Selects the specified letter unit.
        /// </summary>
        /// <param name="unit">The letter unit to select.</param>
        /// <returns>The index of the selected unit.</returns>
        public int SelectLetterUnit(LetterUnit unit)
        {
            var unitIndex = currentLanguagePackage.IndexOf(unit);
            CurrentUnitForCurrentLanguage[currentLanguage] = unitIndex;

            return unitIndex;
        }

        /// <summary>
        /// Selects a letter unit by its index.
        /// </summary>
        /// <param name="unitIndex">The index of the unit to select.</param>
        /// <returns>The selected <see cref="LetterUnit"/>.</returns>
        public LetterUnit SelectLetterUnit(int unitIndex)
        {
            CurrentUnitForCurrentLanguage[currentLanguage] = unitIndex;
            return currentLanguagePackage[unitIndex];
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

        /// <summary>
        /// The predefined German letter learning package.
        /// </summary>
        private static readonly List<LetterUnit> GermanPackage = new()
        {
            new LetterUnit
            (
                new List<string>
                {
                    "E", "N", "I", "S", "T"
                },
                new List<string>
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
            ),

            new LetterUnit
            (new List<string>
                {
                    "A", "R", "D", "H", "L"
                },
                new List<string>
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
            ),

            new LetterUnit
            (new List<string>
                {
                    "M", "U", "O", "G", "B"
                },
                new List<string>
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
            ),

            new LetterUnit
            (new List<string>
                {
                    "W", "F", "K", "Z", "P"
                },
                new List<string>
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
            ),

            new LetterUnit
            (new List<string>
                {
                    "C", "J", "V", "Y", "X"
                },
                new List<string>
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
            ),

            new LetterUnit
            (new List<string>
                {
                    "Ä", "Ö", "Ü", "ß", "ÄU"
                },
                new List<string>
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
            ),
        };
    }
}
