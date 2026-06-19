using System;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI
{
    /// <summary>
    /// Represents a navigable collection of focusable UI elements.
    /// Handles focus management, navigation, and confirmation actions
    /// within a single UI layer.
    /// </summary>
    [Serializable]
    public class UILayer
    {
        [SerializeField] private string name;
        [SerializeField] private List<Focusable> focusables = new();

        public string Name => name;

        /// <summary>
        /// Gets the collection of focusable elements contained in this layer.
        /// </summary>
        public List<Focusable> Focusables => focusables;

        /// <summary>
        /// Gets the index of the currently focused element.
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        /// Gets the currently focused element, or null if the layer contains no elements.
        /// </summary>
        public Focusable Current =>
            Focusables.Count == 0
                ? null
                : Focusables[CurrentIndex];

        /// <summary>
        /// Creates a new UI layer with the specified name.
        /// </summary>
        /// <param name="name">The name of the layer.</param>
        public UILayer(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Creates a new UI layer with the specified name and focusable elements.
        /// </summary>
        /// <param name="name">The name of the layer.</param>
        /// <param name="focusables">The initial collection of focusable elements.</param>
        public UILayer(string name, List<Focusable> focusables)
        {
            this.name = name;
            this.focusables = focusables;
        }

        /// <summary>
        /// Adds a focusable element to the layer.
        /// </summary>
        /// <param name="focusable">The element to add.</param>
        public void Add(Focusable focusable)
        {
            Focusables.Add(focusable);
        }

        /// <summary>
        /// Removes all focusable elements from the layer and resets focus state.
        /// </summary>
        public void Clear()
        {
            Current?.Unfocus();
            Focusables.Clear();
            CurrentIndex = 0;
        }

        /// <summary>
        /// Moves focus to the next element in the layer, wrapping around
        /// to the beginning when necessary.
        /// </summary>
        /// <returns>
        /// The newly focused element, or null if the layer contains no elements.
        /// </returns>
        public Focusable FocusNext()
        {
            if (Focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex = (CurrentIndex + 1) % Focusables.Count;

            Current?.Focus();

            return Current;
        }

        /// <summary>
        /// Moves focus to the previous element in the layer, wrapping around
        /// to the end when necessary.
        /// </summary>
        /// <returns>
        /// The newly focused element, or null if the layer contains no elements.
        /// </returns>
        public Focusable FocusPrevious()
        {
            if (Focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex =
                (CurrentIndex - 1 + Focusables.Count)
                % Focusables.Count;

            Current?.Focus();

            return Current;
        }

        /// <summary>
        /// Focuses the first element in the layer.
        /// </summary>
        /// <returns>
        /// The focused element, or null if the layer contains no elements.
        /// </returns>
        public Focusable FocusFirst()
        {
            if (Focusables.Count == 0)
                return null;

            CurrentIndex = 0;

            Current.Focus();

            return Current;
        }

        /// <summary>
        /// Removes focus from the currently focused element and resets the
        /// current index to the first position.
        /// </summary>
        public void Unfocus()
        {
            Current?.Unfocus();
            CurrentIndex = 0;
        }

        /// <summary>
        /// Invokes the confirmation action on the currently focused element.
        /// </summary>
        public void ConfirmCurrent()
        {
            Current?.ConfirmAction();
        }

        /// <summary>
        /// Invokes the confirmation action on the element at the specified index.
        /// </summary>
        /// <param name="index">The index of the element to confirm.</param>
        public void Confirm(int index)
        {
            if (index < 0 || index >= Focusables.Count)
                return;

            Focusables[index].ConfirmAction();
        }
    }
}