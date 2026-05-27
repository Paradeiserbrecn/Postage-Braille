using UnityEngine;
using System.Collections.Generic;

namespace IO
{
    public class MultimodalInputManager : MonoBehaviour
    {
        private InputHandledBrailleTextObject _textbox;
    
        private NavigationInput _navigation;
        private PerkinsTextInput _perkinsTextInput;
        private BrailleSettingsInput _brailleSettingsInput;
    
        private void Start()
        {
            _textbox = GetComponent<InputHandledBrailleTextObject>();
            _perkinsTextInput = new PerkinsTextInput();
            _perkinsTextInput.Textbox = _textbox;
            _perkinsTextInput.Enable();
            
            _navigation = new NavigationInput();
            _navigation.Enable();
            
            _brailleSettingsInput = new BrailleSettingsInput();
            _brailleSettingsInput.Enable();
        }
    }
}
