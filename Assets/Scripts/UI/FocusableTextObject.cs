using Braille;
using Settings;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class FocusableTextObject : Focusable
    {
        public AssistiveOutput.OutputType OutputType = AssistiveOutput.OutputType.Both;
        public TextMeshProUGUI tmpText;
        
        private string _displayTextOverride;

        public string Text
        {
            get => text;
            set
            {
                text = value;
                tmpText.text = text;
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

        private void Awake()
        {
            tmpText.color = GlobalSettings.TextColor;
        }

        private void RefreshDisplayedText()
        {
            tmpText.text = _displayTextOverride ?? text;
        }

        public void Initialize(TextMeshProUGUI textMesh)
        {
            tmpText = textMesh;
        }

        /// <summary>
        /// Highlights the object and sends assistive output with the specified OutputType
        /// </summary>

        public override void ConfirmAction()
        {
            base.ConfirmAction();
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
