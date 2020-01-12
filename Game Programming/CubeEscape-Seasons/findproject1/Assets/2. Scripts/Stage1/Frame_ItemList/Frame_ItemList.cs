using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Frame_ItemList : MonoBehaviour
{
    private ScrollRect scrollRect;
    private bool mouseDown, buttonDown, buttonUp;

    void Start() {
        scrollRect = GetComponent<ScrollRect>();
    }

    void Update() {
        if(mouseDown) {
            if (buttonDown) {
                ScrollDown();
            }
            else if (buttonUp) {
                ScrollUp();
            }
        }
    }

    public void ButtonDownIsPressed()
    {
        mouseDown = true;
        buttonDown = true;
    }

    public void ButtonUpIsPressed()
    {
        mouseDown = true;
        buttonUp = true;
    }

    private void ScrollDown() {
        if (Input.GetMouseButtonUp(0)) {
            mouseDown = false;
            buttonDown = false;
        }
        else {
            scrollRect.verticalNormalizedPosition -= 0.01f;
        }
    }

    private void ScrollUp() {
        if (Input.GetMouseButtonUp(0)) {
            mouseDown = false;
            buttonUp = false;
        }
        else {
            scrollRect.verticalNormalizedPosition += 0.01f;
        }
    }
}
