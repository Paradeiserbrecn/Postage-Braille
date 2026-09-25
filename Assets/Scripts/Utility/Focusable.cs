using System;
using Braille;
using IO;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Utility
{
    public abstract class Focusable : MonoBehaviour
    {
        public string text;
        [SerializeField] public Image border;
        [SerializeField] internal string confirmText;
        public AssistiveOutput.OutputType assistiveOutputType = GlobalSettings.standardOutputType;
        private void OnEnable()
        {
            border.enabled = false;
            border.color = GlobalSettings.HighlightedColor;
        }

        public virtual void Focus()
        {
            if (!String.IsNullOrEmpty(text)) IOEventManager.InvokeAssistiveOutput(text, assistiveOutputType);
            border.enabled = true;
        }

        public virtual void Unfocus()
        {
            border.enabled = false;
        }

        public virtual void ConfirmAction()
        {
            if (!String.IsNullOrEmpty(confirmText)) IOEventManager.InvokeAssistiveOutput(confirmText, assistiveOutputType);
        }
    }
}
