using System.Collections.Generic;
using IO;
using Settings;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace Braille
{
    public class BrailleObject : MonoBehaviour
    {
        [SerializeField] private List<GameObject> dots = new();
        private readonly List<Image> _dotImages = new();
        private readonly List<RectTransform> _dotRects = new();
        private GridLayoutGroup _gridLayoutGroup;

        public List<bool> DotBools { get; private set; } = new() { false, false, false, false, false, false };

        private void Start()
        {
            foreach (GameObject dot in dots)
            {
                _dotRects.Add(dot.GetComponent<RectTransform>());
                _dotImages.Add(dot.GetComponent<Image>());
            }

            _gridLayoutGroup = transform.GetComponent<GridLayoutGroup>();

            UpdateDotSize();
            UpdateDotColor();
            UpdateCharacterSize();

            IOEventManager.BrailleSizeChanged += UpdateCharacterSize;
            IOEventManager.DotSizeChanged += UpdateDotSize;
            IOEventManager.BrailleColorChanged += UpdateDotColor;
        }

        private void UpdateDotSize()
        {
            foreach (RectTransform rect in _dotRects)
            {
                rect.localScale = Vector3.one * GlobalSettings.DotSize;
            }
        }

        /// <summary>
        /// Sets the Dot Color to the default dot color specified in GlobalSettings.BrailleColor
        /// </summary>
        public void UpdateDotColor()
        {
            foreach (var image in _dotImages)
            {
                image.color = GlobalSettings.BrailleColor;
            }
        }

        /// <summary>
        /// Sets the Dot Color to the highlighted dot color specified in GlobalSettings.HighlightedColor
        /// </summary>
        public void HighlightDots()
        {
            foreach (var image in _dotImages)
            {
                image.color = GlobalSettings.HighlightedColor;
            }
        }

        private void UpdateCharacterSize()
        {
            _gridLayoutGroup.cellSize = Vector2.one * GlobalSettings.BrailleSize;
        }

        //DOES NOT CONVERT only takes bool list from the converter
        public void SetBrailleCharacter(List<bool> braille)
        {
            DotBools = braille;
            if (braille.Count != dots.Count)
            {
                Debug.LogWarning("Braille list size mismatch. Braille: " + braille.Count + " Dot: " + dots.Count);
                return;
            }

            for (int i = 0; i < dots.Count; i++)
            {
                dots[i].GetComponent<Image>().enabled = braille[i];
            }
        }
    }
}