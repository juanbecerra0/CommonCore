using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Members ------------------------------------------------------------

    // Components
    private Text m_Value;

    // Values
    private Vector3 m_InitialLocation = new Vector3();
    private Vector3 m_CurrentOffset = new Vector3();

    // Initialization -----------------------------------------------------

    private void Awake()
    {
        m_InitialLocation = transform.position;
        m_Value = transform.Find("ObjectText").GetComponent<Text>();
    }

    // Interface ----------------------------------------------------------

    public void SetValue(uint value) => m_Value.text = value.ToString();

    // Events -------------------------------------------------------------

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_CurrentOffset = transform.position - Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = m_CurrentOffset + Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = m_InitialLocation;
    }
}
