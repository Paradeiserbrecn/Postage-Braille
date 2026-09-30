using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class FocusableMenuButton : Focusable
    {
        [SerializeField] internal Image image;
        [SerializeField] internal Image iconImage;
        [SerializeField] internal UnityEvent action;
        public override void ConfirmAction()
        {
            action.Invoke();
        }
    }
}
