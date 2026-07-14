using IO;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableQuestionLetter : Focusable
    {
        [SerializeField] public Image image;
        [SerializeField] public GameObject wordbox;

        private void Awake()
        {
            image.color = GlobalSettings.MenuOptionColor;
        }

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, GlobalSettings.questionOutputType);
            image.color = GlobalSettings.HighlightedColor;
        }

        public override void Unfocus()
        {
            image.color = GlobalSettings.MenuOptionColor;
        }

        public override void ConfirmAction()
        {
        }

    }
}