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
        [SerializeField] public RectTransform rectTransform;

        public LetterUnit letterUnit;

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, GlobalSettings.standardOutputType);
            if (LetterPackagePicker.Instance != null) LetterPackagePicker.Instance.ScrollTo(rectTransform);
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
            LetterPackagePicker.Instance.SelectLetterUnit(letterUnit);
            
            IOEventManager.InvokeAssistiveOutput("Einheit gewechselt.", GlobalSettings.standardOutputType);
            // GameManager.Instance.NextQuestion();
        }
    }
}
