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
        var obj = Instantiate(UIManager.Instance.questionTextPrefab);
        var text = obj.GetComponent<TextMeshProUGUI>();
        text.text = "BRAILLE: " + s;

        return obj;
    }
}
