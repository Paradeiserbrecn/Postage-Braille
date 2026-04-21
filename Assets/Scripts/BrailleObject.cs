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
        GetComponent<RectTransform>().localScale = Vector3.one * GlobalVisualProfile.BrailleSize;
    }

    private void UpdateDotSize()
    {
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<RectTransform>().localScale = Vector3.one * GlobalVisualProfile.DotSize;
        }
    }

    private void UpdateDotColor()
    {
        foreach (GameObject dot in dots)
        {
            dot.GetComponent<Image>().color = GlobalVisualProfile.BrailleColor;
        }
    }

    public void setBrailleCharacter(List<bool> braille)
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
