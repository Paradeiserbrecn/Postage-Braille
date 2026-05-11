using UnityEngine;
using UnityEngine.UI;

public class WordObject : MonoBehaviour
{
    private HorizontalLayoutGroup layoutGroup;
    void Start()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        //TODO: subscribe to CharacterSpacing change event
    }

    // Update is called once per frame
    void UpdateSpacing()
    {
        layoutGroup.spacing = GlobalSettings.BrailleSpacing;
    }
}
