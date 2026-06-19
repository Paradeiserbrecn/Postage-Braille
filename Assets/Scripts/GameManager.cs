using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public QuestionType currentQuestionType = QuestionType.CharBrailleToLatin;

    public enum QuestionType
    {
        CharBrailleToLatin,
        CharLatinToBraille
    }
    public enum GameState
    {
        ShowQuestion,
        WaitingForInput,
        ShowFeedback,
    }

    public GameState currentState;
    private Action<string, float> _invoke;

    private void Awake()
    {
        Instance = this;
        _invoke = Invoke;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        NextQuestion();
    }

    public void NextQuestion()
    {
        currentState = GameState.ShowQuestion;

        QuestionManager.Instance.GenerateQuestion();
        // TODO Add a reverse display thing, so you can decide whether to do a braille to latin question or vice versa,
        // Should be done inside of UIManager -- The rest should stay the same probably
        UIManager.Instance.DisplayQuestion();
        
        currentState = GameState.WaitingForInput;
    }

    public void SubmitAnswer(string answer)
    {
        if (currentState != GameState.WaitingForInput) return;
        
        var correct = QuestionManager.Instance.CheckAnswer(answer);
        
        currentState = GameState.ShowFeedback;
        UIManager.Instance.ShowFeedback(correct);

        _invoke(nameof(NextQuestion), 1.5f);
    }
}