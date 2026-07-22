using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using Serialization;
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
    /// Manages language-specific letter packages and tracks the player's
    /// progression through each learning unit.
    /// </summary>
    public class LetterPackages : MonoBehaviour
    {
        /// <summary>
        /// Gets the currently loaded package for the active language.
        /// </summary>
        [HideInInspector] public List<LetterUnit> currentLanguagePackage;

        /// <summary>
        /// Gets the currently selected letter unit.
        /// </summary>
        public LetterUnit CurrentPackageProgresses =>
            packageProgress >= currentLanguagePackage.Count ? null : currentLanguagePackage[packageProgress];

        /// <summary>
        /// Gets or sets the currently selected language.
        /// </summary>
        public SupportedLanguage currentLanguage = SupportedLanguage.De;

        /// <summary>
        /// The currently selected unit index for the active language.
        /// </summary>
        public int packageProgress = 1;

        /// <summary>
        /// Singleton instance of the <see cref="LetterPackages"/> component.
        /// </summary>
        public static LetterPackages Instance;

        /// <summary>
        /// The packages loaded from Resources/LetterPackages
        /// </summary>
        public readonly Dictionary<SupportedLanguage, List<LetterUnit>> Packages = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            foreach (var language in Enum.GetValues(typeof(SupportedLanguage)).Cast<SupportedLanguage>())
            {
                LoadLetterPackageData(language);
            }

            SaveSystem.LoadPackageProgress(Instance);

            ChangePackageLanguage(currentLanguage);
        }

        private void LoadLetterPackageData(SupportedLanguage language)
        {
            TextAsset json = Resources.Load<TextAsset>("LetterPackages/" + language.HumanName());

            LetterPackageData package =
                JsonUtility.FromJson<LetterPackageData>(json.text);


            List<LetterUnit> runtimePackage = new();

            foreach (var unit in package.units)
            {
                runtimePackage.Add(new LetterUnit(unit.letters, unit.words));
            }

            Packages.Add(language, runtimePackage);
        }

        /// <summary>
        /// Advances to the next letter unit if one exists.
        /// </summary>
        /// <returns>The newly selected <see cref="LetterUnit"/>.</returns>
        public LetterUnit ProgressLetterPackage()
        {
            if (packageProgress < currentLanguagePackage.Count - 1)
            {
                packageProgress++;
            }

            return currentLanguagePackage[packageProgress];
        }

        /// <summary>
        /// Selects the specified letter unit.
        /// </summary>
        /// <param name="unit">The letter unit to select.</param>
        /// <returns>The index of the selected unit.</returns>
        public int SelectLetterUnit(LetterUnit unit)
        {
            var unitIndex = currentLanguagePackage.IndexOf(unit);
            packageProgress = unitIndex;

            return unitIndex;
        }

        /// <summary>
        /// Selects a letter unit by its index.
        /// </summary>
        /// <param name="unitIndex">The index of the unit to select.</param>
        /// <returns>The selected <see cref="LetterUnit"/>.</returns>
        public LetterUnit SelectLetterUnit(int unitIndex)
        {
            packageProgress = unitIndex;
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
                    currentLanguagePackage = Packages[SupportedLanguage.De];
                    currentLanguage = SupportedLanguage.De;
                    break;
                default:
                    throw new UnexpectedEnumValueException<SupportedLanguage>(language);
            }

            return currentLanguagePackage;
        }


        public static void SaveCurrentLetterPackagesProgress()
        {
            SaveSystem.SavePackageProgress(LetterPackages.Instance);
        }

        public static void LoadCurrentLetterPackagesProgress()
        {
            SaveSystem.LoadPackageProgress(LetterPackages.Instance);
        }
    }
}
