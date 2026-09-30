using System.Collections;
using UnityEngine;
using Utility;

namespace UI
{
    public class FocusableQuestionLetter : Focusable
    {
        private static readonly int Arrive = Animator.StringToHash("Arrive");
        private static readonly int Leave = Animator.StringToHash("Leave");
        [SerializeField] public GameObject wordbox;
        [SerializeField] public Animator animator;
        private const string LetterLayerName = "Letter";
        public int LetterLayerIndex { get; private set; } = -1;

        /// <summary>
        /// Shorthand for SceneControl.Instance.gameUI.layers[LetterLayerIndex]
        /// </summary>
        public UILayer LetterLayer =>
            LetterLayerIndex == -1 ? null : SceneControl.Instance.gameUI.layers[LetterLayerIndex];
        
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => SceneControl.Instance.gameUI != null);
            LetterLayerIndex = SceneControl.Instance.gameUI.AddLayer(new UILayer(LetterLayerName));
            LetterLayer.Add(this);
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
