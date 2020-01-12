using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCeil : DisplayBase
{
    public GameObject Key;
    private BoxCollider2D coll_key;

    void Awake()
    {
        coll_key = Key.GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        category = DisplayCategory.Ceil;
        InitDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (coll_key.OverlapPoint(wp))
            {
                Debug.Log("coll_key touched");
                GameManager.Instance.TouchKey();
                Key.gameObject.SetActive(false);
            }
        }
    }
}
