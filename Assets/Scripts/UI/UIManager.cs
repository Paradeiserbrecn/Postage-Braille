using System;
using System.Collections.Generic;
using Braille;
using TMPro;
using UnityEngine;
using Utility;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private string UIName;
        public List<UILayer> layers = new();


        private int _currentLayerIdx;
        private int _currentOptionIndex;

        /// <summary>
        /// The focused option in the currently selected layer
        /// </summary>
        public Focusable CurrentlyFocusedOption => CurrentLayer?.Current;

        /// <summary>
        /// The currently selected layer. Switching between different layers is possibe via .SwitchLayer()
        /// </summary>
        public UILayer CurrentLayer => layers.Count == 0 ? null : layers[_currentLayerIdx];

        /// <summary>
        /// The Focusables inside the currently selected layer.
        /// </summary>
        public List<Focusable> CurrentFocusables => layers.Count == 0
            ? null
            : layers[_currentLayerIdx].Focusables;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Highlights the next available option in the current layer.
        /// </summary>
        /// <returns>The newly focused option, or null if no option could be focused.</returns>
        public Focusable HighlightNextOption() => HighlightNextFocusable();

        /// <summary>
        /// Highlights the previous available option in the current layer.
        /// </summary>
        /// <returns>The newly focused option, or null if no option could be focused.</returns>
        public Focusable HighlightPreviousOption() => HighlightPreviousFocusable();

        /// <summary>
        /// Moves focus to the next focusable element in the current layer.
        /// </summary>
        /// <returns>The newly focused element.</returns>
        private Focusable HighlightNextFocusable()
        {
            return CurrentLayer?.FocusNext();
        }

        /// <summary>
        /// Moves focus to the previous focusable element in the current layer.
        /// </summary>
        /// <returns>The newly focused element.</returns>
        private Focusable HighlightPreviousFocusable()
        {
            return CurrentLayer?.FocusPrevious();
        }

        /// <summary>
        /// Destroys all child GameObjects of the specified transform and detaches them.
        /// </summary>
        /// <param name="parent">The transform whose children should be removed.</param>
        public void ClearChildren(Transform parent)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }

            parent.DetachChildren();
        }

        /// <summary>
        /// Switches focus to the next UI layer and focuses its first element.
        /// </summary>
        public void SwitchLayer()
        {
            CurrentLayer?.Unfocus();
            _currentLayerIdx = (_currentLayerIdx + 1) % layers.Count;
            CurrentLayer?.FocusFirst();
        }

        /// <summary>
        /// Switches focus to a layer with the specified index.
        /// <param name="layerIdx">The index of the layer to switch to</param>
        /// <remarks>If the requested layer is already CurrentLayer, it does nothing</remarks>
        /// </summary>
        public void SwitchLayer(int layerIdx)
        {
            if (layerIdx < 0 || layerIdx >= layers.Count)
            {
                Debug.LogWarning("Tried switching to layer out of range");
                return;
            }

            if (layerIdx == _currentLayerIdx) return;

            CurrentLayer?.Unfocus();
            _currentLayerIdx = layerIdx;
            CurrentLayer?.FocusFirst();
        }

        /// <summary>
        /// Creates a new UI layer and optionally transfers focus to it.
        /// </summary>
        /// <param name="layerName">The name assigned to the new layer.</param>
        /// <param name="focusables">
        /// The collection of focusable elements contained in the layer.
        /// </param>
        /// <param name="focusImmediately">
        /// If true, the current layer is unfocused and the first element in the
        /// newly added layer is focused.
        /// If the focusables is empty, focus is not changed
        /// </param>
        /// <returns>
        /// The index of the newly added layer, or -1 if the layer could not be created.
        /// </returns>
        public int AddLayer(string layerName, List<Focusable> focusables, bool focusImmediately = true)
        {
            if (string.IsNullOrWhiteSpace(layerName))
            {
                Debug.LogWarning("Layer name cannot be empty.");
                return -1;
            }

            if (focusables == null)
            {
                Debug.LogWarning("Tried to add a null focusables List to UIManager. Returning -1");
                return -1;
            }

            layers.Add(new UILayer(layerName, focusables));
            var newLayerIdx = layers.Count - 1;

            if (focusImmediately && focusables.Count > 0)
            {
                CurrentLayer?.Unfocus();
                _currentLayerIdx = newLayerIdx;
                CurrentLayer?.FocusFirst();
            }

            return newLayerIdx;
        }


        /// <summary>
        /// Creates a new UI layer and optionally transfers focus to it.
        /// </summary>
        /// <param name="layer">The layer to add to the UIManager</param>
        /// <param name="focusImmediately">
        /// If true, the current layer is unfocused and the first element in the
        /// newly added layer is focused.
        /// If the focusables is empty, focus is not changed
        /// </param>
        /// <returns>
        /// The index of the newly added layer, or -1 if the layer could not be created.
        /// </returns>
        public int AddLayer(UILayer layer, bool focusImmediately = true)
        {
            if (layer == null)
            {
                Debug.LogWarning("Tried to add a null layer to UIManager. Returning -1");
                return -1;
            }

            layers.Add(layer);

            var newLayerIdx = layers.Count - 1;


            if (focusImmediately && layer.Focusables.Count > 0)
            {
                CurrentLayer?.Unfocus();
                _currentLayerIdx = newLayerIdx;
                CurrentLayer?.FocusFirst();
            }

            return newLayerIdx;
        }

        /// <summary>
        /// Removes the specified UI layer.
        /// </summary>
        /// <param name="layerIdx">
        /// The index of the layer to remove.
        /// </param>
        /// <remarks>
        /// The question layer cannot be removed. If the removed layer is currently
        /// focused, focus is returned to the question layer.
        /// </remarks>
        public void RemoveLayer(int layerIdx)
        {
            if (layerIdx < 0 || layerIdx >= layers.Count)
            {
                Debug.LogWarning("Index out of range when trying to remove a layer: " + layerIdx);
                return;
            }

            if (layerIdx < _currentLayerIdx)
            {
                _currentLayerIdx--;
            }

            if (layerIdx == _currentLayerIdx)
            {
                CurrentLayer?.Unfocus();
                _currentLayerIdx = 0;
                CurrentLayer?.FocusFirst();
            }

            layers.RemoveAt(layerIdx);
        }
    }
}
