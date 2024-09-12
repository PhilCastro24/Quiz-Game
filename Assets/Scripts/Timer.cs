using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Seconds to...")]
    [SerializeField] float secondsToCompleteQuestion = 30;
    [SerializeField] float secondsToShowAnswer = 10;

    float timerValue;

    public bool loadNextQuestion;

    public bool isAnsweringQuestion = false;

    public float fillFraction;

    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime; // timerValue = timerValue - time.deltatime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / secondsToCompleteQuestion;
            }
            else
            {
                timerValue = secondsToShowAnswer;
                isAnsweringQuestion = false;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / secondsToShowAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = secondsToCompleteQuestion;
                loadNextQuestion = true;
            }
        }

        Debug.Log("The current status is " + isAnsweringQuestion +
            " Amount of seconds to answer the question " + timerValue + " And the fill Fraction is" + fillFraction);
    }

    public void ResetTimer()
    {
        timerValue = 0;
    }
}
