using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour , IBeginDragHandler , IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;            //for dragging item

    [SerializeField] CanvasGroup canvasGroup;        //for blocking raycast and reduce alpha while drag
    [SerializeField] Vector2 orignalPosition;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GameController2048.ScaleFactor;  //follow position
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts  =   true;
        if(eventData.pointerEnter ==null )
        {
            rectTransform.anchoredPosition = orignalPosition;
        }
    }
    public void PlaceBack()
    {
        rectTransform.anchoredPosition = orignalPosition;
    }
}
