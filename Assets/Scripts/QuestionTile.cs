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

    // Members
    private bool m_IsStatic;
    private uint m_SlottedValue = 0;

    // Initalization

    private void Awake()
    {
        m_Value = transform.Find("ObjectText").GetComponent<Text>();
        m_BackgroundImage = GetComponent<Image>();
    }

    // Interface

    public void Init(uint value) => Init(value, m_IsStatic);

    public void Init(uint value, bool isStatic)
    {
        m_IsStatic = isStatic;
        m_SlottedValue = 0;

        if (!isStatic)
        {
            m_Value.text = "";
            m_BackgroundImage.enabled = true;
        }
        else
        {
            m_Value.text = value.ToString();
            m_BackgroundImage.enabled = false;
        }
    }

    public void Show(bool show) => gameObject.SetActive(show);
    public bool IsShown() => isActiveAndEnabled;
    public uint GetValue() => (uint)System.Int32.Parse(m_Value.text);
    public bool IsStatic() => m_IsStatic;
    public void SlotValue(uint value) => m_SlottedValue = value;
    public bool IsCorrect()
    {
        if (m_IsStatic)
            return true;
        else
            return GetValue() == m_SlottedValue;
    }

}
