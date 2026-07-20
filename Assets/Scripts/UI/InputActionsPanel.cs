using System;
using System.Collections.Generic;
using IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class InputActionsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject rebindButtonPrefab;
        [SerializeField] private GameObject scrollRectContent;
        [SerializeField] private ScrollRect scrollRect;
        private readonly List<FocusableRebindOption> _rebindButtons = new List<FocusableRebindOption>();


        private void Start()
        {
            ActionRebinder.Instance.SpecifyInputActionsPanel(this);
        }

        public FocusableRebindOption AddButton(InputAction inputAction,
            Action<InputAction, FocusableRebindOption> action)
        {
            var newButton = Instantiate(rebindButtonPrefab, scrollRectContent.transform)
                .GetComponent<FocusableRebindOption>();
            newButton.SetActionName(inputAction.name);
            newButton.SetBindingText(inputAction.bindings[0].effectivePath);
            newButton.inputAction = inputAction;
            newButton.inputActionsPanel = this;
            _rebindButtons.Add(newButton);
            return newButton;
        }

        /// <summary>
        /// Scrolls in such a way that the selected option is at the top unless there aren't enough options below to facilitate that
        /// </summary>
        /// <param name="targetRectTransform"></param>
        public void ScrollTo(RectTransform targetRectTransform)
        {
            Debug.Log("target pos: " + targetRectTransform.localPosition);
            Debug.Log(scrollRect.viewport.localPosition);
            Debug.Log(scrollRectContent.transform.localPosition);
            
            
            Canvas.ForceUpdateCanvases();
            Vector2 viewportLocalPosition = scrollRect.viewport.localPosition;

            Vector2 targetLocalPosition = targetRectTransform.localPosition;

            Vector2 newTargetLocalPosition = new Vector2(
                0 - (viewportLocalPosition.x + targetLocalPosition.x),
                Math.Min(
                    0 - (viewportLocalPosition.y + targetLocalPosition.y) + (scrollRect.viewport.rect.height / 2) -
                    (targetRectTransform.rect.height * 1.3f),
                    viewportLocalPosition.y + scrollRect.viewport.rect.height - targetRectTransform.rect.height / 2.5f)
            );

            scrollRectContent.transform.localPosition = newTargetLocalPosition;
        }

        public void ScrollToFirst()
        {
            if (_rebindButtons.Count > 0)
            {
                var rectTransform = _rebindButtons[0].GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    ScrollTo(rectTransform);
                    Debug.Log("scrolled to " + _rebindButtons[0].text);
                }
            }
        }

        public void ClearAll()
        {
            foreach (var button in _rebindButtons)
            {
                Destroy(button.gameObject);
            }

            _rebindButtons.Clear();
        }

        private void OnDestroy()
        {
            ActionRebinder.Instance.SpecifyInputActionsPanel(null);
        }
    }
}
