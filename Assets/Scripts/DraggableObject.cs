using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Members ------------------------------------------------------------

    // Constants
    private const string TARGET_TAG = "QuestionTile";

    // Components
    private Camera m_MainCamera;
    private Text m_Value;

    // Values
    private Vector3 m_InitialLocation = new Vector3();
    private Vector3 m_CurrentOffset = new Vector3();

    // Initialization -----------------------------------------------------

    private void Awake()
    {
        m_MainCamera = Camera.main;
        m_InitialLocation = transform.position;
        m_Value = transform.Find("ObjectText").GetComponent<Text>();
    }

    // Interface ----------------------------------------------------------

    public void SetValue(uint value) => m_Value.text = value.ToString();
    public uint GetValue() => (uint)System.Int32.Parse(m_Value.text);

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
        // Temporarely move draggable object back to initial location
        transform.position = m_InitialLocation;

        // Perform a raycast at mouse pointer position
        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward, 10.0f);

        // Check if raycast hit was a non-static question tile
        if (hit && hit.transform.gameObject.tag.Equals(TARGET_TAG))
        {
            // Get a reference to this question tile
            QuestionTile tile = hit.transform.GetComponent<QuestionTile>();

            // If non-static, slot this value
            if (!tile.IsStatic())
            {
                transform.position = tile.transform.position;
                tile.SlotValue(GetValue());
            }
        }
    }
}
