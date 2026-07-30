using TMPro;
using UnityEngine;

namespace UI
{
    public class SelectedActionMapDisplay: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _actionMapName;

        public void SetActionMapName(string text)
        {
            _actionMapName.text = text;
        }
    }
}
