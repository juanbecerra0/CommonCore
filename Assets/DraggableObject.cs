using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Members ------------------------------------------------------------

    private Vector3 m_CurrentOffset = new Vector3();

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
        
    }
}
