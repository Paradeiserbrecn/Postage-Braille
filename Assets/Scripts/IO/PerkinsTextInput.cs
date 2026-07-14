using System.Collections.Generic;
using System.Linq;
using System.Text;
using Braille;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace IO
{
    public class PerkinsTextInput: AbstractTextInput
    {
        private List<bool> _pressedDots;
        public PerkinsTextInput(GameActions gameActions) : base(gameActions)
        {
            this.Actions = gameActions;
        }
        public override void Enable()
        {
            _pressedDots = new List<bool>(Textbox.EmptyBrailleList);
            Actions.PerkinsBrailer.Dot0.started += OnDot0Started;
            Actions.PerkinsBrailer.Dot0.canceled += OnDot0Canceled;
            Actions.PerkinsBrailer.Dot1.started += OnDot1Started;
            Actions.PerkinsBrailer.Dot1.canceled += OnDot1Canceled;
            Actions.PerkinsBrailer.Dot2.started += OnDot2Started;
            Actions.PerkinsBrailer.Dot2.canceled += OnDot2Canceled;
            Actions.PerkinsBrailer.Dot3.started += OnDot3Started;
            Actions.PerkinsBrailer.Dot3.canceled += OnDot3Canceled;
            Actions.PerkinsBrailer.Dot4.started += OnDot4Started;
            Actions.PerkinsBrailer.Dot4.canceled += OnDot4Canceled;
            Actions.PerkinsBrailer.Dot5.started += OnDot5Started;
            Actions.PerkinsBrailer.Dot5.canceled += OnDot5Canceled;
            Actions.PerkinsBrailer.Delete.started += OnDeleteCharacter;
            Actions.PerkinsBrailer.Space.started += OnSpace;

            StringBuilder actionsString = new StringBuilder();
            actionsString.Append("action string: \n");
            foreach (var binding in Actions)
            {
                actionsString.Append(binding + "\n");
            }
            Debug.Log(actionsString.ToString());

            Actions.PerkinsBrailer.Enable();
        }

        public override void Disable()
        {
            Actions.PerkinsBrailer.Dot0.started -= OnDot0Started;
            Actions.PerkinsBrailer.Dot0.canceled -= OnDot0Canceled;
            Actions.PerkinsBrailer.Dot1.started -= OnDot1Started;
            Actions.PerkinsBrailer.Dot1.canceled -= OnDot1Canceled;
            Actions.PerkinsBrailer.Dot2.started -= OnDot2Started;
            Actions.PerkinsBrailer.Dot2.canceled -= OnDot2Canceled;
            Actions.PerkinsBrailer.Dot3.started -= OnDot3Started;
            Actions.PerkinsBrailer.Dot3.canceled -= OnDot3Canceled;
            Actions.PerkinsBrailer.Dot4.started -= OnDot4Started;
            Actions.PerkinsBrailer.Dot4.canceled -= OnDot4Canceled;
            Actions.PerkinsBrailer.Dot5.started -= OnDot5Started;
            Actions.PerkinsBrailer.Dot5.canceled -= OnDot5Canceled;
            Actions.PerkinsBrailer.Delete.started -= OnDeleteCharacter;
            Actions.PerkinsBrailer.Space.started -= OnSpace;

            Actions.PerkinsBrailer.Disable();
        }

        #region DotControlsActions
        private void OnDot0Started(InputAction.CallbackContext context) => OnDotNStarted(0, context);
        private void OnDot0Canceled(InputAction.CallbackContext context) => OnDotNCanceled(0, context);
        private void OnDot1Started(InputAction.CallbackContext context) => OnDotNStarted(1, context);
        private void OnDot1Canceled(InputAction.CallbackContext context) => OnDotNCanceled(1, context);
        private void OnDot2Started(InputAction.CallbackContext context) => OnDotNStarted(2, context);
        private void OnDot2Canceled(InputAction.CallbackContext context) => OnDotNCanceled(2, context);
        private void OnDot3Started(InputAction.CallbackContext context) => OnDotNStarted(3, context);
        private void OnDot3Canceled(InputAction.CallbackContext context) => OnDotNCanceled(3, context);
        private void OnDot4Started(InputAction.CallbackContext context) => OnDotNStarted(4, context);
        private void OnDot4Canceled(InputAction.CallbackContext context) => OnDotNCanceled(4, context);
        private void OnDot5Started(InputAction.CallbackContext context) => OnDotNStarted(5, context);
        private void OnDot5Canceled(InputAction.CallbackContext context) => OnDotNCanceled(5, context);
        #endregion
        
        private void LockInCharacter()
        {
            if (_pressedDots.SequenceEqual(Textbox.EmptyBrailleList))
            {
                Textbox.AddCurrentBrailleListAsCharacter();
                Textbox.NextBrailleCharacter();
            }
        }

        private void OnDotNStarted(int n, InputAction.CallbackContext context)
        {
            Textbox.currentBrailleList[n] = true;
            _pressedDots[n] = true;
            Textbox.UpdateCurrentBrailleObject();
        }

        private void OnDotNCanceled(int i, InputAction.CallbackContext context)
        {
            _pressedDots[i] = false;
            LockInCharacter();
        }

        private void OnDeleteCharacter(InputAction.CallbackContext context)
        {
            Textbox.DeleteCharacter();
        }

        private void OnSpace(InputAction.CallbackContext context)
        {
            Textbox.NextBrailleCharacter();
        }
    }
}