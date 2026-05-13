using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Unity.VisualScripting;

public class PerkinsInputHandler : MonoBehaviour
{
    private readonly List<bool> _emptyBrailleList = new List<bool>{false, false, false, false, false, false};
    private PerkinsActions _actions;
    
    [SerializeField] private GameObject brailleObjectPrefab;
    [SerializeField] private GameObject textObjectPrefab;
    [SerializeField] private GameObject targetObject; //where the text will show up
    
    private List<BrailleObject> _brailleObjects = new List<BrailleObject>();
    private StringBuilder _text = new StringBuilder();
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
    private void NewBrailleCharacter()
    {
        ResetCharacter();
        _currentBrailleObject = Instantiate(brailleObjectPrefab, _gridTextObject.transform).GetComponent<BrailleObject>();
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
            Debug.Log(_text.ToString());
        }
    }
    private void OnDot0Started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentBrailleList[0] = true;
        _pressedDots[0] = true;
        _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
    }
    private void OnDot0Canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _pressedDots[0] = false;
        LockInCharacter();
    }
    private void OnDot1Started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentBrailleList[1] = true;
        _pressedDots[1] = true;
        _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
    }
    private void OnDot1Canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _pressedDots[1] = false;
        LockInCharacter();
    }
    private void OnDot2Started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentBrailleList[2] = true;
        _pressedDots[2] = true;
        _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
    }
    private void OnDot2Canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _pressedDots[2] = false;
        LockInCharacter();
    }
    private void OnDot3Started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentBrailleList[3] = true;
        _pressedDots[3] = true;
        _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
    }
    private void OnDot3Canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _pressedDots[3] = false;
        LockInCharacter();
    }
    private void OnDot4Started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentBrailleList[4] = true;
        _pressedDots[4] = true;
        _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
    }
    private void OnDot4Canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _pressedDots[4] = false;
        LockInCharacter();
    }
    private void OnDot5Started(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _currentBrailleList[5] = true;
        _pressedDots[5] = true;
        _currentBrailleObject.SetBrailleCharacter(_currentBrailleList);
    }
    private void OnDot5Canceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _pressedDots[5] = false;
        LockInCharacter();
    }

    private void OnDeleteCharacter(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (_brailleObjects.Count > 1)
        {
            int lastBrailleLength = GridBrailleConverter.Instance.ConvertBrailleToCharacter(_brailleObjects[^2].DotBools).Length;
            Destroy(_brailleObjects[^2].gameObject);
            _brailleObjects.Remove(_brailleObjects[^2]);
            _text.Remove(_text.Length - lastBrailleLength, lastBrailleLength);
        }
    }
    private void OnSpace(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        NewBrailleCharacter();
        _text.Append(" ");
    }

    public String GetText()
    {
        return _text.ToString();
    }

    public void ClearText()
    {
        while(_brailleObjects.Count > 1)
        {
            OnDeleteCharacter(new UnityEngine.InputSystem.InputAction.CallbackContext());
        }
    }
}