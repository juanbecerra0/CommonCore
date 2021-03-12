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
    private const uint NUM_DRAGGABLE_OBJECTS = 4;   // By default, have four draggable components

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
        SetPoints(0);
        InitProgress(10);

        // TODO: test
        ChangePoints(500);
        ChangeProgress(4);

        // Init question box. Returns n number of values to depict as draggable values.
        System.Random rand = new System.Random();
        uint[] draggableValues = m_QuestionBox.Init((uint)rand.Next(10, 999), (uint)rand.Next(10, 999), 4);
        InitDraggableObjects(draggableValues);
    }

    private void InitProgress(uint totalQuestions)
    {
        m_CurrentProgress = 0;
        m_TotalQuestions = totalQuestions;
    }

    private void InitDraggableObjects(uint[] values)
    {
        if (values.Length != NUM_DRAGGABLE_OBJECTS)
        {
            Debug.LogError("Attempted to pass " + values.Length + " values into list of size " + NUM_DRAGGABLE_OBJECTS + "!");
            return;
        }

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
            m_DraggableObjects[i].SetValue(values[i]);
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
