using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public GameObject arrow_right;
    public GameObject arrow_left;
    public GameObject arrow_up;
    public GameObject arrow_down;

    private BoxCollider2D coll_right;
    private BoxCollider2D coll_left;
    private BoxCollider2D coll_up;
    private BoxCollider2D coll_down;

    void Awake()
    {
        coll_right = arrow_right.GetComponent<BoxCollider2D>();
        coll_left = arrow_left.GetComponent<BoxCollider2D>();
        coll_up = arrow_up.GetComponent<BoxCollider2D>();
        coll_down = arrow_down.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0))
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (coll_left.OverlapPoint(wp))
            {
                Debug.Log("left touched");
                GameManager.Instance.ChangeDirection(Direction.Left);
            }
            else if (coll_right.OverlapPoint(wp))
            {
                Debug.Log("right touched");
                GameManager.Instance.ChangeDirection(Direction.Right);
            }
            else if (coll_up.OverlapPoint(wp))
            {
                Debug.Log("up touched");
                GameManager.Instance.ChangeDirection(Direction.Up);
            }
            else if (coll_down.OverlapPoint(wp))
            {
                Debug.Log("down touched");
                GameManager.Instance.ChangeDirection(Direction.Down);
            }
        }        
    }

    public void UpdateArrow()
    {
        //arrow_left.SetActive(true);
        //arrow_right.SetActive(true);
        //arrow_up.SetActive(true);

        if (GameManager.Instance.PoupOn)
        {
            arrow_up.SetActive(false);
            arrow_down.SetActive(true);
        }
        else
        {
            arrow_up.SetActive(true);
            if (GameManager.Instance.CeilDirection)
            {
                arrow_down.SetActive(true);
            }
            else
            {
                arrow_down.SetActive(false);
            }
        }
    }
}
