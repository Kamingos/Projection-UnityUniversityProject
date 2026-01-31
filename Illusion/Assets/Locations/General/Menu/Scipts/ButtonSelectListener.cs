using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<PointerEventData> OnButtonSelected;
    public event Action<PointerEventData> OnButtonDeselected;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnButtonSelected?.Invoke(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnButtonDeselected?.Invoke(eventData);
    }
}