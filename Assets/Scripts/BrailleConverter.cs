using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BrailleConverter : MonoBehaviour
{
    public  static BrailleConverter Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public GameObject ConvertCharacterToBraille(string s)
    {
        var textMeshProUGUI = gameObject.GetOrAddComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = s;
        return textMeshProUGUI.gameObject;
    }
}
