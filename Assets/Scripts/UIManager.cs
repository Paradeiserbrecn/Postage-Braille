using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject questionPosition;
    public GameObject optionsGrid;
    public int optionsCount;
    public TextMeshProUGUI feedbackText;
    public GameObject questionTextPrefab;

    private TextMeshProUGUI _questionText;
    private readonly List<FocusableTextTextObject> _optionTexts = new();
    private readonly List<GridTextObject> _optionBrailleTexts = new();

    public List<IFocusableText> Options =>
        _optionTexts.Cast<IFocusableText>()
            .Concat(_optionBrailleTexts)
            .ToList();
    private int currentOptionIndex;

    public IFocusableText HighlightedOption;

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
            Debug.Log("Tried to display unsupported question type.");
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
            var tmpObject = Instantiate(questionTextPrefab, optionsGrid.transform, false).GetComponent<TextMeshProUGUI>();
            var focusableText = new FocusableTextTextObject(tmpObject)
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

            optionBraille.transform.SetParent(optionsGrid.transform, false);

            _optionBrailleTexts.Add(optionBraille.GetComponent<GridTextObject>());
        }

        feedbackText.text = "";
    }

    public IFocusableText HighlightNextOption() => HighlightOption();
    public IFocusableText HighlightPreviousOption() => HighlightOption(false);

    private IFocusableText HighlightOption(bool next = true)
    {
        if (GameManager.Instance.currentState != GameManager.GameState.WaitingForInput)
        {
            Debug.Log("Tried to highlight a braille text object while not waiting for input.");
            return null;
        }


        // This will be expanded once we have different menu options.
        switch (GameManager.Instance.currentQuestionType)
        {
            case GameManager.QuestionType.CharBrailleToLatin:
            case GameManager.QuestionType.CharLatinToBraille:
                return next ? HighlightNextFocusable() : HighlightPreviousFocusable();
        }

        Debug.Log("Highlighting for this Option type is not yet supported");
        return null;
    }

    private IFocusableText HighlightPreviousFocusable()
    {
        List<IFocusableText> options = Options;

        if (options.Count == 0)
            return null;

        if (HighlightedOption == null)
        {
            HighlightedOption = options[0];
            currentOptionIndex = 0;
        }
        else
        {
            HighlightedOption.Unfocus();
            
            currentOptionIndex = (currentOptionIndex - 1 + options.Count) % options.Count;

            HighlightedOption = options[currentOptionIndex];
        }

        HighlightedOption.Focus();
        return HighlightedOption;
    }

    private IFocusableText HighlightNextFocusable()
    {
        List<IFocusableText> options = Options;

        if (options.Count == 0)
            return null;

        if (HighlightedOption == null)
        {
            HighlightedOption = options[0];
            currentOptionIndex = 0;
        }
        else
        {
            HighlightedOption.Unfocus();

            currentOptionIndex = (currentOptionIndex + 1) % options.Count;

            HighlightedOption = options[currentOptionIndex];
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