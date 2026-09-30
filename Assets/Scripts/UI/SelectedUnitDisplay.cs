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
    public class SelectedUnitDisplay : Focusable
    {
        [SerializeField] public TextMeshProUGUI indexTMP;
        [SerializeField] public TextMeshProUGUI lettersTMP;
        [SerializeField] public TextMeshProUGUI percentageTMP;

        public LetterUnit LetterUnit { get; private set; }

        private void Start()
        {
            indexTMP.color = GlobalSettings.TextColor;
            lettersTMP.color = GlobalSettings.PackageTextColor;
            percentageTMP.color = GlobalSettings.PackageTextColor;
        }

        public void ChangeLetterUnit(LetterUnit letterUnit)
        {
            this.LetterUnit = letterUnit;
            indexTMP.text = letterUnit.UnitIndex.ToString();
            lettersTMP.text = string.Join(", ", letterUnit.Letters);
            percentageTMP.text = letterUnit.SuccessPercentage + "%";

            text =
                    $"Einheit     {letterUnit.UnitIndex},\n" +
                    $"Buchstaben:  {string.Join(", ", letterUnit.Letters)},\n" +
                    $"Versuche: {letterUnit.attempts},\n" +
                    $"Erfolge:  {letterUnit.SuccessPercentage}%";
        }

        public override void Focus()
        {
            if (text != null)
                IOEventManager.InvokeAssistiveOutput("Derzeitig Aktive Einheit: " + text,
                    GlobalSettings.standardOutputType);
            border.enabled = true;
        }

        public override void ConfirmAction()
        {
            if (text != null)
                IOEventManager.InvokeAssistiveOutput("Derzeitig Aktive Einheit: " + text,
                    GlobalSettings.standardOutputType);
        }
    }
}
