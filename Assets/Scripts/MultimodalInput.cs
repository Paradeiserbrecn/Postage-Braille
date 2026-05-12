using System;
using UnityEngine;

public class MultimodalInput : MonoBehaviour
{
    private PerkinsInputHandler perkinsInputHandler;
    public enum InputModes
    {
        KEYBOARD, PERKINS
    }
    void Start()
    {
        perkinsInputHandler = GetComponent<PerkinsInputHandler>();
    }

    void ChangeMode(InputModes inputMode)
    {
        
    }
}
