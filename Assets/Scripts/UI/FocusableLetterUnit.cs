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
        [SerializeField] public Image image;
        [SerializeField] public TextMeshProUGUI indexTMP;
        [SerializeField] public TextMeshProUGUI lettersTMP;
        [SerializeField] public TextMeshProUGUI attemptsTMP;
        [SerializeField] public TextMeshProUGUI percentageTMP;

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
