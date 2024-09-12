using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionSeen = 0;


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int GetCorrectAnswer()
    {
        return correctAnswers;
    }
    public int GetQuestionSeen()
    {
        return questionSeen;
    }

    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public void IncrementQuestionSeen() 
    {
        questionSeen++;
    }
    // (x/x)*100=z%

    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionSeen * 100);
    }
}
