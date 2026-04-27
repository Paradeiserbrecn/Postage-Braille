using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TextObject : MonoBehaviour
{
    private HorizontalLayoutGroup layoutGroup;
    public string text;
    private Button _button;
    
    
    void Start()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        UpdateSpacing();
        _button = GetComponent<Button>();
        //TODO: subscribe to WordSpacing change event
    }

    void UpdateSpacing()
    {
        layoutGroup.spacing = GlobalSettings.WordSpacing;
        Debug.Log("word spacing is: " + GlobalSettings.WordSpacing);
    }

    public void ButtonTest()
    {
        if(text != null) IOEventManager.InvokeAssistiveOutput(text, AssistiveOutput.OutputType.SPEAK);
    }
}