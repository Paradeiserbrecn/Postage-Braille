using System;
using UnityEngine;

namespace Utility
{
    public abstract class Focusable : MonoBehaviour
    {
        public string text;
        public abstract void Focus();
        public abstract void Unfocus();
        public abstract void ConfirmAction();
    }
}