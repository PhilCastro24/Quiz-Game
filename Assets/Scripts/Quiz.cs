using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

    [Header("Questions")]
    [SerializeField] List<QuestionSO> Questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    QuestionSO currentQuestion;


    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;


    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer-related things")]
    [SerializeField] Image timerImage;

    [Header("Score-related things")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    Timer timer;



    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {

        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
        
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.ResetTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore();
    }

    void GetNextQuestion()
    {
        if (Questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.Getquestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();

            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, Questions.Count);
        currentQuestion = Questions[index];

        if (Questions.Contains(currentQuestion))
        {
            Questions.Remove(currentQuestion);
        }
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;

        if (index == currentQuestion.GetCorrectAnswer())
        {
            questionText.text = "Correct!";

            buttonImage = answerButtons[index].GetComponent<Image>();

            buttonImage.sprite = correctAnswerSprite;

            scoreKeeper.IncrementCorrectAnswers();

            //Debug.Log("Button with Index" + index + "was pressed");
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswer();

            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);

            questionText.text = "Sorry, the correct answer was: " + correctAnswer;

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

            buttonImage.sprite = correctAnswerSprite;

            scoreKeeper.GetQuestionSeen();

            //Debug.Log("Button with Index" + index + "was pressed");
        }
    }
}
