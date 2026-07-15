using System;
using Data;
using Settings;
using UnityEngine;

namespace UI
{
    public class LetterPackagePicker : MonoBehaviour
    {
        private const string PackageLayerName = "PackageLayer";
        private int _packageLayerIndex;
        private UILayer PackageLayer => UIManager.Instance.layers[_packageLayerIndex];

        [SerializeField] private GameObject packageListObject;
        [SerializeField] private GameObject packagePrefab;

        void Start()
        {
            _packageLayerIndex = UIManager.Instance.AddLayer(new UILayer(PackageLayerName));
            PopulateWithCurrentLanguagePackage();
        }

        public void PopulateWithCurrentLanguagePackage()
        {
            foreach (LetterUnit letterUnit in LetterPackages.Instance.currentLanguagePackage)
            {
                FocusableLetterUnit focusableUnit = Instantiate(packagePrefab, packageListObject.transform)
                    .GetComponentInChildren<FocusableLetterUnit>();


                focusableUnit.image.color = GlobalSettings.MenuOptionColor;
                focusableUnit.text = letterUnit.unitIndex + " : " + letterUnit.Letters + " : " + letterUnit.attempts +
                                     " : " +
                                     letterUnit.SuccessPercentage;

                focusableUnit.indexTMP.text = letterUnit.unitIndex.ToString();
                focusableUnit.lettersTMP.text = letterUnit.Letters.ToString();
                focusableUnit.attemptsTMP.text = letterUnit.attempts.ToString();
                focusableUnit.percentageTMP.text = letterUnit.SuccessPercentage + "%";

                focusableUnit.indexTMP.color = GlobalSettings.PackageTextColor;
                focusableUnit.lettersTMP.color = GlobalSettings.PackageTextColor;
                focusableUnit.attemptsTMP.color = GlobalSettings.PackageTextColor;
                focusableUnit.percentageTMP.color = GlobalSettings.PackageTextColor;


                focusableUnit.letterUnit = letterUnit;
                PackageLayer.Add(focusableUnit);
            }
        }
    }
}
