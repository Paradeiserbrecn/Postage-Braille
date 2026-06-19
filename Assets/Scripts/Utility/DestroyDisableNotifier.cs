using System;
using UnityEngine;

namespace Utility
{
    public class DestroyDisableNotifier : MonoBehaviour
    {
        public event Action Destroyed, Disabled;

        private void OnDestroy()
        {
            Destroyed?.Invoke();
        }

        private void OnDisable()
        {
            Disabled?.Invoke();
        }
    }
}
