using System.Collections;
using System.Collections.Generic;
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
    }

    private void Start()
    {
        SetPoints(0);
        InitProgress(10);

        // TODO: test
        ChangePoints(500);
        ChangeProgress(4);
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
