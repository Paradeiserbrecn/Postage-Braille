using System;
using Data;
using IO;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableLetterUnit : Focusable
    {
        [SerializeField] public Image border;
        [SerializeField] public TextMeshProUGUI indexTMP;
        [SerializeField] public TextMeshProUGUI lettersTMP;
        [SerializeField] public TextMeshProUGUI attemptsTMP;
        [SerializeField] public TextMeshProUGUI percentageTMP;
        [SerializeField] public RectTransform rectTransform;

        public LetterUnit letterUnit;

        private void OnEnable()
        {
            border.enabled = false;
            border.color = GlobalSettings.HighlightedColor;
        }

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, GlobalSettings.standardOutputType);
            if (LetterPackagePicker.Instance != null) LetterPackagePicker.Instance.ScrollTo(rectTransform);
            border.enabled = true;
        }

        public override void Unfocus()
        {
            border.enabled = false;
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
