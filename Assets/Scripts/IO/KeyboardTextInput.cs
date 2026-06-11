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
        Actions.BrailleKeyboard.Enable();
        Actions.BrailleKeyboard.a.started += APressed;
        Actions.BrailleKeyboard.b.started += BPressed;
        Actions.BrailleKeyboard.c.started += CPressed;
        Actions.BrailleKeyboard.d.started += DPressed;
        Actions.BrailleKeyboard.e.started += EPressed;
        Actions.BrailleKeyboard.f.started += FPressed;
        Actions.BrailleKeyboard.g.started += GPressed;
        Actions.BrailleKeyboard.h.started += HPressed;
        Actions.BrailleKeyboard.i.started += IPressed;
        Actions.BrailleKeyboard.j.started += JPressed;
        Actions.BrailleKeyboard.k.started += KPressed;
        Actions.BrailleKeyboard.l.started += LPressed;
        Actions.BrailleKeyboard.m.started += MPressed;
        Actions.BrailleKeyboard.n.started += NPressed;
        Actions.BrailleKeyboard.o.started += OPressed;
        Actions.BrailleKeyboard.p.started += PPressed;
        Actions.BrailleKeyboard.q.started += QPressed;
        Actions.BrailleKeyboard.r.started += RPressed;
        Actions.BrailleKeyboard.s.started += SPressed;
        Actions.BrailleKeyboard.t.started += TPressed;
        Actions.BrailleKeyboard.u.started += UPressed;
        Actions.BrailleKeyboard.v.started += VPressed;
        Actions.BrailleKeyboard.w.started += WPressed;
        Actions.BrailleKeyboard.x.started += XPressed;
        Actions.BrailleKeyboard.y.started += YPressed;
        Actions.BrailleKeyboard.z.started += ZPressed;
        Actions.BrailleKeyboard.ä.started += ÄPressed;
        Actions.BrailleKeyboard.ö.started += ÖPressed;
        Actions.BrailleKeyboard.ü.started += ÜPressed;
        Actions.BrailleKeyboard.ß.started += ßPressed;
        Actions.BrailleKeyboard.d1.started += D1Pressed;
        Actions.BrailleKeyboard.d2.started += D2Pressed;
        Actions.BrailleKeyboard.d3.started += D3Pressed;
        Actions.BrailleKeyboard.d4.started += D4Pressed;
        Actions.BrailleKeyboard.d5.started += D5Pressed;
        Actions.BrailleKeyboard.d6.started += D6Pressed;
        Actions.BrailleKeyboard.d7.started += D7Pressed;
        Actions.BrailleKeyboard.d8.started += D8Pressed;
        Actions.BrailleKeyboard.d9.started += D9Pressed;
        Actions.BrailleKeyboard.d0.started += D0Pressed;
        Actions.BrailleKeyboard.comma.started += CommaPressed;
        Actions.BrailleKeyboard.period.started += PeriodPressed;
        Actions.BrailleKeyboard.dash.started += DashPressed;
        Actions.BrailleKeyboard.hashtag.started += HashtagPressed;
        Actions.BrailleKeyboard.space.started += SpacePressed;
        Actions.BrailleKeyboard.delete.started += OnDeleteCharacter;
        Actions.BrailleKeyboard.shift.started += OnShiftPrss;
        Actions.BrailleKeyboard.shift.canceled += OnShiftRelease;
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
        Actions.BrailleKeyboard.Disable();
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
