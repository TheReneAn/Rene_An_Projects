using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNorth : DisplayBase
{
    public GameObject Room;
    public GameObject Popup1;

    public GameObject Drawer;
    public GameObject Locker;
    public Sprite Lock_open;

    private BoxCollider2D _colDrawer;
    private BoxCollider2D _coll_locker;

    [SerializeField]
    private north_drawer _drawer;

    //popup -


    void Awake()
    {
        _coll_locker = Locker.GetComponent<BoxCollider2D>();
        _colDrawer = Drawer.GetComponent<BoxCollider2D>();



    }

    // Start is called before the first frame update
    void Start()
    {
        Room.SetActive(true);
        Popup1.SetActive(false);

        category = DisplayCategory.North;
        InitDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_coll_locker.OverlapPoint(wp))
            {
                Debug.Log("coll_locker touched");
                bool clear = GameManager.Instance.TouchLocker();

                if(clear)
                {
                    var sr = Locker.GetComponent<SpriteRenderer>();
                    sr.sprite = Lock_open;

                    _coll_locker.enabled = false;
                }
            }
            else if(_colDrawer.OverlapPoint(wp))
            {
                GameManager.Instance.TouchDrawer();
            }
        }
    }
}
