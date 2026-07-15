using System.Collections.Generic;
using System.Text;
using Braille;
using UI;
using UnityEngine;
using Utility;

namespace IO
{
    public class InputHandledUITextObject : UITextObject
    {
        [SerializeField] private GameObject brailleObjectPrefab;
        public DestroyDisableNotifier destroyDisableNotifier;
        public List<bool> currentBrailleList;

        public readonly List<BrailleObject> BrailleObjects = new();

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
            text = _proceedingText.ToString();
            _textMeshPro.text = text;
            ResetCharacter();
            _currentBrailleObject =
                Instantiate(brailleObjectPrefab, transform).GetComponent<BrailleObject>();
            _currentBrailleObject.SetBrailleCharacter(currentBrailleList);
            BrailleObjects.Add(_currentBrailleObject);
        }

        public void AddCurrentBrailleListAsCharacter()
        {
            _proceedingText.Append(GridBrailleConverter.Instance.ConvertBrailleToCharacter(currentBrailleList));
        }

        public void UpdateCurrentBrailleObject()
        {
            _currentBrailleObject.SetBrailleCharacter(currentBrailleList);
        }

        private void ResetCharacter()
        {
            currentBrailleList = new List<bool>(EmptyBrailleList);
        }

        public void AddCharacter(string character)
        {
            currentBrailleList = GridBrailleConverter.Instance.ConvertCharacterToBrailleList(character);
            _proceedingText.Append(character);
            UpdateCurrentBrailleObject();
            NextBrailleCharacter();
        }

        public void DeleteCharacter()
        {
            if (BrailleObjects.Count > 1)
            {
                int lastBrailleLength = GridBrailleConverter.Instance
                    .ConvertBrailleToCharacter(BrailleObjects[^2].DotBools).Length;
                Destroy(BrailleObjects[^2].gameObject);
                BrailleObjects.Remove(BrailleObjects[^2]);
                _proceedingText.Remove(_proceedingText.Length - lastBrailleLength, lastBrailleLength);
                text = _proceedingText.ToString();
                _textMeshPro.text = text;
            }
        }

        public string GetText()
        {
            return _proceedingText.ToString();
        }

        public void ClearText()
        {
            while (BrailleObjects.Count > 1)
            {
                DeleteCharacter();
            }
        }
        public override void Focus()
        {
            base.Focus();
            MultimodalInputManager.Instance.EnableTextInput(MultimodalInputManager.TextInputType.Perkins, this);
        }

        public override void Unfocus()
        {
            base.Unfocus();
            MultimodalInputManager.Instance.DisableTextInput();
        }
    }
}