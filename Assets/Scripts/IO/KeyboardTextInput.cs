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
    public override void Enable()
    {
        Actions.BrailleKeyboard.Enable();
        Actions.BrailleKeyboard.A.started += APressed;
        Actions.BrailleKeyboard.B.started += BPressed;
        Actions.BrailleKeyboard.C.started += CPressed;
        Actions.BrailleKeyboard.D.started += DPressed;
        Actions.BrailleKeyboard.E.started += EPressed;
        Actions.BrailleKeyboard.F.started += FPressed;
        Actions.BrailleKeyboard.G.started += GPressed;
        Actions.BrailleKeyboard.H.started += HPressed;
        Actions.BrailleKeyboard.I.started += IPressed;
        Actions.BrailleKeyboard.J.started += JPressed;
        Actions.BrailleKeyboard.K.started += KPressed;
        Actions.BrailleKeyboard.L.started += LPressed;
        Actions.BrailleKeyboard.M.started += MPressed;
        Actions.BrailleKeyboard.N.started += NPressed;
        Actions.BrailleKeyboard.O.started += OPressed;
        Actions.BrailleKeyboard.P.started += PPressed;
        Actions.BrailleKeyboard.Q.started += QPressed;
        Actions.BrailleKeyboard.R.started += RPressed;
        Actions.BrailleKeyboard.S.started += SPressed;
        Actions.BrailleKeyboard.T.started += TPressed;
        Actions.BrailleKeyboard.U.started += UPressed;
        Actions.BrailleKeyboard.V.started += VPressed;
        Actions.BrailleKeyboard.W.started += WPressed;
        Actions.BrailleKeyboard.X.started += XPressed;
        Actions.BrailleKeyboard.Y.started += YPressed;
        Actions.BrailleKeyboard.Z.started += ZPressed;
        Actions.BrailleKeyboard.Ä.started += ÄPressed;
        Actions.BrailleKeyboard.Ö.started += ÖPressed;
        Actions.BrailleKeyboard.Ü.started += ÜPressed;
        Actions.BrailleKeyboard.ß.started += ßPressed;
        Actions.BrailleKeyboard.D1.started += D1Pressed;
        Actions.BrailleKeyboard.D2.started += D2Pressed;
        Actions.BrailleKeyboard.D3.started += D3Pressed;
        Actions.BrailleKeyboard.D4.started += D4Pressed;
        Actions.BrailleKeyboard.D5.started += D5Pressed;
        Actions.BrailleKeyboard.D6.started += D6Pressed;
        Actions.BrailleKeyboard.D7.started += D7Pressed;
        Actions.BrailleKeyboard.D8.started += D8Pressed;
        Actions.BrailleKeyboard.D9.started += D9Pressed;
        Actions.BrailleKeyboard.D0.started += D0Pressed;
        Actions.BrailleKeyboard.Comma.started += CommaPressed;
        Actions.BrailleKeyboard.Period.started += PeriodPressed;
        Actions.BrailleKeyboard.Dash.started += DashPressed;
        Actions.BrailleKeyboard.Hashtag.started += HashtagPressed;
        Actions.BrailleKeyboard.Space.started += SpacePressed;
        Actions.BrailleKeyboard.Delete.started += OnDeleteCharacter;
        Actions.BrailleKeyboard.Exclamation.started += ExclamationPressed;
        Actions.BrailleKeyboard.Quote.started += QuotePressed;
        Actions.BrailleKeyboard.Semicolon.started += SemicolonPressed;
        Actions.BrailleKeyboard.Colon.started += ColonPressed;
        Actions.BrailleKeyboard.Question.started += QuestionPressed;
        Actions.BrailleKeyboard.LBrace.started += LBracePressed;
        Actions.BrailleKeyboard.RBrace.started += RBracePressed;
        Actions.BrailleKeyboard.Apostrophe.started += ApostrophePressed;
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
    public void ExclamationPressed(InputAction.CallbackContext context){OnBasicKeyPress('!', context);}
    public void QuotePressed(InputAction.CallbackContext context){OnBasicKeyPress('"', context);}
    public void SemicolonPressed(InputAction.CallbackContext context){OnBasicKeyPress(';', context);}
    public void ColonPressed(InputAction.CallbackContext context){OnBasicKeyPress(':', context);}
    public void QuestionPressed(InputAction.CallbackContext context){OnBasicKeyPress('?', context);}
    public void LBracePressed(InputAction.CallbackContext context){OnBasicKeyPress('(', context);}
    public void RBracePressed(InputAction.CallbackContext context){OnBasicKeyPress(')', context);}
    public void ApostrophePressed(InputAction.CallbackContext context){OnBasicKeyPress('\'', context);}
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

    public char Quote(InputAction.CallbackContext context)
    {
        if (Textbox.BrailleObjects.Count < 2 ||
            Textbox.BrailleObjects[^2].DotBools.SequenceEqual(Textbox.EmptyBrailleList))
        {
            return '„';
        }
        return '“';
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
    }

    public void OnDeleteCharacter(InputAction.CallbackContext context)
    {
        Textbox.DeleteCharacter();
        Debug.Log(Textbox.text);
    }
}
