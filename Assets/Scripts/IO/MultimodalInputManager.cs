using UnityEngine;
using System.Collections.Generic;

namespace IO
{
    public class MultimodalInputManager : MonoBehaviour
    {
        private InputHandledBrailleTextObject _currentTextbox;
        
        private Dictionary<TextInputType, AbstractTextInput> _textInputs = new();
        private Dictionary<InputType, AbstractInput> _inputs = new();
        
        private GameActions _actions;
        
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
        }
    
        private void Start()
        {
            _inputs[InputType.Navigation] = new NavigationInput();
            EnableInput(InputType.Navigation);
            
            _inputs[InputType.BrailleSettings] = new BrailleSettingsInput();
            EnableInput(InputType.BrailleSettings);
            
            _textInputs[TextInputType.Perkins] = new PerkinsTextInput();
            _textInputs[TextInputType.Keyboard] = new KeyboardTextInput();
            EnableTextInput(TextInputType.Keyboard, defaultTextbox);
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
        }

        public void DisableTextInput()
        {
            foreach (var input in _textInputs.Keys)
            {
                _textInputs[input].Disable();
            }
        }
    }
}
