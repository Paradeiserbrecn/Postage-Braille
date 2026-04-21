using UnityEngine;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;

public class BrailleObject : MonoBehaviour
{
    [SerializeField] private List<GameObject> dots = new List<GameObject>();
    void Start()
    {
        UpdateBrailleSize();
        UpdateDotSize();
        UpdateDotColor();
    }
    private void UpdateBrailleSize()
    {
        GetComponent<RectTransform>().localScale = Vector3.one * GlobalSettings.BrailleSize;
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

    //DOES NOT COVERT only takes bool list from the converter
    public void SetBrailleCharacter(List<bool> braille)
    {
        if (braille.Count != dots.Count)
        {
            Debug.Log("Braille list size mismatch");
            return;
        }
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].SetActive(braille[i]);
        }
    }
}
