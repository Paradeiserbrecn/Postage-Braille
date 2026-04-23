using UnityEngine;
using UnityEngine.UI;

public class TextObject : MonoBehaviour
{
    private HorizontalLayoutGroup layoutGroup;
    void Start()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        UpdateSpacing();
        //TODO: subscribe to WordSpacing change event
    }

    // Update is called once per frame
    void UpdateSpacing()
    {
        layoutGroup.spacing = GlobalSettings.WordSpacing;
        Debug.Log("word spacing is: " + GlobalSettings.WordSpacing);
    }
}