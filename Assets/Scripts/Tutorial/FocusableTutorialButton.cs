using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;

namespace Tutorial
{
    public class FocusableTutorialButton : Focusable
    {
        [SerializeField] internal Image image;
        [SerializeField] internal Image iconImage;
        [SerializeField] internal UnityEvent action;
        [SerializeField] internal UnityEvent focusAction;
        
        public override void ConfirmAction()
        {
            action.Invoke();
        }

        public override void Focus()
        {
            border.enabled = true;
            focusAction.Invoke();
        }
    }
}
