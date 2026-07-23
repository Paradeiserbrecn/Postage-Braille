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
        private const float TopOffset = 210f;


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
        public void ScrollTo(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();

            RectTransform content = scrollRect.content;
            RectTransform viewport = scrollRect.viewport;

            float viewportHeight = viewport.rect.height;
            float contentHeight = content.rect.height;

            // Position of the target from the top of the content
            float targetTop = -target.anchoredPosition.y;

            // Desired content position so target is at the top
            float desiredY = targetTop - TopOffset;

            // Clamp so we don't scroll past the bottom
            desiredY = Mathf.Clamp(desiredY, 0, contentHeight - viewportHeight);

            Vector2 pos = content.anchoredPosition;
            pos.y = desiredY;
            content.anchoredPosition = pos;
        }
        
        public void ScrollToTop()
        {
            if (scrollRect.content.childCount == 0)
                return;

            ScrollTo(scrollRect.content.GetChild(0) as RectTransform);
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
