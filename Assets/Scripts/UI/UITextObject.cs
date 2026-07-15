using System;
using Braille;
using IO;
using Settings;
using TMPro;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using BrailleObject = Braille.BrailleObject;

namespace UI
{
    public class UITextObject : Focusable
    {
        [SerializeField] private GridLayoutGroup _layoutGroup;
        [SerializeField] protected TextMeshProUGUI _textMeshPro;
        public AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.Both;
        
        private DisplayMode _displayMode;
        public enum DisplayMode
        {
            Braille,
            InkPrint
        }

        void Awake()
        {
            UpdateSpacing();
            UpdateCharacterSize();

            IOEventManager.BrailleSpacingChanged += UpdateSpacing;
            IOEventManager.LineSpacingChanged += UpdateSpacing;
            IOEventManager.BrailleSizeChanged += UpdateCharacterSize;
            IOEventManager.BrailleColorChanged += UpdateDotColor;
        }

        private void OnEnable()
        {
            Awake();
        }
        
        private void OnDisable()
        {
            OnDestroy();
        }

        private void OnDestroy()
        {
            IOEventManager.BrailleSpacingChanged -= UpdateSpacing;
            IOEventManager.LineSpacingChanged -= UpdateSpacing;
            IOEventManager.BrailleSizeChanged -= UpdateCharacterSize;
        }

        public void SetOutputType(AssistiveOutput.OutputType outputType)
        {
            this.outputType = outputType;
        }

        public void SetDisplayMode(DisplayMode displayMode)
        {
            if (displayMode == DisplayMode.Braille)
            {
                foreach (BrailleObject character in GetComponentsInChildren<BrailleObject>())
                {
                    character.gameObject.SetActive(true);
                }
                _textMeshPro.enabled = false;
            }
            else if (displayMode == DisplayMode.InkPrint)
            {
                foreach (BrailleObject character in GetComponentsInChildren<BrailleObject>())
                {
                    character.gameObject.SetActive(false);
                }
                _textMeshPro.enabled = true;
            }
            else
            {
                Debug.LogWarning("Tried to switch to unsupported display mode.");
                return;
            }
            _displayMode = displayMode;
            Debug.Log(text + " display mode is " + displayMode + "and the text was enabled?" + _textMeshPro.enabled);
        }

        public void UpdateBlackletterText()
        {
            _textMeshPro.text = text;
        }

        void UpdateSpacing()
        {
            _layoutGroup.spacing = new Vector2(GlobalSettings.BrailleSpacing, GlobalSettings.LineSpacing);
            _textMeshPro.characterSpacing = GlobalSettings.BrailleSpacing;
            _textMeshPro.lineSpacing = GlobalSettings.LineSpacing;
        }

        private void UpdateCharacterSize()
        {
            Vector2 scale = Vector2.one * GlobalSettings.BrailleSize;
            scale.x *= 2; //two dots horizontally
            scale.y *= 3; //three dots vertically
            _layoutGroup.cellSize = scale;
            _textMeshPro.fontSize = scale.y;
        }

        public void UpdateDotColor()
        {
            foreach (BrailleObject character in GetComponentsInChildren<BrailleObject>())
            {
                character.UpdateDotColor();
            }

            _textMeshPro.color = GlobalSettings.BrailleColor;
        }

        public void UpdateDotColor(Color color)
        {
            foreach (BrailleObject character in GetComponentsInChildren<BrailleObject>())
            {
                character.UpdateDotColor(color);
            }

            _textMeshPro.color = color;
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