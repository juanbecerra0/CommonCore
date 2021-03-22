using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizHandler : MonoBehaviour
{
    // Members

    private QuestionCanvas m_Canvas;

    private int m_QuestionIndex;

    // Question structs

    struct Question
    {
        public uint Num1;
        public uint Num2;
        public uint Blanks;

        public Question (uint Num1, uint Num2, uint Blanks)
        {
            this.Num1 = Num1;
            this.Num2 = Num2;
            this.Blanks = Blanks;
        }
    }

    private Question[] m_Questions = new Question[]
    {
        new Question(12, 24, 2),
        new Question(42, 63, 2),
        new Question(83, 17, 3),
        new Question(152, 13, 3),
        new Question(43, 230, 3),
        new Question(430, 11, 4),
        new Question(53, 542, 5),
        new Question(120, 240, 5),
        new Question(123, 321, 6),
        new Question(360, 732, 6)
    };

    // Initialization

    private void Awake()
    {
        m_Canvas = GameObject.Find("QuestionCanvas").GetComponent<QuestionCanvas>();
    }

    private void Start()
    {
        // Set up values

        // Set up default amount of points and total questions
        m_Canvas.SetPoints(0);
        m_Canvas.InitProgress(10);

        // Change the current amount of points and current question value
        m_Canvas.ChangePoints(0);
        m_Canvas.ChangeProgress(0);

        // Set up the first question
        m_QuestionIndex = 0;
        NextQuestion();
    }

    private void DisplayQuestion(Question question) => m_Canvas.SetUpQuestion(question.Num1, question.Num2, question.Blanks);

    // Button API

    public void NextQuestion()
    {
        if (m_QuestionIndex < m_Questions.Length - 1)
            DisplayQuestion(m_Questions[m_QuestionIndex++]);
        else
            Debug.Log("Reached end!");
    }

}
