using Settings;
using TMPro;
using UnityEngine;
using Utility;
using Braille;
using IO;
using UnityEngine.InputSystem;

namespace UI
{
    public class FocusableRebindOption : Focusable
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI bindingText;
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
            base.Focus();
            IOEventManager.AssistiveOutput(nameText.text + ": " + bindingText.text, AssistiveOutput.OutputType.Both);
            if (inputActionsPanel != null) inputActionsPanel.ScrollTo(rectTransform);
        }

        public override void ConfirmAction()
        {
            base.ConfirmAction();
            ActionRebinder.Instance.RebindAction(inputAction, this);
        }

        public void SetActionName(string actionName)
        {
            nameText.text = actionName;
        }

        public void SetBindingText(string binding)
        {
            bindingText.text = binding;
        }
    }
}
