using System;
using IO;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class SortingBoxMenuButton : Focusable
    {
        [SerializeField] public GameObject boxContent;
        [SerializeField] internal Image image;
        [SerializeField] internal Image iconImage;

        public override void Unfocus()
        {
            image.color = GlobalSettings.SortingBoxColor;
        }

        public override void ConfirmAction()
        {
            GameManager.Instance.SubmitAnswer(text);
        }
        

        private void Awake()
        {
            image.color = GlobalSettings.MenuOptionColor;
        }

        public override void Focus()
        {
            if (text != null) IOEventManager.InvokeAssistiveOutput(text, GlobalSettings.standardOutputType);
            image.color = GlobalSettings.HighlightedColor;
        }

    }
}