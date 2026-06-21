using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Braille;
using Data;
using Settings;
using TMPro;
using UI;
using Unity.Collections;
using Utility;
using Random = UnityEngine.Random;

/// <summary>
/// Manages question generation, display, answer validation,
/// and UI interactions for Braille learning exercises.
/// Supports both Braille-to-Latin and Latin-to-Braille question modes.
/// </summary>
public class QuestionManager : MonoBehaviour
{
    /// <summary>
    /// Defines the supported question directions.
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// Display a Braille character and ask the user
        /// to select the corresponding Latin character.
        /// </summary>
        CharBrailleToLatin,

        /// <summary>
        /// Display a Latin character and ask the user
        /// to select the corresponding Braille representation.
        /// </summary>
        CharLatinToBraille
    }

    private const string QuestionLayerName = "QuestionLayer";
    public static QuestionManager Instance;

    public QuestionType currentQuestionType = QuestionType.CharBrailleToLatin;

    [Header("Parameters")] public int optionsCount;

    [Header("Scene References")] [SerializeField]
    private GameObject questionPosition;

    [SerializeField] private GameObject optionsGrid;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI questionText;

    [Header("Prefabs")] [SerializeField] private GameObject optionParentPrefab;
    [SerializeField] public GameObject questionTextPrefab;

    /// <summary>
    /// The currently active question UI layer.
    /// </summary>
    /// <remarks>Mainly used as a shorthand for UIManager.Instance.layers[_questionLayerIndex]</remarks>
    public UILayer QuestionLayer => UIManager.Instance.layers[_questionLayerIndex];

    public string correctAnswer;

    /// <summary>
    /// The answer options currently available to the player,
    /// including the correct answer.
    /// </summary>
    public List<string> currentOptions = new();

    private int _questionLayerIndex;

    private void Awake()
    {
        Instance = this;
        _questionLayerIndex = UIManager.Instance.AddLayer(new UILayer(QuestionLayerName));
    }

    /// <summary>
    /// Populates <see cref="currentOptions"/> with a randomized set of answer options
    /// and assigns a value to <see cref="correctAnswer"/> for the current question.
    /// The generated options always include the correct answer exactly once.
    /// </summary>
    public void PopulateCurrentOptions()
    {
        // Because we take 4 option anchors as SerializedFields in the UIManager Component
        if (LetterPackages.Instance.CurrentPackageProgresses.Letters.Count < optionsCount)
            throw new InvalidOperationException("Not enough possible letters to generate options.");

        currentOptions.Clear();

        // Pick correct answer
        correctAnswer =
            LetterPackages.Instance.CurrentPackageProgresses.Letters[
                Random.Range(0, LetterPackages.Instance.CurrentPackageProgresses.Letters.Count)];

        // Create a pool of wrong answers
        var wrongOptions = LetterPackages.Instance.CurrentPackageProgresses.Letters
            .Where(letter => letter != correctAnswer)
            .OrderBy(_ => Random.value)
            .Take(optionsCount - 1)
            .ToList();

        // Combine correct + wrong
        currentOptions.Add(correctAnswer);
        currentOptions.AddRange(wrongOptions);

        Helpers.ShuffleList(currentOptions);
    }


    /// <summary>
    /// Generates GridTextObjects/FocusableTextObjects based on QuestionManager.Instance.currentOptions,
    /// sets the current layer to the question-layer, and then focuses the first option in that layer
    /// </summary>
    public void DisplayQuestion()
    {
        if (currentQuestionType == QuestionType.CharBrailleToLatin)
        {
            DisplayBrailleToLatinQuestion();
        }
        else if (currentQuestionType == QuestionType.CharLatinToBraille)
        {
            DisplayLatinToBrailleQuestion();
        }
        else
        {
            Debug.LogWarning("Tried to display unsupported question type.");
        }

        UIManager.Instance.SwitchLayer(_questionLayerIndex);
        QuestionLayer.FocusFirst();
    }


    /// <summary>
    /// Displays a Braille-to-Latin question by rendering the correct answer
    /// in Braille and creating text-based answer options.
    /// </summary>
    private void DisplayBrailleToLatinQuestion()
    {
        ClearQuestionCanvas();
        questionText.text = "";

        // Generate the question braille and set it to the correct position
        GridBrailleConverter.Instance
            .ConvertTextToBraille(correctAnswer, parent: questionPosition.transform);

        foreach (var option in currentOptions)
        {
            QuestionLayer
                .Add(GenerateFocusableTextObjectOption(option));
        }

        feedbackText.text = "";
    }


    /// <summary>
    /// Creates a focusable text option and adds it to the options grid.
    /// </summary>
    /// <param name="optionText">The text displayed for the option.</param>
    /// <returns>The created focusable text object.</returns>
    private FocusableTextObject GenerateFocusableTextObjectOption(string optionText)
    {
        var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

        var focusableText = Instantiate(questionTextPrefab, parent.transform)
            .GetComponent<FocusableTextObject>();

        focusableText.Text = optionText;

        return focusableText;
    }

    /// <summary>
    /// Displays feedback indicating whether the user's answer was correct.
    /// </summary>
    /// <param name="correct">
    /// True to display positive feedback; otherwise displays negative feedback.
    /// </param>
    public void ShowFeedback(bool correct)
    {
        feedbackText.text = correct ? "Correct!" : "Wrong!";
    }


    /// <summary>
    /// Displays a Latin-to-Braille question by showing the answer as text
    /// and generating Braille answer options.
    /// </summary>
    private void DisplayLatinToBrailleQuestion()
    {
        ClearQuestionCanvas();

        questionText.text = correctAnswer;

        foreach (var optionText in currentOptions)
        {
            QuestionLayer.Add(GenerateBrailleOption(optionText));
        }

        feedbackText.text = "";
    }

    /// <summary>
    /// Creates a Braille option and adds it to the options grid.
    /// </summary>
    /// <param name="optionText">The text to convert into Braille.</param>
    /// <returns>The generated Braille grid text object.</returns>
    private BrailleTextObject GenerateBrailleOption(string optionText)
    {
        var parent = Instantiate(optionParentPrefab, optionsGrid.transform, false);

        var optionBraille = GridBrailleConverter.Instance
            .ConvertTextToBraille(optionText, parent: parent.transform);

        return optionBraille.GetComponent<BrailleTextObject>();
    }

    /// <summary>
    /// Removes all currently displayed question content, option content,
    /// and registered focusable elements from the active layer.
    /// </summary>
    private void ClearQuestionCanvas()
    {
        UIManager.Instance.ClearChildren(questionPosition.transform);
        UIManager.Instance.ClearChildren(optionsGrid.transform);
        QuestionLayer.Clear();
    }

    /// <summary>
    /// Determines whether the supplied answer matches
    /// the correct answer for the current question.
    /// </summary>
    /// <param name="answer">The answer selected by the user.</param>
    /// <returns>
    /// True if the answer is correct; otherwise, false.
    /// </returns>
    public bool CheckAnswer(string answer)
    {
        Debug.Log("Answered with: " + answer + " and correct: " + correctAnswer);
        return answer == correctAnswer;
    }
}