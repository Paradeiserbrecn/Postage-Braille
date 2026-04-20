using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    public string correctAnswer;
    public List<string> currentOptions = new();

    private List<string> _possibleLetters = new() { "A", "B", "L", "M", "AU" };

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Picks 1 correct and 3 false letters from `_possibleLetters` and adds them to `this.currentOptions` and populated `this.correctAnswer`
    /// </summary>
    /// <exception cref="InvalidOperationException">If there are too few possible letters to populate all options</exception>
    public void GenerateQuestion()
    {
        if (_possibleLetters.Count < 4)
            throw new InvalidOperationException("Not enough possible letters to generate options.");

        currentOptions.Clear();

        // Pick correct answer
        correctAnswer = _possibleLetters[Random.Range(0, _possibleLetters.Count)];

        // Create a pool of wrong answers
        var wrongOptions = _possibleLetters
            .Where(letter => letter != correctAnswer)
            .OrderBy(_ => Random.value)
            .Take(3)
            .ToList();

        // Combine correct + wrong
        currentOptions.Add(correctAnswer);
        currentOptions.AddRange(wrongOptions);

        Shuffle(currentOptions);
    }

    public bool CheckAnswer(string answer)
    {
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