using System;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Utility;

namespace IO
{
    public class MultimodalInputManager : MonoBehaviour
    {
        private InputHandledBrailleTextObject _currentTextbox;
        
        private Dictionary<TextInputType, AbstractTextInput> _textInputs = new();
        private Dictionary<InputType, AbstractInput> _inputs = new();
        
        private GameActions _actions;
        public ActionRebinder ActionRebinder { get; private set; }
        
        [SerializeField] private InputHandledBrailleTextObject defaultTextbox;
        
        [SerializeField] private RebindUI rebindUI;

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
            _actions = new GameActions();
            
            _inputs[InputType.Navigation] = new NavigationInput(_actions);
            EnableInput(InputType.Navigation);
            
            _inputs[InputType.BrailleSettings] = new BrailleSettingsInput(_actions);
            EnableInput(InputType.BrailleSettings);
            
            _textInputs[TextInputType.Perkins] = new PerkinsTextInput(_actions);
            _textInputs[TextInputType.Keyboard] = new KeyboardTextInput(_actions);
            if (defaultTextbox != null)
            {
                EnableTextInput(TextInputType.Keyboard, defaultTextbox);
            }
        }

        private void Start()
        {
            ActionRebinder = new ActionRebinder(_actions);
            
            /////// The following is for testing only!!
            ActionRebinder.ListActions(_actions.Navigation);
            /////// end of testing section
        }


        public void EnableInput(InputType inputType)
        {
            _inputs[inputType].Enable();
        }

        public void DisableInput(InputType inputType)
        {
            _inputs[inputType].Disable();
        }

        public void EnableTextInput(TextInputType textInputType, InputHandledBrailleTextObject textBox)
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
