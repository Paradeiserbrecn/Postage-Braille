using System.IO;
using Data;
using UnityEngine;

namespace Serialization
{
    public static class SaveSystem
    {
        public static void SavePackageProgress(LetterPackages packages)
        {
            var save = new LetterPackagesSaveData
            {
                currentLanguage = packages.currentLanguage,
                currentUnit = packages.packageProgress
            };

            foreach (var unit in packages.currentLanguagePackage)
            {
                save.progress.Add(new LetterUnitProgress
                {
                    attempts = unit.attempts,
                    successes = unit.successes
                });
            }

            string json = JsonUtility.ToJson(save, true);

            File.WriteAllText(
                Path.Combine(Application.persistentDataPath, "save.json"),
                json);
        }

        public static void LoadPackageProgress(LetterPackages packages)
        {
            string path = Path.Combine(Application.persistentDataPath, "save.json");

            if (!File.Exists(path))
                return;

            var save = JsonUtility.FromJson<LetterPackagesSaveData>(
                File.ReadAllText(path));

            for (int i = 0;
                 i < save.progress.Count &&
                 i < packages.Packages[save.currentLanguage].Count;
                 i++)
            {
                packages.Packages[save.currentLanguage][i].attempts =
                    save.progress[i].attempts;

                packages.Packages[save.currentLanguage][i].successes =
                    save.progress[i].successes;
            }

            packages.SelectLetterUnit(save.currentUnit);
        }
    }
}
