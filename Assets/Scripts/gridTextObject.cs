using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class gridTextObject : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    private GridLayoutGroup _layoutGroup;
    public string text;
    [FormerlySerializedAs("type")] public AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.BOTH;
    
    
    void Start()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        UpdateSpacing();
        //TODO: subscribe to BrailleSpacing and LineSpacing change event
    }
    void UpdateSpacing()
    {
        _layoutGroup.spacing = new Vector2(GlobalSettings.BrailleSpacing, GlobalSettings.LineSpacing);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
    }
}