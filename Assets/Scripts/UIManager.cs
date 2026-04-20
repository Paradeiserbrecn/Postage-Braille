using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject questionPosition;
    public List<TextMeshProUGUI> optionTexts;
    public TextMeshProUGUI feedbackText;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayQuestion()
    {
        var questionText = BrailleConverter.Instance.ConvertCharacterToBraille(QuestionManager.Instance.correctAnswer);
        
        questionText.transform.SetParent(questionPosition.transform, false);

        for (var i = 0; i < optionTexts.Count; i++)
        {
            optionTexts[i].text = (i + 1) + ": " + QuestionManager.Instance.currentOptions[i];
        }

        feedbackText.text = "";
    }

    public void ShowFeedback(bool correct)
    {
        feedbackText.text = correct ? "Correct!" : "Wrong!";
    }
}