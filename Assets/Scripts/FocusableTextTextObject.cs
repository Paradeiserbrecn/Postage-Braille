using System;
using TMPro;
using UnityEngine;

public class FocusableTextTextObject : IFocusableText
{
    public AssistiveOutput.OutputType OutputType = AssistiveOutput.OutputType.BOTH;
    public readonly TextMeshProUGUI TMPText;

    private bool focused = false;
    private string _text;
    private string _displayTextOverride;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            _displayTextOverride = null;
            RefreshDisplayedText();
        }
    }

    /// <summary>
    /// Overrides the displayed text.
    /// Set to null to display <see cref="Text"/> again.
    /// </summary>
    public string DisplayText
    {
        get => _displayTextOverride ?? _text;
        set
        {
            _displayTextOverride = value;
            RefreshDisplayedText();
        }
    }

    private void RefreshDisplayedText()
    {
        TMPText.text = _displayTextOverride ?? _text;
    }

    public FocusableTextTextObject(TextMeshProUGUI tmpText)
    {
        TMPText = tmpText;
    }

    /// <summary>
    /// Highlights the object and sends assistive output with the specified OutputType
    /// </summary>
    public void Focus()
    {
        focused = true;
        if (TMPText.text != null) IOEventManager.InvokeAssistiveOutput(TMPText.text, OutputType);
        TMPText.color = GlobalSettings.HighlightedColor;
    }

    public void Unfocus()
    {
        focused = false;
        if (TMPText.text != null) IOEventManager.InvokeAssistiveOutput(TMPText.text, OutputType);
        TMPText.color = GlobalSettings.TextColor;
    }

    public void ConfirmAction()
    {
        if (!focused) throw new Exception("Tried to Execute Focus action on unfocused object");
        switch (GameManager.Instance.currentState)
        {
            case GameManager.GameState.WaitingForInput:
                GameManager.Instance.SubmitAnswer(Text);
                break;
            default:
                Debug.Log("Confirmed focus when no confirm action was provided");
                break;
        }
    }
}