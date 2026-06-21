using System;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using System.Collections.Generic;
using IO;
using UnityEngine.InputSystem;

namespace UI
{
    public class FocusableRebindOption : Focusable
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI bindingText;
        [SerializeField] private Image background;
        
        public InputAction InputAction;

        public override void Focus()
        {
            background.color = GlobalSettings.HighlightedButtonColor;
            Debug.Log(InputAction);
        }

        public override void Unfocus()
        {
            background.color = GlobalSettings.MenuOptionColor;
        }

        public override void ConfirmAction()
        {
            MultimodalInputManager.Instance.ActionRebinder?.RebindAction(InputAction, this);
        }

        public void SetActionName(string text)
        { 
            nameText.text = text;
        }

        public void SetBindingText(string text)
        {
            bindingText.text = text;
        }
    }
}