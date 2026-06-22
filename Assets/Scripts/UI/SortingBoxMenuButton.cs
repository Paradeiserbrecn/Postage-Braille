using System;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace UI
{
    public class SortingBoxMenuButton : FocusableMenuButton
    {
        [SerializeField] public GameObject boxContent;

        public override void Unfocus()
        {
            image.color = GlobalSettings.SortingBoxColor;
        }

        public override void ConfirmAction()
        {
            GameManager.Instance.SubmitAnswer(text);
        }
    }
}