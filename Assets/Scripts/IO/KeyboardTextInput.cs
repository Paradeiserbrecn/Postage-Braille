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
    
    private readonly Dictionary<char,char> _shiftChars = new Dictionary<char,char>
    {
        {'1', '!'},
        {'2', '"'},
        {',', ';'},
        {'.', ':'},
        {'ß', '?'},
        {'8', '('},
        {'9', ')'},
        {'#', '\''}
    };
    public override void Enable()
    {
        _actions.BrailleKeyboard.Enable();
        _actions.BrailleKeyboard.a.started += APressed;
        _actions.BrailleKeyboard.b.started += BPressed;
        _actions.BrailleKeyboard.c.started += CPressed;
        _actions.BrailleKeyboard.d.started += DPressed;
        _actions.BrailleKeyboard.e.started += EPressed;
        _actions.BrailleKeyboard.f.started += FPressed;
        _actions.BrailleKeyboard.g.started += GPressed;
        _actions.BrailleKeyboard.h.started += HPressed;
        _actions.BrailleKeyboard.i.started += IPressed;
        _actions.BrailleKeyboard.j.started += JPressed;
        _actions.BrailleKeyboard.k.started += KPressed;
        _actions.BrailleKeyboard.l.started += LPressed;
        _actions.BrailleKeyboard.m.started += MPressed;
        _actions.BrailleKeyboard.n.started += NPressed;
        _actions.BrailleKeyboard.o.started += OPressed;
        _actions.BrailleKeyboard.p.started += PPressed;
        _actions.BrailleKeyboard.q.started += QPressed;
        _actions.BrailleKeyboard.r.started += RPressed;
        _actions.BrailleKeyboard.s.started += SPressed;
        _actions.BrailleKeyboard.t.started += TPressed;
        _actions.BrailleKeyboard.u.started += UPressed;
        _actions.BrailleKeyboard.v.started += VPressed;
        _actions.BrailleKeyboard.w.started += WPressed;
        _actions.BrailleKeyboard.x.started += XPressed;
        _actions.BrailleKeyboard.y.started += YPressed;
        _actions.BrailleKeyboard.z.started += ZPressed;
        _actions.BrailleKeyboard.ä.started += ÄPressed;
        _actions.BrailleKeyboard.ö.started += ÖPressed;
        _actions.BrailleKeyboard.ü.started += ÜPressed;
        _actions.BrailleKeyboard.ß.started += ßPressed;
        _actions.BrailleKeyboard.d1.started += D1Pressed;
        _actions.BrailleKeyboard.d2.started += D2Pressed;
        _actions.BrailleKeyboard.d3.started += D3Pressed;
        _actions.BrailleKeyboard.d4.started += D4Pressed;
        _actions.BrailleKeyboard.d5.started += D5Pressed;
        _actions.BrailleKeyboard.d6.started += D6Pressed;
        _actions.BrailleKeyboard.d7.started += D7Pressed;
        _actions.BrailleKeyboard.d8.started += D8Pressed;
        _actions.BrailleKeyboard.d9.started += D9Pressed;
        _actions.BrailleKeyboard.d0.started += D0Pressed;
        _actions.BrailleKeyboard.comma.started += CommaPressed;
        _actions.BrailleKeyboard.period.started += PeriodPressed;
        _actions.BrailleKeyboard.dash.started += DashPressed;
        _actions.BrailleKeyboard.hashtag.started += HashtagPressed;
        _actions.BrailleKeyboard.space.started += SpacePressed;
        _actions.BrailleKeyboard.delete.started += OnDeleteCharacter;
        _actions.BrailleKeyboard.shift.started += OnShiftPrss;
        _actions.BrailleKeyboard.shift.canceled += OnShiftRelease;
        Debug.Log("Keyboard text input");
    }
    
    
    #region Functions for each Keypress
    public void APressed(InputAction.CallbackContext context){OnBasicKeyPress('a' , context);}
    public void BPressed(InputAction.CallbackContext context){OnBasicKeyPress('b' , context);}
    public void CPressed(InputAction.CallbackContext context){OnBasicKeyPress('c' , context);}
    public void DPressed(InputAction.CallbackContext context){OnBasicKeyPress('d' , context);}
    public void EPressed(InputAction.CallbackContext context){OnBasicKeyPress('e' , context);}
    public void FPressed(InputAction.CallbackContext context){OnBasicKeyPress('f' , context);}
    public void GPressed(InputAction.CallbackContext context){OnBasicKeyPress('g' , context);}
    public void HPressed(InputAction.CallbackContext context){OnBasicKeyPress('h' , context);}
    public void IPressed(InputAction.CallbackContext context){OnBasicKeyPress('i' , context);}
    public void JPressed(InputAction.CallbackContext context){OnBasicKeyPress('j' , context);}
    public void KPressed(InputAction.CallbackContext context){OnBasicKeyPress('k' , context);}
    public void LPressed(InputAction.CallbackContext context){OnBasicKeyPress('l' , context);}
    public void MPressed(InputAction.CallbackContext context){OnBasicKeyPress('m' , context);}
    public void NPressed(InputAction.CallbackContext context){OnBasicKeyPress('n' , context);}
    public void OPressed(InputAction.CallbackContext context){OnBasicKeyPress('o' , context);}
    public void PPressed(InputAction.CallbackContext context){OnBasicKeyPress('p' , context);}
    public void QPressed(InputAction.CallbackContext context){OnBasicKeyPress('q' , context);}
    public void RPressed(InputAction.CallbackContext context){OnBasicKeyPress('r' , context);}
    public void SPressed(InputAction.CallbackContext context){OnBasicKeyPress('s' , context);}
    public void TPressed(InputAction.CallbackContext context){OnBasicKeyPress('t' , context);}
    public void UPressed(InputAction.CallbackContext context){OnBasicKeyPress('u' , context);}
    public void VPressed(InputAction.CallbackContext context){OnBasicKeyPress('v' , context);}
    public void WPressed(InputAction.CallbackContext context){OnBasicKeyPress('w' , context);}
    public void XPressed(InputAction.CallbackContext context){OnBasicKeyPress('x' , context);}
    public void YPressed(InputAction.CallbackContext context){OnBasicKeyPress('y' , context);}
    public void ZPressed(InputAction.CallbackContext context){OnBasicKeyPress('z' , context);}
    public void ÄPressed(InputAction.CallbackContext context){OnBasicKeyPress('ä' , context);}
    public void ÖPressed(InputAction.CallbackContext context){OnBasicKeyPress('ö' , context);}
    public void ÜPressed(InputAction.CallbackContext context){OnBasicKeyPress('ü' , context);}
    public void ßPressed(InputAction.CallbackContext context){OnBasicKeyPress('ß' , context);}
    public void CommaPressed(InputAction.CallbackContext context){OnBasicKeyPress(',' , context);}
    public void PeriodPressed(InputAction.CallbackContext context){OnBasicKeyPress('.' , context);}
    public void DashPressed(InputAction.CallbackContext context){OnBasicKeyPress('-' , context);}
    public void HashtagPressed(InputAction.CallbackContext context){OnBasicKeyPress('#' , context);}
    public void SpacePressed(InputAction.CallbackContext context){OnBasicKeyPress(' ' , context);}
    public void D1Pressed(InputAction.CallbackContext context){OnBasicKeyPress('1', context);}
    public void D2Pressed(InputAction.CallbackContext context){OnBasicKeyPress('2', context);}
    public void D3Pressed(InputAction.CallbackContext context){OnBasicKeyPress('3', context);}
    public void D4Pressed(InputAction.CallbackContext context){OnBasicKeyPress('4', context);}
    public void D5Pressed(InputAction.CallbackContext context){OnBasicKeyPress('5', context);}
    public void D6Pressed(InputAction.CallbackContext context){OnBasicKeyPress('6', context);}
    public void D7Pressed(InputAction.CallbackContext context){OnBasicKeyPress('7', context);}
    public void D8Pressed(InputAction.CallbackContext context){OnBasicKeyPress('8', context);}
    public void D9Pressed(InputAction.CallbackContext context){OnBasicKeyPress('9', context);}
    public void D0Pressed(InputAction.CallbackContext context){OnBasicKeyPress('0', context);}
    #endregion

    public override void Disable()
    {
        _actions.BrailleKeyboard.Disable();
    }

    public void OnShiftPrss(InputAction.CallbackContext context)
    {
        _shiftActive = true;
    }

    public void OnShiftRelease(InputAction.CallbackContext context)
    {
        _shiftActive = false;
    }

    public char Quote(InputAction.CallbackContext context)
    {
        if (Textbox.BrailleObjects[^2] == null ||
            Textbox.BrailleObjects[^2].DotBools.SequenceEqual(Textbox.EmptyBrailleList))
        {
            return '„';
        }
        return '“';
    }

    public void OnBasicKeyPress(char key, InputAction.CallbackContext context)
    {
        Debug.Log(key.ToString());
        if (_shiftActive && _shiftChars.TryGetValue(key, out char c))
        {
            key = c;
        }

        if (Char.IsDigit(key))
        {
            if (!_typingNumber)
            {
                _typingNumber = true;
                Textbox.AddCharacter("#");
            }
            Textbox.AddCharacter(GridBrailleConverter.Instance.ConvertNumberToChar(key).ToString());
        }
        else
        {
            _typingNumber = false;
            
            if (key == '"')
            {
                key = Quote(context);
            }
            
            Textbox.AddCharacter(key.ToString());
            
        }
        Debug.Log(Textbox.text);
    }

    public void OnDeleteCharacter(InputAction.CallbackContext context)
    {
        Textbox.DeleteCharacter();
        Debug.Log(Textbox.text);
    }
}
