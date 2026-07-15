using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Utility;

namespace IO
{
    public class MultimodalInputManager : MonoBehaviour
    {
        private InputHandledUITextObject _currentTextbox;
        
        private Dictionary<TextInputType, AbstractTextInput> _textInputs = new();
        private Dictionary<InputType, AbstractInput> _inputs = new();
        
        public GameActions Actions { get; private set; }
        public ActionRebinder ActionRebinder { get; private set; }
        
        [SerializeField] private InputHandledUITextObject defaultTextbox;

        public enum TextInputType
        {
            Perkins,
            Keyboard
        }

        public enum InputType
        {
            Navigation,
            BrailleSettings
        }
        
        public static MultimodalInputManager Instance;
        private void Awake()
        {
            Instance = this;
            Actions = new GameActions();
            
            ActionRebinder = new ActionRebinder(Actions);
            
            _inputs[InputType.Navigation] = new NavigationInput(Actions);
            EnableInput(InputType.Navigation);
            
            _inputs[InputType.BrailleSettings] = new BrailleSettingsInput(Actions);
            EnableInput(InputType.BrailleSettings);
            
            _textInputs[TextInputType.Perkins] = new PerkinsTextInput(Actions);
            _textInputs[TextInputType.Keyboard] = new KeyboardTextInput(Actions);
            if (defaultTextbox != null)
            {
                EnableTextInput(TextInputType.Keyboard, defaultTextbox);
            }
        }
        


        public void EnableInput(InputType inputType)
        {
            _inputs[inputType].Enable();
        }

        public void DisableInput(InputType inputType)
        {
            _inputs[inputType].Disable();
        }

        public void EnableTextInput(TextInputType textInputType, InputHandledUITextObject textBox)
        {
            DisableTextInput();
            
            _textInputs[textInputType].Textbox = textBox;
            _textInputs[textInputType].Enable();
            _currentTextbox = textBox;
            
            var notifier = _currentTextbox.destroyDisableNotifier;
            if (notifier == null)
            {
                notifier = _currentTextbox.destroyDisableNotifier;
            }
            notifier.Destroyed += DisableTextInput;
            notifier.Disabled += DisableTextInput;
        }

        public void DisableTextInput()
        {
            foreach (var input in _textInputs.Keys)
            {
                _textInputs[input].Disable();
            }

            if (_currentTextbox != null)
            {
                var notifier = _currentTextbox.destroyDisableNotifier;
                if (notifier != null)
                {
                    notifier.Destroyed -= DisableTextInput;
                    notifier.Disabled -= DisableTextInput;
                }
                _currentTextbox = null;
            }
        }
    }
}
