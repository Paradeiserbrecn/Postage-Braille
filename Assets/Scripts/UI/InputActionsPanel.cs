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
        public void ScrollTo(RectTransform target)
        {
            Debug.Log("scroll");
            Canvas.ForceUpdateCanvases();

            RectTransform content = scrollRect.content;
            RectTransform viewport = scrollRect.viewport;

            float viewportHeight = viewport.rect.height;
            float contentHeight = content.rect.height;

            float desiredY = -target.localPosition.y 
                             - (1 - target.pivot.y) * target.rect.height;

            float maxOffset = Mathf.Max(0, contentHeight - viewportHeight) / 2f;
            desiredY = Mathf.Clamp(desiredY, -maxOffset, maxOffset);

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
