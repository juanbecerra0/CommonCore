using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionTile : MonoBehaviour
{
    // Public attributes
    [SerializeField]
    public uint X_Index;
    [SerializeField]
    public uint Y_Index;

    // Components
    private Text m_Value;
    private Image m_BackgroundImage;

    // Initalization

    private void Awake()
    {
        m_Value = transform.Find("ObjectText").GetComponent<Text>();
        m_BackgroundImage = GetComponent<Image>();
    }

    // Interface

    public void Init(bool isStatic, uint value)
    {
        if (!isStatic)
        {
            m_Value.text = "";
        }
        else
        {
            m_Value.text = value.ToString();
            m_BackgroundImage.enabled = false;
        }
    }

    public void Show(bool show) => gameObject.SetActive(show);  

}
