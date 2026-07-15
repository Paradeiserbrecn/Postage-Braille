using System;
using Data;
using IO;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableLetterUnit : Focusable
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI indexTMP;
        [SerializeField] private TextMeshProUGUI lettersTMP;
        [SerializeField] private TextMeshProUGUI attemptsTMP;
        [SerializeField] private TextMeshProUGUI percentageTMP;

        public LetterUnit letterUnit;
        
        private void Awake()
        {
            image.color = GlobalSettings.MenuOptionColor;
            if (letterUnit != null)
            {
                text = letterUnit.unitIndex + " : " + letterUnit.Letters + " : " + letterUnit.attempts + " : " +
                       letterUnit.SuccessPercentage;
            }
        }

        private void Start()
        {
            if (letterUnit == null)
            {
                Debug.Log("You forgot to set the letter unit of the FocusableLetterUnit");
                return;
            }

            indexTMP.text = letterUnit.unitIndex.ToString();
            lettersTMP.text = letterUnit.Letters.ToString();
            attemptsTMP.text = letterUnit.attempts.ToString();
            percentageTMP.text = percentageTMP.text = letterUnit.SuccessPercentage + "%";
            
            indexTMP.color = GlobalSettings.PackageTextColor;
            lettersTMP.color = GlobalSettings.PackageTextColor;
            attemptsTMP.color = GlobalSettings.PackageTextColor;
            percentageTMP.color = GlobalSettings.PackageTextColor;
        }

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, GlobalSettings.standardOutputType);
            image.color = GlobalSettings.HighlightedColor;
        }

        public override void Unfocus()
        {
            image.color = GlobalSettings.MenuOptionColor;
        }

        public override void ConfirmAction()
        {
            if (letterUnit == null)
            {
                Debug.Log("Letter unit is not set");
                return;
            }
            
            var idx = LetterPackages.Instance.SelectLetterUnit(letterUnit);
            Debug.Log("Letter unit selected: " + idx);
            // GameManager.Instance.NextQuestion();
        }
    }
}
