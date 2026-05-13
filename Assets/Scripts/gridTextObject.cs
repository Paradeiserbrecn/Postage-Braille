using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GridTextObject : MonoBehaviour, IFocusable
{
    private GridLayoutGroup _layoutGroup;
    public string text;
    [FormerlySerializedAs("type")] public AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.Both;

    void Start()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        UpdateSpacing();
        UpdateCharacterSize();

        //TODO: subscribe to BrailleSpacing and LineSpacing change event

        IOEventManager.BrailleSpacingChanged += UpdateSpacing;
        IOEventManager.LineSpacingChanged += UpdateSpacing;
        IOEventManager.BrailleSizeChanged += UpdateCharacterSize;
    }

    void UpdateSpacing()
    {
        Debug.Log("Trying to update spacing");
        _layoutGroup.spacing = new Vector2(GlobalSettings.BrailleSpacing, GlobalSettings.LineSpacing);
    }

    private void UpdateCharacterSize()
    {
        Vector2 scale = Vector2.one * GlobalSettings.BrailleSize;
        scale.x *= 2; //two dots horizontally
        scale.y *= 3; //three dots vertically
        transform.GetComponent<GridLayoutGroup>().cellSize = scale;
    }

    public void Focus()
    {
       if (text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
        foreach (var brailleObject in GetComponentsInChildren<BrailleObject>())
        {
            brailleObject.HighlightDots();
        }
    }

    public void Unfocus()
    {
        if (text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
        foreach (var brailleObject in GetComponentsInChildren<BrailleObject>())
        {
            brailleObject.UpdateDotColor();
        }
    }

    public void ConfirmAction()
    {
        switch (GameManager.Instance.currentState)
        {
            case GameManager.GameState.WaitingForInput:
                GameManager.Instance.SubmitAnswer(text);
                break;
            default:
                Debug.Log("Confirmed focus when no confirm action was provided");
                break;
        }
    }
}
