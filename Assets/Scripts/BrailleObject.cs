using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BrailleObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> dots = new List<GameObject>();
    private List<bool> _dotBools = new List<bool> {false, false, false, false, false, false};
    void Start()
    {
        UpdateDotSize();
        UpdateDotColor();
        UpdateCharacterSize();

        IOEventManager.BrailleSizeChanged += UpdateCharacterSize;
        IOEventManager.DotSizeChanged += UpdateDotSize;
        IOEventManager.BrailleColorChanged += UpdateDotColor;
    }
    private void UpdateDotSize()
    {
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<RectTransform>().localScale = Vector3.one * GlobalSettings.DotSize;
        }
    }
    private void UpdateDotColor()
    {
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<Image>().color = GlobalSettings.BrailleColor;
        }
    }
    
    private void UpdateCharacterSize()
    {
        transform.GetComponent<GridLayoutGroup>().cellSize = Vector2.one * GlobalSettings.BrailleSize;
    }

    //DOES NOT CONVERT only takes bool list from the converter
    public void SetBrailleCharacter(List<bool> braille)
    {
        _dotBools = braille;
        if (braille.Count != dots.Count)
        {
            Debug.Log("Braille list size mismatch. Braille: " + braille.Count + " Dot: " + dots.Count);
            return;
        }
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].GetComponent<Image>().enabled = braille[i];
            //Debug.Log("dot "+ i + " is " + braille[i]);
        }
    }
    
    public List<bool> DotBools => _dotBools;
}
