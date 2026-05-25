using UnityEngine;

namespace IO
{
    public class MultimodalInput : MonoBehaviour
    {
        private InputHandledBrailleTextObject _textbox;
    
        private PerkinsTextInput _perkinsTextInput;
    
        private void Start()
        {
            _textbox = GetComponent<InputHandledBrailleTextObject>();
            _perkinsTextInput = new PerkinsTextInput();
            _perkinsTextInput.Textbox = _textbox;
            _perkinsTextInput.OnEnable();
        }
    }
}
