using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySouth : DisplayBase
{

    [SerializeField] private BoxCollider2D _doorLeft;
    [SerializeField] private BoxCollider2D _doorRight;
    [SerializeField] private BoxCollider2D _doorLeftOpen;
    [SerializeField] private BoxCollider2D _doorRightOpen;

    [SerializeField] private BoxCollider2D _birdFood;
    [SerializeField] private BoxCollider2D _pan;
    [SerializeField] private BoxCollider2D _wood;


    // Start is called before the first frame update
    void Start()
    {
        category = DisplayCategory.South;
        InitDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_doorLeft.OverlapPoint(wp))
            {
                _doorLeft.gameObject.SetActive(false);
                _doorLeftOpen.gameObject.SetActive(true);
            }
            else if (_doorRight.OverlapPoint(wp))
            {
                _doorRight.gameObject.SetActive(false);
                _doorRightOpen.gameObject.SetActive(true);
            }
            else if (_doorLeftOpen.OverlapPoint(wp))
            {
                _doorLeftOpen.gameObject.SetActive(false);
                _doorLeft.gameObject.SetActive(true);
            }
            else if (_doorRightOpen.OverlapPoint(wp))
            {
                _doorRightOpen.gameObject.SetActive(false);
                _doorRight.gameObject.SetActive(true);
            }
            else if (_birdFood.OverlapPoint(wp))
            {
                Debug.Log("click bird food");
            }
            else if (_pan.OverlapPoint(wp))
            {
                Debug.Log("click pan");
            }
            else if (_wood.OverlapPoint(wp))
            {
                Debug.Log("click wood");
            }
        }

    }
}
