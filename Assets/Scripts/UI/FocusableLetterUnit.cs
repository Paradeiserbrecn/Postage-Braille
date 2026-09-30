using Data;
using IO;
using Settings;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class FocusableLetterUnit : Focusable
    {
        [SerializeField] public TextMeshProUGUI indexTMP;
        [SerializeField] public TextMeshProUGUI lettersTMP;
        [SerializeField] public TextMeshProUGUI attemptsTMP;
        [SerializeField] public TextMeshProUGUI percentageTMP;
        [SerializeField] public RectTransform rectTransform;

        public LetterUnit letterUnit;
        public override void Focus()
        {
            base.Focus();
            if (LetterPackagePicker.Instance != null) LetterPackagePicker.Instance.ScrollTo(rectTransform);
        }

        public override void ConfirmAction()
        {
            if (letterUnit == null)
            {
                Debug.LogWarning("Letter unit is not set");
                return;
            }
            
            var idx = LetterPackages.Instance.SelectLetterUnit(letterUnit);
            Debug.Log("Letter unit selected: " + idx);
            LetterPackagePicker.Instance.SelectLetterUnit(letterUnit);
            
            IOEventManager.InvokeAssistiveOutput("Einheit gewechselt.", GlobalSettings.standardOutputType);
        }
    }
}
