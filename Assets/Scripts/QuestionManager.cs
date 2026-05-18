using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Data;
using Settings;
using UI;
using Random = UnityEngine.Random;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    public string correctAnswer;
    public List<string> currentOptions = new();


    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// TODO: Add summary 
    /// </summary>
    /// <exception cref="InvalidOperationException">If there are too few possible letters to populate all options</exception>
    public void GenerateQuestion()
    {
        // Because we take 4 option anchors as SerializedFields in the UIManager Component
        if (LetterPackages.Instance.CurrentPackageProgresses.Letters.Count < UIManager.Instance.optionsCount)
            throw new InvalidOperationException("Not enough possible letters to generate options.");

        currentOptions.Clear();

        // Pick correct answer
        correctAnswer = LetterPackages.Instance.CurrentPackageProgresses.Letters[Random.Range(0, LetterPackages.Instance.CurrentPackageProgresses.Letters.Count)];
        
        // Create a pool of wrong answers
        var wrongOptions = LetterPackages.Instance.CurrentPackageProgresses.Letters
            .Where(letter => letter != correctAnswer)
            .OrderBy(_ => Random.value)
            .Take(UIManager.Instance.optionsCount - 1)
            .ToList();

        // Combine correct + wrong
        currentOptions.Add(correctAnswer);
        currentOptions.AddRange(wrongOptions);

        Shuffle(currentOptions);
    }

    public bool CheckAnswer(string answer)
    {
        Debug.Log("Answered with: " + answer + " and correct: " + correctAnswer);
        return answer == correctAnswer;
    }

    void Shuffle(List<string> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var temp = list[i];
            var randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}