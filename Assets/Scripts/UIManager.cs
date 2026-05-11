using System;
using UnityEngine;
using System.Collections.Generic;
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

    private readonly List<TextMeshProUGUI> _optionTexts = new();
    private TextMeshProUGUI _questionText;
    private readonly CircularList<GridTextObject> _gridTextObjects = new();
    public GridTextObject HighlightedGridTextObject;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Create question text once
        _questionText = questionPosition.GetOrAddComponent<TextMeshProUGUI>();

        // Create option texts once
        for (var i = 0; i < optionsCount; i++)
        {
            var obj = Instantiate(questionTextPrefab, optionsGrid.transform, false);
            var text = obj.GetComponent<TextMeshProUGUI>();
            _optionTexts.Add(text);
        }
    }

    public void DisplayBrailleToLatinQuestion()
    {
        // Clear only braille visuals if needed (not text objects)
        ClearChildren(questionPosition.transform);
        _questionText.text = "";
        _gridTextObjects.Clear();

        var questionBraille = GridBrailleConverter.Instance
            .ConvertTextToBraille(QuestionManager.Instance.correctAnswer);

        questionBraille.transform.SetParent(questionPosition.transform, false);

        for (int i = 0; i < _optionTexts.Count; i++)
        {
            ClearChildren(_optionTexts[i].transform);
            _optionTexts[i].text = (i + 1) + ": " + QuestionManager.Instance.currentOptions[i];
        }

        feedbackText.text = "";
    }

    public void DisplayLatinToBrailleQuestion()
    {
        ClearChildren(questionPosition.transform);
        _gridTextObjects.Clear();

        _questionText.text = QuestionManager.Instance.correctAnswer;

        // Instead of destroying, clear and reuse option containers
        for (var i = 0; i < _optionTexts.Count; i++)
        {
            ClearChildren(_optionTexts[i].transform);

            _optionTexts[i].text = "";

            var optionBraille = GridBrailleConverter.Instance
                .ConvertTextToBraille(QuestionManager.Instance.currentOptions[i]);

            optionBraille.transform.SetParent(_optionTexts[i].transform, false);

            // Add the newly created braille option object to a list for arrow key navigation and highlighting
            _gridTextObjects.Add(optionBraille.GetComponent<GridTextObject>());
        }

        feedbackText.text = "";
    }
    

    public GridTextObject HighlightNextGridTextObject()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.WaitingForInput)
        {
            Debug.Log("Tried to highlight a braille text object while not waiting for input.");
            return null;
        }
        
        Debug.Log(_gridTextObjects.ToString());

        if (!HighlightedGridTextObject)
            HighlightedGridTextObject = _gridTextObjects[0];
        else
        {
            HighlightedGridTextObject.Unfocus();
            HighlightedGridTextObject = _gridTextObjects.Next();
        }
        HighlightedGridTextObject.Focus();
        return HighlightedGridTextObject;
    }
    
    public GridTextObject HighlightPreviousGridTextObject()
    {
        if (GameManager.Instance.currentState != GameManager.GameState.WaitingForInput)
        {
            Debug.Log("Tried to highlight a braille text object while not waiting for input.");
            return null;
        }

        if (!HighlightedGridTextObject)
        {
            HighlightedGridTextObject = _gridTextObjects[0];
        }
        else
        {
            HighlightedGridTextObject.Unfocus();
            HighlightedGridTextObject = _gridTextObjects.Previous();
        }
        HighlightedGridTextObject.Focus();
        return HighlightedGridTextObject;
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
    }
}