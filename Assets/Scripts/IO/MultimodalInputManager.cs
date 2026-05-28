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

        public enum TextInputType
        {
            Perkins
        }
        
        public static MultimodalInputManager Instance;
        private void Awake()
        {
            Instance = this;
        }
    
        private void Start()
        {
            _navigation = new NavigationInput();
            _navigation.Enable();
            
            _brailleSettingsInput = new BrailleSettingsInput();
            _brailleSettingsInput.Enable();
            
            _textInputs[TextInputType.Perkins] = new PerkinsTextInput();
        }

        public void EnableTextInput(TextInputType textInputType, InputHandledBrailleTextObject textBox)
        {
            DisableTextInput();
            _textInputs[textInputType].Enable();
            _textInputs[textInputType].Textbox = textBox;
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
