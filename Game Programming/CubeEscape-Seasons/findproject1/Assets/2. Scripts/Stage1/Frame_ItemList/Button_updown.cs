using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Button_updown : MonoBehaviour, IPointerDownHandler {

    [SerializeField]
    private Frame_ItemList frame_itemlist;
    [SerializeField]
    private bool isDownButton;

    public void OnPointerDown (PointerEventData eventData) {
        if(isDownButton) {
            frame_itemlist.ButtonDownIsPressed();
        }
        else {
            frame_itemlist.ButtonUpIsPressed();
        }
    }
}
