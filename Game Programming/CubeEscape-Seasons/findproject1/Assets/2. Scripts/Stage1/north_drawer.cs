using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class north_drawer : MonoBehaviour
{
    public GameObject Compartment1;
    public GameObject Compartment2;
    public GameObject Compartment3;
    public GameObject Compartment4;

    private BoxCollider2D _colThis;
    private BoxCollider2D[] _colCompartments = new BoxCollider2D[4];
    private bool[] _compartmentsOpen = new bool[4];
    

    void Awake()
    {
        _colThis = this.GetComponent<BoxCollider2D>();
        _colCompartments[0] = Compartment1.GetComponent<BoxCollider2D>();
        _colCompartments[1] = Compartment2.GetComponent<BoxCollider2D>();
        _colCompartments[2] = Compartment3.GetComponent<BoxCollider2D>();
        _colCompartments[3] = Compartment4.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);            
            if (_colThis.OverlapPoint(wp))
            {
                
            }
            else if (_colCompartments[0].OverlapPoint(wp))
            {
                ClickCompartment(0);
            }
            else if (_colCompartments[1].OverlapPoint(wp))
            {
                ClickCompartment(1);
            }
            else if (_colCompartments[2].OverlapPoint(wp))
            {
                ClickCompartment(2);
            }
            else if (_colCompartments[3].OverlapPoint(wp))
            {
                ClickCompartment(3);
            }
        }
    }

    public void ResetDrawer()
    {
        for(int i=0; i<4; i++)
        {
            if(_compartmentsOpen[i])
            {
                ClickCompartment(i);
            }
        }
    }

    void ClickCompartment(int index)
    {
        var position = _colCompartments[index].gameObject.transform.localPosition;
        if (_compartmentsOpen[index])
        {
            position.y += 1.26f;
        }
        else
        {
            position.y -= 1.26f;
        }
        _colCompartments[index].gameObject.transform.localPosition = position;
        _compartmentsOpen[index] = !_compartmentsOpen[index];
    }


}
