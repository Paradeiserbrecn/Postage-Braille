using Braille;
using IO;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class BrailleTextObject : Focusable
    {
        [SerializeField] private GridLayoutGroup _layoutGroup;
        public string text;
        [FormerlySerializedAs("type")] public AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.Both;

        void Awake()
        {
            UpdateSpacing();
            UpdateCharacterSize();

            //TODO: subscribe to BrailleSpacing and LineSpacing change event

            IOEventManager.BrailleSpacingChanged += UpdateSpacing;
            IOEventManager.LineSpacingChanged += UpdateSpacing;
            IOEventManager.BrailleSizeChanged += UpdateCharacterSize;
        }

        void UpdateSpacing()
        {
            _layoutGroup.spacing = new Vector2(GlobalSettings.BrailleSpacing, GlobalSettings.LineSpacing);
        }

        private void UpdateCharacterSize()
        {
            Vector2 scale = Vector2.one * GlobalSettings.BrailleSize;
            scale.x *= 2; //two dots horizontally
            scale.y *= 3; //three dots vertically
            _layoutGroup.cellSize = scale;
        }

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
            foreach (var brailleObject in GetComponentsInChildren<BrailleObject>())
            {
                brailleObject.HighlightDots();
            }
        }

        public override void Unfocus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
            foreach (var brailleObject in GetComponentsInChildren<BrailleObject>())
            {
                brailleObject.UpdateDotColor();
            }
        }

        public override void ConfirmAction()
        {
            switch (GameManager.Instance.currentState)
            {
                case GameManager.GameState.WaitingForInput:
                    GameManager.Instance.SubmitAnswer(text);
                    break;
                default:
                    Debug.LogWarning("Confirmed focus when no confirm action was provided");
                    break;
            }
        }
    }
}
