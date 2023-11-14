using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler 
{
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null )  
        {
            if (eventData.pointerDrag.transform.parent.name==this.name)  // Same Cell Placment Case
            {
                eventData.pointerDrag.gameObject.GetComponent<DragDrop>().PlaceBack();
                return; 
            }
                 if (this.gameObject.transform.childCount == 0) //Empty Cell Placemnt Case
                {
                    eventData.pointerDrag.transform.SetParent(this.gameObject.transform);
                    eventData.pointerDrag.gameObject.GetComponent<DragDrop>().PlaceBack();

                    GameController2048.instance.AddNewLineBelow();
                    GameController2048.instance.BringAllDown();
                }
                else if (eventData.pointerDrag.gameObject.GetComponent<Fill2048>().value == this.transform.GetChild(0).gameObject.GetComponent<Fill2048>().value) //Cell holding same number Case
                {
                    Destroy(this.gameObject.transform.GetChild(0).gameObject);
                    eventData.pointerDrag.transform.SetParent(this.gameObject.transform);
                    this.gameObject.transform.GetChild(1).GetComponent<Fill2048>().UpdateMerge();
                    GameController2048.instance.BringAllDown();
                }
            else
            {
                eventData.pointerDrag.gameObject.GetComponent<DragDrop>().PlaceBack();
            }
        }
    }
}
