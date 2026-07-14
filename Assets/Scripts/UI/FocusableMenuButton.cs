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
        [SerializeField] internal UnityEvent action;

        private void Awake()
        {
            image.color = GlobalSettings.MenuOptionColor;
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
            action.Invoke();
        }
    }
}