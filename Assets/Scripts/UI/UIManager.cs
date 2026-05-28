using System.Collections.Generic;
using System.Linq;
using Braille;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Utility;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [Header("Scene References")]
        [SerializeField] private GameObject questionPosition;
        [SerializeField] private GameObject optionsGrid;
        [SerializeField] private TextMeshProUGUI feedbackText;
        
        [Header("Prefabs")]
        public GameObject optionParentPrefab;
        public GameObject questionTextPrefab;
        
        private readonly List<GridTextObject> _optionBrailleTexts = new();
        private readonly List<FocusableTextObject> _optionTexts = new();

        private TextMeshProUGUI _questionText;

        private int _currentOptionIndex;

        public IFocusable HighlightedOption;
        [Header("Parameters")]
        public int optionsCount;

        public List<IFocusable> Options =>
            _optionTexts.Cast<IFocusable>()
                .Concat(_optionBrailleTexts)
                .ToList();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Create question text once
            _questionText = questionPosition.GetOrAddComponent<TextMeshProUGUI>();
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
            
        }

        private void ClearQuestionCanvas()
        {
            ClearChildren(questionPosition.transform);
            ClearChildren(optionsGrid.transform);
            _optionTexts.Clear();
            _optionBrailleTexts.Clear();
            HighlightedOption = null;
        }

        private void DisplayBrailleToLatinQuestion()
        {
            ClearQuestionCanvas();
            _questionText.text = "";

            // Generate the question braille and set it to the correct position
            var questionBraille = GridBrailleConverter.Instance
                .ConvertTextToBraille(QuestionManager.Instance.correctAnswer);

            questionBraille.transform.SetParent(questionPosition.transform, false);

            for (int i = 0; i < QuestionManager.Instance.currentOptions.Count; i++)
            {
                var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);
                var tmpObject = Instantiate(questionTextPrefab, parent.transform)
                    .GetComponent<TextMeshProUGUI>();
                var focusableText = new FocusableTextObject(tmpObject)
                {
                    Text = QuestionManager.Instance.currentOptions[i],
                    DisplayText = (i + 1) + ": " + QuestionManager.Instance.currentOptions[i]
                };

                _optionTexts.Add(focusableText);
            }

            feedbackText.text = "";
        }

        private void DisplayLatinToBrailleQuestion()
        {
            ClearQuestionCanvas();

            _questionText.text = QuestionManager.Instance.correctAnswer;

            for (var i = 0; i < QuestionManager.Instance.currentOptions.Count; i++)
            {
                var optionBraille = GridBrailleConverter.Instance
                    .ConvertTextToBraille(QuestionManager.Instance.currentOptions[i]);

                var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

                optionBraille.transform.SetParent(parent.transform, false);

                _optionBrailleTexts.Add(optionBraille.GetComponent<GridTextObject>());
            }

            feedbackText.text = "";
        }

        public IFocusable HighlightNextOption() => HighlightOption();
        public IFocusable HighlightPreviousOption() => HighlightOption(false);

        private IFocusable HighlightOption(bool next = true)
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

        private IFocusable HighlightPreviousFocusable()
        {
            List<IFocusable> options = Options;

            if (options.Count == 0)
                return null;

            if (HighlightedOption == null)
            {
                HighlightedOption = options[0];
                _currentOptionIndex = 0;
            }
            else
            {
                HighlightedOption.Unfocus();

                _currentOptionIndex = (_currentOptionIndex - 1 + options.Count) % options.Count;

                HighlightedOption = options[_currentOptionIndex];
            }

            HighlightedOption.Focus();
            return HighlightedOption;
        }

        private IFocusable HighlightNextFocusable()
        {
            List<IFocusable> options = Options;

            if (options.Count == 0)
                return null;

            if (HighlightedOption == null)
            {
                HighlightedOption = options[0];
                _currentOptionIndex = 0;
            }
            else
            {
                HighlightedOption.Unfocus();

                _currentOptionIndex = (_currentOptionIndex + 1) % options.Count;

                HighlightedOption = options[_currentOptionIndex];
            }

            HighlightedOption.Focus();
            return HighlightedOption;
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