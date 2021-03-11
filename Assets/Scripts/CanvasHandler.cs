using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    // Public -------------------------------------------------------------

    [SerializeField]
    public AudioClip MenuSelectSoundEffect;

    // Members ------------------------------------------------------------

    // UI Elements
    private Text m_PointsText;
    private Button m_AddPointsButton;
    private AudioSource m_AudioPlayer;

    // Values
    private int m_CurrentPoints;

    // Initialization -----------------------------------------------------

    private void Awake()
    {
        // Get references to entities in the scene
        m_PointsText = transform.Find("PointsValue").GetComponent<Text>();
        m_AddPointsButton = transform.Find("AddPointsButton").GetComponent<Button>();
        m_AudioPlayer = transform.Find("AudioPlayer").GetComponent<AudioSource>();

        // Bind event to button click
        m_AddPointsButton.onClick.AddListener(OnAddPointsClicked);
    }

    private void Start()
    {
        // Set initial value for points
        m_CurrentPoints = 0;

        // Update Points UI
        SetPoints(0);
    }

    // Events ------------------------------------------------------------

    private void OnAddPointsClicked()
    {
        // Add ten points
        ChangePoints(10);

        // Play a sound effect
        if (MenuSelectSoundEffect)
            m_AudioPlayer.PlayOneShot(MenuSelectSoundEffect, 0.5f);
    }

    // Interface ---------------------------------------------------------

    public void ChangePoints(int deltaPoints)
    {
        SetPoints(m_CurrentPoints + deltaPoints);
    }

    public void SetPoints(int points)
    {
        // Change current points
        m_CurrentPoints = points;

        // Update UI
        m_PointsText.text = m_CurrentPoints.ToString();
    }

    public int GetPoints()
    {
        return m_CurrentPoints;
    }
}
