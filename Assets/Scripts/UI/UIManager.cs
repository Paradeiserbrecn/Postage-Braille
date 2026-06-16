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

        [Header("Scene References")] [SerializeField]
        private GameObject questionPosition;

        [SerializeField] private GameObject optionsGrid;
        [SerializeField] private TextMeshProUGUI feedbackText;

        [Header("Prefabs")] public GameObject optionParentPrefab;
        public GameObject questionTextPrefab;

        private readonly List<GridTextObject> _optionBrailleTexts = new();
        private readonly List<FocusableTextObject> _optionTexts = new();

        private TextMeshProUGUI _questionText;

        private int _currentOptionIndex;

        public Focusable HighlightedOption;
        [Header("Parameters")] public int optionsCount;

        public List<Focusable> Options =>
            _optionTexts.Cast<Focusable>()
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
            GridBrailleConverter.Instance
                .ConvertTextToBraille(QuestionManager.Instance.correctAnswer, parent: questionPosition.transform);

            foreach (var option in QuestionManager.Instance.currentOptions)
            {
                _optionTexts.Add(GenerateFocusableTextObjectOption(option));
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
                _optionBrailleTexts.Add(GenerateBrailleOption(optionText));
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

        private Focusable HighlightPreviousFocusable()
        {
            List<Focusable> options = Options;

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

        private Focusable HighlightNextFocusable()
        {
            List<Focusable> options = Options;

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