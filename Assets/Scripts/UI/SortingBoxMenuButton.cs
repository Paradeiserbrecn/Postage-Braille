using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class SortingBoxMenuButton : Focusable
    {
        [SerializeField] public GameObject boxContent;
        [SerializeField] internal Image iconImage;
        public override void ConfirmAction()
        {
            GameManager.Instance.SubmitAnswer(text);
        }
    }
}
