using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName ="New question")]

public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string question = "Enter new question";
    [SerializeField] string[] answers = new string[4];

    [SerializeField] int correctAnswerIndex;


    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswer()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
   
}
