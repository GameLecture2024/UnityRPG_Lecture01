using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }
}
