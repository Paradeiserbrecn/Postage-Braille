using System;
using Braille;
using IO;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace UI
{
    public class FocusableTextObject : Focusable
    {
        public AssistiveOutput.OutputType OutputType = AssistiveOutput.OutputType.Both;
        public TextMeshProUGUI tmpText;

        private bool _focused = false;
        private string _text;
        private string _displayTextOverride;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                tmpText.text = _text;
            }
        }

        /// <summary>
        /// Overrides the displayed text.
        /// Set to null to display <see cref="Text"/> again.
        /// </summary>
        public string DisplayText
        {
            get => tmpText.text;
            set => tmpText.text = value;
        }

        private void RefreshDisplayedText()
        {
            tmpText.text = _displayTextOverride ?? _text;
        }

        public void Initialize(TextMeshProUGUI textMesh)
        {
            tmpText = textMesh;
        }

        /// <summary>
        /// Highlights the object and sends assistive output with the specified OutputType
        /// </summary>
        public override void Focus()
        {
            _focused = true;
            if (tmpText.text != null) IOEventManager.InvokeAssistiveOutput(tmpText.text, OutputType);
            tmpText.color = GlobalSettings.HighlightedColor;
        }

        public override void Unfocus()
        {
            _focused = false;
            if (tmpText.text != null) IOEventManager.InvokeAssistiveOutput(tmpText.text, OutputType);
            tmpText.color = GlobalSettings.TextColor;
        }

        public override void ConfirmAction()
        {
            if (!_focused) throw new Exception("Tried to Execute Focus action on unfocused object");
            switch (GameManager.Instance.currentState)
            {
                case GameManager.GameState.WaitingForInput:
                    GameManager.Instance.SubmitAnswer(Text);
                    break;
                default:
                    Debug.LogWarning("Confirmed focus when no confirm action was provided");
                    break;
            }
        }
    }
}