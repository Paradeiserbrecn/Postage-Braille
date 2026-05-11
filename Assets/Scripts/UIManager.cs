using UnityEngine;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject questionPosition;
    public List<GameObject> optionsPositions = new();
    public TextMeshProUGUI feedbackText;
    public GameObject questionTextPrefab;
    
    private List<TextMeshProUGUI> optionTexts = new();
    private TextMeshProUGUI questionText;
    
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        // Create question text once
        questionText = questionPosition.GetOrAddComponent<TextMeshProUGUI>();

        // Create option texts once
        foreach (var t in optionsPositions)
        {
            var obj = Instantiate(questionTextPrefab, t.transform, false);
            var text = obj.GetComponent<TextMeshProUGUI>();
            optionTexts.Add(text);
        }
    }
    
    public void DisplayBrailleToLatinQuestion()
    {
        // Clear only braille visuals if needed (not text objects)
        ClearChildren(questionPosition.transform);

        var questionBraille = GridBrailleConverter.Instance
            .ConvertTextToBraille(QuestionManager.Instance.correctAnswer);

        questionBraille.transform.SetParent(questionPosition.transform, false);

        for (int i = 0; i < optionTexts.Count; i++)
        {
            optionTexts[i].text = (i + 1) + ": " + QuestionManager.Instance.currentOptions[i];
        }

        feedbackText.text = "";
    }

    public void DisplayLatinToBrailleQuestion()
    {
        ClearChildren(questionPosition.transform);

        questionText.text = QuestionManager.Instance.correctAnswer;

        // Instead of destroying, clear and reuse option containers
        for (var i = 0; i < optionsPositions.Count; i++)
        {
            ClearChildren(optionsPositions[i].transform);

            var optionBraille = GridBrailleConverter.Instance
                .ConvertTextToBraille(QuestionManager.Instance.currentOptions[i]);

            optionBraille.transform.SetParent(optionsPositions[i].transform, false);
        }

        feedbackText.text = "";
    }
    
    public void ShowFeedback(bool correct)
    {
        feedbackText.text = correct ? "Correct!" : "Wrong!";
    }
    
    private static void ClearChildren(Transform parent)
    {
        for (var i = parent.childCount - 1; i >= 0; i--)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
}