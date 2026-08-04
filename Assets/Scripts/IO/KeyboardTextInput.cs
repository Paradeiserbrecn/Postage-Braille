using System;
using System.Collections.Generic;
using Braille;
using IO;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class KeyboardTextInput: AbstractTextInput
{
    private bool _shiftActive, _typingNumber = false;
    
    public KeyboardTextInput(GameActions gameActions) : base(gameActions)
    {
        this.Actions = gameActions;
    }
    
    private readonly List<(InputAction action, Action<InputAction.CallbackContext> callback)> _bindings
        = new();

    private void Bind(InputAction action, char c)
    {
        Action<InputAction.CallbackContext> callback = ctx => OnBasicKeyPress(c, ctx);

        _bindings.Add((action, callback));
        action.started += callback;
    }

    public void UnBindAll()
    {
        foreach (var (action, callback) in _bindings)
            action.started -= callback;

        _bindings.Clear();
    }
    
    public override void Enable()
    {
        Actions.BrailleKeyboard.Enable();
    
        Bind(Actions.BrailleKeyboard.A, 'a');
        Bind(Actions.BrailleKeyboard.B, 'b');
        Bind(Actions.BrailleKeyboard.C, 'c');
        Bind(Actions.BrailleKeyboard.D, 'd');
        Bind(Actions.BrailleKeyboard.E, 'e');
        Bind(Actions.BrailleKeyboard.F, 'f');
        Bind(Actions.BrailleKeyboard.G, 'g');
        Bind(Actions.BrailleKeyboard.H, 'h');
        Bind(Actions.BrailleKeyboard.I, 'i');
        Bind(Actions.BrailleKeyboard.J, 'j');
        Bind(Actions.BrailleKeyboard.K, 'k');
        Bind(Actions.BrailleKeyboard.L, 'l');
        Bind(Actions.BrailleKeyboard.M, 'm');
        Bind(Actions.BrailleKeyboard.N, 'n');
        Bind(Actions.BrailleKeyboard.O, 'o');
        Bind(Actions.BrailleKeyboard.P, 'p');
        Bind(Actions.BrailleKeyboard.Q, 'q');
        Bind(Actions.BrailleKeyboard.R, 'r');
        Bind(Actions.BrailleKeyboard.S, 's');
        Bind(Actions.BrailleKeyboard.T, 't');
        Bind(Actions.BrailleKeyboard.U, 'u');
        Bind(Actions.BrailleKeyboard.V, 'v');
        Bind(Actions.BrailleKeyboard.W, 'w');
        Bind(Actions.BrailleKeyboard.X, 'x');
        Bind(Actions.BrailleKeyboard.Y, 'y');
        Bind(Actions.BrailleKeyboard.Z, 'z');
    
        Bind(Actions.BrailleKeyboard.Ä, 'ä');
        Bind(Actions.BrailleKeyboard.Ö, 'ö');
        Bind(Actions.BrailleKeyboard.Ü, 'ü');
        Bind(Actions.BrailleKeyboard.ß, 'ß');
    
        Bind(Actions.BrailleKeyboard.D0, '0');
        Bind(Actions.BrailleKeyboard.D1, '1');
        Bind(Actions.BrailleKeyboard.D2, '2');
        Bind(Actions.BrailleKeyboard.D3, '3');
        Bind(Actions.BrailleKeyboard.D4, '4');
        Bind(Actions.BrailleKeyboard.D5, '5');
        Bind(Actions.BrailleKeyboard.D6, '6');
        Bind(Actions.BrailleKeyboard.D7, '7');
        Bind(Actions.BrailleKeyboard.D8, '8');
        Bind(Actions.BrailleKeyboard.D9, '9');
    
        Bind(Actions.BrailleKeyboard.Comma, ',');
        Bind(Actions.BrailleKeyboard.Period, '.');
        Bind(Actions.BrailleKeyboard.Dash, '-');
        Bind(Actions.BrailleKeyboard.Hashtag, '#');
        Bind(Actions.BrailleKeyboard.Space, ' ');
        Bind(Actions.BrailleKeyboard.Exclamation, '!');
        Bind(Actions.BrailleKeyboard.Semicolon, ';');
        Bind(Actions.BrailleKeyboard.Colon, ':');
        Bind(Actions.BrailleKeyboard.Question, '?');
        Bind(Actions.BrailleKeyboard.Apostrophe, '\'');
    
        Actions.BrailleKeyboard.Delete.started += OnDeleteCharacter;
    }

    public override void Disable()
    {
        UnBindAll();
        Actions.BrailleKeyboard.Delete.started -= OnDeleteCharacter;
        
        Actions.BrailleKeyboard.Disable();
    }

    public void OnBasicKeyPress(char key, InputAction.CallbackContext context)
    {
        if (Char.IsDigit(key))
        {
            if (!_typingNumber)
            {
                _typingNumber = true;
                Textbox.AddCharacter("#");
            }
            Textbox.AddCharacter(BrailleConverter.Instance.ConvertNumberToChar(key).ToString());
        }
        else
        {
            _typingNumber = false;
            Textbox.AddCharacter(key.ToString());
        }
    }

    public void OnDeleteCharacter(InputAction.CallbackContext context)
    {
        Textbox.DeleteCharacter();
    }
}
