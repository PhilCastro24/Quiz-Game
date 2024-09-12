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

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer-related things")]
    [SerializeField] Image timerImage;

    Timer timer;

    int correctAnswerIndex;

    QuestionSO question;


    void Start()
    {
        GetNextQuestion();
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        if (timer != null && timerImage != null)
        {
            timerImage.fillAmount = timer.fillFraction;
        }
        else
        {
            Debug.Log("Timer wasn´t found");
        }
    }

    public void OnAnswerSelected(int index)
    {
        Image buttonImage;

        if (index == question.GetCorrectAnswer())
        {
            questionText.text = "Correct!";

            buttonImage = answerButtons[index].GetComponent<Image>();

            buttonImage.sprite = correctAnswerSprite;

            Debug.Log("Button with Index" + index + "was pressed");
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswer();

            string correctAnswer = question.GetAnswer(correctAnswerIndex);

            questionText.text = "Sorry, the correct answer was: \n" + correctAnswer;

            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();

            buttonImage.sprite = correctAnswerSprite;

            Debug.Log("Button with Index" + index + "was pressed");
        }

        SetButtonState(false);
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        questionText.text = question.Getquestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
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
}
