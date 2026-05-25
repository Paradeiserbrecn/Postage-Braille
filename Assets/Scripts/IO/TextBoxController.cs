using System;
using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Braille;
using UI;
using UnityEngine.Serialization;

namespace IO
{
    public class TextBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject brailleObjectPrefab;
        [SerializeField] private GameObject textObjectPrefab;
        [SerializeField] private GameObject targetObject; //where the text will show up

        private readonly List<BrailleObject> _brailleObjects = new();
        public readonly List<bool> EmptyBrailleList = new() { false, false, false, false, false, false };
        
        private readonly StringBuilder _text = new();
        private BrailleObject _currentBrailleObject;
        public List<bool> currentBrailleList;
        private GridTextObject _gridTextObject;
        
        
        public void Start()
        {
            ResetCharacter();

            _gridTextObject = Instantiate(textObjectPrefab, targetObject.transform).GetComponent<GridTextObject>();
            Debug.Log(_text.ToString());
            NextBrailleCharacter();
        }
        
        public void NextBrailleCharacter()
        {
            _text.Append(GridBrailleConverter.Instance.ConvertBrailleToCharacter(currentBrailleList));
            ResetCharacter();
            _currentBrailleObject =
                Instantiate(brailleObjectPrefab, _gridTextObject.transform).GetComponent<BrailleObject>();
            _currentBrailleObject.SetBrailleCharacter(currentBrailleList);
            _brailleObjects.Add(_currentBrailleObject);
        }

        public void UpdateCurrentBrailleObject()
        {
            _currentBrailleObject.SetBrailleCharacter(currentBrailleList);
        }

        private void ResetCharacter()
        {
            currentBrailleList = new List<bool>(EmptyBrailleList);
        }
        
        public void DeleteCharacter()
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
        
        public string GetText()
        {
            return _text.ToString();
        }
        
        public void ClearText()
        {
            while (_brailleObjects.Count > 1)
            {
                DeleteCharacter();
            }
        }
    }
}