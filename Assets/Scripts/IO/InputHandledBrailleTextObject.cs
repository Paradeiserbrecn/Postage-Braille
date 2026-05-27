using System.Collections.Generic;
using System.Text;
using Braille;
using UI;
using UnityEngine;

namespace IO
{
    public class InputHandledBrailleTextObject : BrailleTextObject
    {
        [SerializeField] private GameObject brailleObjectPrefab;
        public List<bool> currentBrailleList;

        private readonly List<BrailleObject> _brailleObjects = new();

        private readonly StringBuilder _proceedingText = new();
        public readonly List<bool> EmptyBrailleList = new() { false, false, false, false, false, false };
        private BrailleObject _currentBrailleObject;


        public void Start()
        {
            ResetCharacter();
            NextBrailleCharacter();
        }

        public void NextBrailleCharacter()
        {
            _proceedingText.Append(GridBrailleConverter.Instance.ConvertBrailleToCharacter(currentBrailleList));
            text = _proceedingText.ToString();
            ResetCharacter();
            _currentBrailleObject =
                Instantiate(brailleObjectPrefab, transform).GetComponent<BrailleObject>();
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
                _proceedingText.Remove(_proceedingText.Length - lastBrailleLength, lastBrailleLength);
                text = _proceedingText.ToString();
            }
        }

        public string GetText()
        {
            return _proceedingText.ToString();
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