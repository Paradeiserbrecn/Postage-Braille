using System.Collections.Generic;
using System.Linq;
using Braille;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Scene References")] [SerializeField]
        private GameObject questionPosition;

        [SerializeField] private GameObject optionsGrid;
        [SerializeField] private TextMeshProUGUI feedbackText;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private List<UILayer> _layers;

        [Header("Prefabs")] [SerializeField] private GameObject optionParentPrefab;
        public GameObject questionTextPrefab;
        [Header("Parameters")] public int optionsCount;


        private int _currentOptionIndex;

        public Focusable CurrentlyFocusedOption => CurrentLayer?.Current;

        private int _currentLayerIdx = 0;
        private UILayer CurrentLayer => _layers.Count == 0 ? null : _layers[_currentLayerIdx];

        public List<Focusable> CurrentOptions => _layers.Count == 0
            ? null
            : _layers[_currentLayerIdx].Focusables;

        private void Awake()
        {
            Instance = this;
            _layers.Add(new UILayer("Base"));
        }
        
        public void DisplayQuestion()
        {
            if (GameManager.Instance.currentQuestionType == GameManager.QuestionType.CharBrailleToLatin)
            {
                DisplayBrailleToLatinQuestion();
            }
            else if (GameManager.Instance.currentQuestionType == GameManager.QuestionType.CharLatinToBraille)
            {
                DisplayLatinToBrailleQuestion();
            }
            else
            {
                Debug.LogWarning("Tried to display unsupported question type.");
            }

            CurrentLayer.FocusFirst();
        }

        private void ClearQuestionCanvas()
        {
            ClearChildren(questionPosition.transform);
            ClearChildren(optionsGrid.transform);
            CurrentLayer?.Clear();
        }

        private void DisplayBrailleToLatinQuestion()
        {
            ClearQuestionCanvas();
            _questionText.text = "";

            // Generate the question braille and set it to the correct position
            GridBrailleConverter.Instance
                .ConvertTextToBraille(QuestionManager.Instance.correctAnswer, parent: questionPosition.transform);

            foreach (var option in QuestionManager.Instance.currentOptions)
            {
                CurrentLayer.Add(GenerateFocusableTextObjectOption(option));
            }

            feedbackText.text = "";
        }

        private FocusableTextObject GenerateFocusableTextObjectOption(string optionText)
        {
            var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

            var focusableText = Instantiate(questionTextPrefab, parent.transform)
                .GetComponent<FocusableTextObject>();

            focusableText.Text = optionText;

            return focusableText;
        }

        private void DisplayLatinToBrailleQuestion()
        {
            ClearQuestionCanvas();

            _questionText.text = QuestionManager.Instance.correctAnswer;

            foreach (var optionText in QuestionManager.Instance.currentOptions)
            {
                CurrentLayer.Add(GenerateBrailleOption(optionText));
            }

            feedbackText.text = "";
        }

        private GridTextObject GenerateBrailleOption(string optionText)
        {
            var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

            var optionBraille = GridBrailleConverter.Instance
                .ConvertTextToBraille(optionText, parent: parent.transform);

            return optionBraille.GetComponent<GridTextObject>();
        }

        public Focusable HighlightNextOption() => HighlightOption();
        public Focusable HighlightPreviousOption() => HighlightOption(false);

        private Focusable HighlightOption(bool next = true)
        {
            if (GameManager.Instance.currentState != GameManager.GameState.WaitingForInput)
            {
                Debug.LogWarning("Tried to highlight a braille text object while not waiting for input.");
                return null;
            }

            // This will be expanded once we have different menu options.
            switch (GameManager.Instance.currentQuestionType)
            {
                case GameManager.QuestionType.CharBrailleToLatin:
                case GameManager.QuestionType.CharLatinToBraille:
                    return next ? HighlightNextFocusable() : HighlightPreviousFocusable();
            }

            Debug.LogWarning("Highlighting for this Option type is not yet supported");
            return null;
        }

        private Focusable HighlightNextFocusable()
        {
            return CurrentLayer.FocusNext();
        }

        private Focusable HighlightPreviousFocusable()
        {
            return CurrentLayer.FocusPrevious();
        }

        public void ShowFeedback(bool correct)
        {
            feedbackText.text = correct ? "Correct!" : "Wrong!";
        }

        private void ClearChildren(Transform parent)
        {
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }

            parent.DetachChildren();
        }
    }
}