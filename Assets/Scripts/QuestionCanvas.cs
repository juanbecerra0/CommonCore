using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    // Members ------------------------------------------------------------

    // Progress Panel Components
    private Text m_PointsValue;
    private Text m_ProgressValue;
    private Slider m_ProgressBar;

    // Question Panel Components
    private QuestionPrompt m_QuestionPrompt;
    private QuestionBox m_QuestionBox;

    // Navigation Panel Components


    // Drag Panel Components
    private DraggableObject[] m_DraggableObjects;

    // Members
    private uint m_CurrentPoints;
    private uint m_CurrentProgress;
    private uint m_TotalQuestions;

    // Initialization -----------------------------------------------------

    private void Awake()
    {
        // Retreive references to indivual panels
        Transform progressPanel = transform.Find("ProgressPanel");
        Transform questionPanel = transform.Find("QuestionPanel");
        Transform navigationPanel = transform.Find("NavigationPanel");
        Transform dragPanel = transform.Find("DragPanel");

        // Init progress panel
        m_PointsValue = progressPanel.Find("PointsValue").GetComponent<Text>();
        m_ProgressValue = progressPanel.Find("ProgressValue").GetComponent<Text>();
        m_ProgressBar = progressPanel.Find("ProgressBar").GetComponent<Slider>();

        // Init question panel
        m_QuestionPrompt = questionPanel.Find("QuestionPrompt").GetComponent<QuestionPrompt>();
        m_QuestionBox = questionPanel.Find("QuestionBox").GetComponent<QuestionBox>();

        // Init navigation panel


        // Init drag panel
        m_DraggableObjects = dragPanel.GetComponentsInChildren<DraggableObject>();
    }

    private void Start()
    {
        // TODO: Test values

        // Set up default amount of points and total questions
        SetPoints(0);
        InitProgress(10);

        // Change the current amount of points and current question value
        ChangePoints(500);
        ChangeProgress(6);

        // Set up current question
        SetUpQuestion(123, 321, 6);
    }

    private void SetUpQuestion(uint num1, uint num2, uint numBlanks)
    {
        // Validate input
        if (num1 < 10 || num1 > 999 || num2 < 10 || num2 > 999 || numBlanks < 1 || numBlanks > 6)
        {
            Debug.LogError("Cannot init question box with these values: " + num1 + ", " + num2 + ", " + numBlanks);
            return;
        }

        // Initialize the grid of values
        uint[] draggableValues = m_QuestionBox.Init(num1, num2, numBlanks);

        // Shuffle the draggable objects array
        System.Random random = new System.Random();
        for (int i = m_DraggableObjects.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(0, i + 1);

            DraggableObject temp = m_DraggableObjects[i];
            m_DraggableObjects[i] = m_DraggableObjects[randomIndex];
            m_DraggableObjects[randomIndex] = temp;
        }

        // Set values of each draggable object
        for (int i = 0; i < m_DraggableObjects.Length; i++)
            m_DraggableObjects[i].SetValue(draggableValues[i]);
    }

    private void InitProgress(uint totalQuestions)
    {
        m_CurrentProgress = 0;
        m_TotalQuestions = totalQuestions;
    }

    // Interface ---------------------------------------------------------

    public void SetPoints(uint points)
    {
        m_CurrentPoints = points;
        m_PointsValue.text = m_CurrentPoints.ToString();
    }

    public void ChangePoints(uint deltaPoints)
    {
        uint newPointsValue = (uint)Mathf.Clamp(m_CurrentProgress + deltaPoints, 0, float.MaxValue);
        SetPoints(newPointsValue);
    }

    public void SetProgress(uint progress)
    {
        m_CurrentProgress = (uint)Mathf.Clamp(progress, 0, m_TotalQuestions); ;
        m_ProgressValue.text = m_CurrentProgress.ToString() + " / " + m_TotalQuestions.ToString();
        m_ProgressBar.value = (float)m_CurrentProgress / m_TotalQuestions;
    }

    public void ChangeProgress(uint deltaProgress)
    {
        uint newProgress = (uint)Mathf.Clamp(m_CurrentProgress + deltaProgress, 0, m_TotalQuestions);
        SetProgress(newProgress);
    }

}
