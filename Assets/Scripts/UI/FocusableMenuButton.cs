using System;
using Braille;
using IO;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableMenuButton : Focusable
    {
        [SerializeField] internal Image image;
        [SerializeField] internal Image iconImage;
        [SerializeField] internal Image border;
        [SerializeField] internal UnityEvent action;
        [SerializeField] internal string confirmText;


        private void Awake()
        {
            border.color = GlobalSettings.HighlightedColor;
            border.enabled = false;
        }

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, GlobalSettings.standardOutputType);
            border.enabled = true;
        }

        public override void Unfocus()
        {
            border.enabled = false;
        }

        public override void ConfirmAction()
        {
            if (confirmText != "") IOEventManager.InvokeAssistiveOutput(confirmText, GlobalSettings.standardOutputType);
            action.Invoke();
        }
    }
}
