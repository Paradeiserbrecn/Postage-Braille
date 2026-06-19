using System;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableMenuOption : Focusable
    {
        [SerializeField] private TextMeshProUGUI tmpText;
        [SerializeField] private Image border;
        [SerializeField] private Image background;

        public override void Focus()
        {
            border.color = GlobalSettings.HighlightedColor;
        }

        public override void Unfocus()
        {
            border.color = GlobalSettings.MenuOptionColor;
        }

        public override void ConfirmAction()
        {
            Debug.Log("Confirm Action");
        }
    }
}
