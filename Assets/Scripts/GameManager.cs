using System;
using IO;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        MultimodalInputManager.Instance.EnableInput(MultimodalInputManager.InputType.Navigation);

        QuestionManager.Instance.PopulateCurrentOptions();
        QuestionManager.Instance.DisplayQuestion();
        
        currentState = GameState.WaitingForInput;
    }

    public void SubmitAnswer(string answer)
    {
        if (currentState != GameState.WaitingForInput) return;
        
        var correct = QuestionManager.Instance.CheckAnswer(answer);
        
        currentState = GameState.ShowFeedback;
        MultimodalInputManager.Instance.DisableInput(MultimodalInputManager.InputType.Navigation);
        QuestionManager.Instance.ShowFeedback(correct);

        _invoke(nameof(NextQuestion), 1.5f);
    }
}