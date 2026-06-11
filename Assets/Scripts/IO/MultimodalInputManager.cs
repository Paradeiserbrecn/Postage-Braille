using UnityEngine;
using System.Collections.Generic;

namespace IO
{
    public class MultimodalInputManager : MonoBehaviour
    {
        private NavigationInput _navigation;
        private BrailleSettingsInput _brailleSettingsInput;
        
        private InputHandledBrailleTextObject _currentTextbox;
        private Dictionary<TextInputType, AbstractTextInput> _textInputs = new();
        
        private GameActions _actions;
        
        [SerializeField] private InputHandledBrailleTextObject defaultTextbox;
        
        private ActionRebinder _actionRebinder;
        [SerializeField] private RebindUI rebindUI;

        public enum TextInputType
        {
            Perkins,
            Keyboard
        }
        
        public static MultimodalInputManager Instance;
        private void Awake()
        {
            Instance = this;
            _actions = new GameActions();
        }
    
        private void Start()
        {
            _actionRebinder = new ActionRebinder(_actions, rebindUI);
            
            _navigation = new NavigationInput();
            //_navigation.Enable();
            
            _brailleSettingsInput = new BrailleSettingsInput();
            _brailleSettingsInput.Enable();
            
            _textInputs[TextInputType.Perkins] = new PerkinsTextInput();
            _textInputs[TextInputType.Keyboard] = new KeyboardTextInput();
            EnableTextInput(TextInputType.Perkins, defaultTextbox);
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
