using System;
using System.Collections;
using IO;
using Settings;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableQuestionLetter : Focusable
    {
        private static readonly int Arrive = Animator.StringToHash("Arrive");
        private static readonly int Leave = Animator.StringToHash("Leave");
        [SerializeField] public Image image;
        [SerializeField] public GameObject wordbox;
        [SerializeField] public Animator animator;
        private const string LetterLayerName = "Letter";
        public int LetterLayerIndex { get; private set; } = -1;

        /// <summary>
        /// Shorthand for SceneControl.Instance.gameUI.layers[LetterLayerIndex]
        /// </summary>
        public UILayer LetterLayer =>
            LetterLayerIndex == -1 ? null : SceneControl.Instance.gameUI.layers[LetterLayerIndex];

        private void Awake()
        {
            image.color = GlobalSettings.MenuOptionColor;
        }

        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SceneControl.Instance.gameUI != null);
            LetterLayerIndex = SceneControl.Instance.gameUI.AddLayer(new UILayer(LetterLayerName));
            LetterLayer.Add(this);
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

        /// <summary>
        /// Sets the trigger for the letters animator to Leave
        /// </summary>
        public void HideLetter() => animator.SetTrigger(Leave);

        /// <summary>
        /// Sets the trigger for the letters animator to Arrive
        /// </summary>
        public void ShowLetter() => animator.SetTrigger(Arrive);
    }
}
