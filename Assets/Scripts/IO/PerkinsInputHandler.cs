using System.Collections.Generic;
using System.Linq;
using System.Text;
using Braille;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace IO
{
    public class PerkinsInputHandler : MonoBehaviour
    {
        [SerializeField] private GameObject brailleObjectPrefab;
        [SerializeField] private GameObject textObjectPrefab;
        [SerializeField] private GameObject targetObject; //where the text will show up

        private readonly List<BrailleObject> _brailleObjects = new();
        private readonly List<bool> _emptyBrailleList = new() { false, false, false, false, false, false };
        private readonly StringBuilder _text = new();
        private PerkinsActions _actions;
        private List<bool> _currentBrailleList, _pressedDots;
        private BrailleObject _currentBrailleObject;
        private GridTextObject _gridTextObject;

        public void Awake()
        {
            ResetCharacter();
            _actions = new PerkinsActions();

            _gridTextObject = Instantiate(textObjectPrefab, targetObject.transform).GetComponent<GridTextObject>();
            NewBrailleCharacter();
            _pressedDots = new List<bool>(_emptyBrailleList);
        }

        private void OnEnable()
        {
            _actions.PerkinsBrailer.Dot0.started += OnDot0Started;
            _actions.PerkinsBrailer.Dot0.canceled += OnDot0Canceled;
            _actions.PerkinsBrailer.Dot1.started += OnDot1Started;
            _actions.PerkinsBrailer.Dot1.canceled += OnDot1Canceled;
            _actions.PerkinsBrailer.Dot2.started += OnDot2Started;
            _actions.PerkinsBrailer.Dot2.canceled += OnDot2Canceled;
            _actions.PerkinsBrailer.Dot3.started += OnDot3Started;
            _actions.PerkinsBrailer.Dot3.canceled += OnDot3Canceled;
            _actions.PerkinsBrailer.Dot4.started += OnDot4Started;
            _actions.PerkinsBrailer.Dot4.canceled += OnDot4Canceled;
            _actions.PerkinsBrailer.Dot5.started += OnDot5Started;
            _actions.PerkinsBrailer.Dot5.canceled += OnDot5Canceled;
            _actions.PerkinsBrailer.Delete.started += OnDeleteCharacter;
            _actions.PerkinsBrailer.Space.started += OnSpace;

            _actions.Enable();
        }

        private void OnDisable()
        {
            _actions.PerkinsBrailer.Dot0.started -= OnDot0Started;
            _actions.PerkinsBrailer.Dot0.canceled -= OnDot0Canceled;
            _actions.PerkinsBrailer.Dot1.started -= OnDot1Started;
            _actions.PerkinsBrailer.Dot1.canceled -= OnDot1Canceled;
            _actions.PerkinsBrailer.Dot2.started -= OnDot2Started;
            _actions.PerkinsBrailer.Dot2.canceled -= OnDot2Canceled;
            _actions.PerkinsBrailer.Dot3.started -= OnDot3Started;
            _actions.PerkinsBrailer.Dot3.canceled -= OnDot3Canceled;
            _actions.PerkinsBrailer.Dot4.started -= OnDot4Started;
            _actions.PerkinsBrailer.Dot4.canceled -= OnDot4Canceled;
            _actions.PerkinsBrailer.Dot5.started -= OnDot5Started;
            _actions.PerkinsBrailer.Dot5.canceled -= OnDot5Canceled;
            _actions.PerkinsBrailer.Delete.started -= OnDeleteCharacter;
            _actions.PerkinsBrailer.Space.started -= OnSpace;

            _actions.Disable();
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

        private void NewBrailleCharacter()
        {
            ResetCharacter();
            _currentBrailleObject =
                Instantiate(brailleObjectPrefab, _gridTextObject.transform).GetComponent<BrailleObject>();
            _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
            _brailleObjects.Add(_currentBrailleObject);
        }

        private void ResetCharacter()
        {
            _currentBrailleList = new List<bool>(_emptyBrailleList);
        }

        private void LockInCharacter()
        {
            if (_pressedDots.SequenceEqual(_emptyBrailleList))
            {
                _text.Append(GridBrailleConverter.Instance.ConvertBrailleToCharacter(_currentBrailleList));
                NewBrailleCharacter();
            }
        }


        private void OnDotNStarted(int n, InputAction.CallbackContext context)
        {
            _currentBrailleList[n] = true;
            _pressedDots[n] = true;
            _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
        }

        private void OnDotNCanceled(int i, InputAction.CallbackContext context)
        {
            _pressedDots[i] = false;
            LockInCharacter();
        }

        private void OnDeleteCharacter(InputAction.CallbackContext context)
        {
            if (_brailleObjects.Count > 1)
            {
                int lastBrailleLength = GridBrailleConverter.Instance
                    .ConvertBrailleToCharacter(_brailleObjects[^2].DotBools).Length;
                Destroy(_brailleObjects[^2].gameObject);
                _brailleObjects.Remove(_brailleObjects[^2]);
                _text.Remove(_text.Length - lastBrailleLength, lastBrailleLength);
            }
        }

        private void OnSpace(InputAction.CallbackContext context)
        {
            NewBrailleCharacter();
            _text.Append(" ");
        }

        public string GetText()
        {
            return _text.ToString();
        }

        public void ClearText()
        {
            while (_brailleObjects.Count > 1)
            {
                OnDeleteCharacter(new InputAction.CallbackContext());
            }
        }
    }
}