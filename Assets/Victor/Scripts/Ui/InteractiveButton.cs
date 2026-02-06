using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractiveButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 m_originalScale;

    private void Start()
    {
        m_originalScale =  transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(m_originalScale.x, m_originalScale.y, m_originalScale.z) * 1.2f ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = m_originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.localScale = m_originalScale;
    }
}