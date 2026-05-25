using IO;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MultimodalInput : MonoBehaviour
{
    [SerializeField] private TextBoxController _textbox;
    
    private PerkinsInputHandler _perkinsInputHandler;
    
    private void Start()
    {
        _perkinsInputHandler = new PerkinsInputHandler();
        _perkinsInputHandler.textbox = _textbox;
        _perkinsInputHandler.OnEnable();
    }
}
