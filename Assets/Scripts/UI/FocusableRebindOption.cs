using System;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using System.Collections.Generic;
using Braille;
using IO;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace UI
{
    public class FocusableRebindOption : Focusable
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI bindingText;
        [SerializeField] private Image border;
        [SerializeField] private RectTransform rectTransform;
        public InputActionsPanel inputActionsPanel;

        public InputAction inputAction;

        private void Awake()
        {
            border.enabled = false;
            border.color = GlobalSettings.HighlightedColor;
        }

        public override void Focus()
        {
            IOEventManager.AssistiveOutput(nameText.text + ": " + bindingText.text, AssistiveOutput.OutputType.Both);
            border.enabled = true;
            if (inputActionsPanel != null) inputActionsPanel.ScrollTo(rectTransform);
        }

        public override void Unfocus()
        {
            border.enabled = false;
        }

        public override void ConfirmAction()
        {
            ActionRebinder.Instance.RebindAction(inputAction, this);
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
