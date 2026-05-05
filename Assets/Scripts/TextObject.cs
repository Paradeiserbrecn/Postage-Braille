using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TextObject : MonoBehaviour, IPointerEnterHandler
{
    private HorizontalLayoutGroup _layoutGroup;
    public string text;
    [FormerlySerializedAs("type")] public AssistiveOutput.OutputType outputType = AssistiveOutput.OutputType.BOTH;
    private Button _button;
    
    
    void Start()
    {
        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
        UpdateSpacing();
        _button = GetComponent<Button>();
        //TODO: subscribe to WordSpacing change event
    }

    void UpdateSpacing()
    {
        _layoutGroup.spacing = GlobalSettings.WordSpacing;
        Debug.Log("word spacing is: " + GlobalSettings.WordSpacing);
    }

    public void ButtonTest()
    {
        if(text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(text != null) IOEventManager.InvokeAssistiveOutput(text, outputType);
    }
}