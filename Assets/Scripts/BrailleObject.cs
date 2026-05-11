using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BrailleObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> dots = new List<GameObject>();
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
        Debug.Log("Trying to update dotSize");
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<RectTransform>().localScale = Vector3.one * GlobalSettings.DotSize;
        }
    }
    public void UpdateDotColor()
    {
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<Image>().color = GlobalSettings.BrailleColor;
        }
    }
    
    public void HighlightDots()
    {
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<Image>().color = GlobalSettings.HighlightedColor;
        }
    }
    
    private void UpdateCharacterSize()
    {
        transform.GetComponent<GridLayoutGroup>().cellSize = Vector2.one * GlobalSettings.BrailleSize;
    }

    //DOES NOT CONVERT only takes bool list from the converter
    public void SetBrailleCharacter(List<bool> braille)
    {
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
}
