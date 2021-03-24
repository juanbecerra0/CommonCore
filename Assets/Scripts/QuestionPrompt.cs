using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPrompt : MonoBehaviour
{
    // Members ------------------------------------------------------------

    // UI Elements
    private Text m_QuestionText;
    private Button m_TextToSpeechButton;

    // Initialization -----------------------------------------------------

    private void Awake()
    {
        // Get references to entities in the object
        m_QuestionText = transform.Find("QuestionText").GetComponent<Text>();
        m_TextToSpeechButton = transform.Find("TextToSpeechButton").GetComponent<Button>();

        // Bind event to button clock
        m_TextToSpeechButton.onClick.AddListener(OnTextToSpeechClicked);
    }

    // Events

    private void OnTextToSpeechClicked()
    {
        JSFunctions.TextToSpeech(m_QuestionText.text.ToString());
    }

}
