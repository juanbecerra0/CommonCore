using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuestionCanvas : MonoBehaviour
{
    // Public components --------------------------------------------------

    [SerializeField]
    public AudioClip ButtonSound;
    [SerializeField]
    public AudioClip TickSound;

    // Members ------------------------------------------------------------

    // Constants
    private readonly Color CORRECT_COLOR = new Color(0.2f, 0.8f, 0.2f);
    private readonly Color INCORRECT_COLOR = new Color(0.8f, 0.2f, 0.2f);

    // Progress Panel Components
    private Text m_PointsValue;
    private Text m_ProgressValue;
    private Slider m_ProgressBar;

    // Question Panel Components
    private QuestionPrompt m_QuestionPrompt;
    private QuestionBox m_QuestionBox;

    // Navigation Panel Components
    private Button m_ExitButton;
    private Button m_ResetButton;
    private Button m_CheckButton;
    private Button m_NextButton;

    // Drag Panel Components
    private DraggableObject[] m_DraggableObjects;

    // Other components
    private AudioSource m_AudioSource;
    private Text m_ConsoleText;

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
        m_ExitButton = navigationPanel.Find("ExitButton").GetComponent<Button>();
        m_ExitButton.onClick.AddListener(OnExit);
        m_ResetButton = navigationPanel.Find("ResetButton").GetComponent<Button>();
        m_ResetButton.onClick.AddListener(OnReset);
        m_CheckButton = navigationPanel.Find("CheckButton").GetComponent<Button>();
        m_CheckButton.onClick.AddListener(OnCheck);
        m_NextButton = navigationPanel.Find("NextButton").GetComponent<Button>();
        m_NextButton.onClick.AddListener(OnNext);
        m_ConsoleText = navigationPanel.Find("ConsoleText").GetComponent<Text>();
        m_ConsoleText.text = "";

        // Init drag panel
        m_DraggableObjects = dragPanel.GetComponentsInChildren<DraggableObject>();

        // Init other components
        m_AudioSource = GetComponent<AudioSource>();
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

    // Navigation Events -------------------------------------------------

    private void OnNext()
    {
        PlayClickSound();


    }

    private void OnCheck()
    {
        PlayClickSound();

        if (m_QuestionBox.IsCorrect())
        {
            m_ConsoleText.text = "Correct!";
            m_ConsoleText.color = CORRECT_COLOR;
        }
        else
        {
            m_ConsoleText.text = "Incorrect!";
            m_ConsoleText.color = INCORRECT_COLOR;
        }
    }

    private void OnReset()
    {
        PlayClickSound();

        // Reset every draggable object and tile
        foreach (DraggableObject dobj in m_DraggableObjects)
            dobj.ResetInstance();

        m_QuestionBox.ResetInstance();
    }

    private void OnExit()
    {
        PlayClickSound();

        // Simulate web browser "back" function
        JSFunctions.ReturnToPreviousPage();
    }

    private void PlayClickSound()
    {
        if (ButtonSound)
            m_AudioSource.PlayOneShot(ButtonSound, 0.8f);
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
