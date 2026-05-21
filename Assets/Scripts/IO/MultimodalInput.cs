using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Braille;
using UI;

namespace IO
{
    public class MultimodalInput : MonoBehaviour
    {
        [SerializeField] private GameObject brailleObjectPrefab;
        [SerializeField] private GameObject textObjectPrefab;
        [SerializeField] private GameObject targetObject; //where the text will show up

        private readonly List<BrailleObject> _brailleObjects = new();
        private readonly List<bool> _emptyBrailleList = new() { false, false, false, false, false, false };
        
        private readonly StringBuilder _text = new();
        private BrailleObject _currentBrailleObject;
        private GridTextObject _gridTextObject;
        
        
        
    }
}