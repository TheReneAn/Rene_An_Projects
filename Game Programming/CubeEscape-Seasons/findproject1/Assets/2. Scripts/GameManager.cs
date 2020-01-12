using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DisplayCategory { None = 0, North=1, East=2, South=3, West=4, Ceil =5}
public enum Direction { Left, Right, Up, Down }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public bool PoupOn = false;
    public bool CeilDirection = false;
    public DisplayCategory CurrentDirection = DisplayCategory.South;

    [SerializeField]
    private RoomController _roomController;

    public GameObject DisplayCeil;
    public GameObject DisplayEast;
    public GameObject DisplayWest;
    public GameObject DisplaySouth;
    public GameObject DisplayNorth;

    public Text Message;

    private Stage1Data _stage1Data;

    private DisplayNorth _north;

    private GameObject _lastPopup;
    private GameObject _lastRoom;

    public Stage1Data Data
    {
        get
        {
            return _stage1Data;
        }
    }

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }
        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        _north = DisplayNorth.GetComponent<DisplayNorth>();

    }


    // Start is called before the first frame update
    void Start()
    {
        _stage1Data = ScriptableObject.CreateInstance<Stage1Data>();
        Message.gameObject.SetActive(false);


        DisplayCeil.transform.localPosition = Vector3.zero;
        DisplayEast.transform.localPosition = Vector3.zero;
        DisplayWest.transform.localPosition = Vector3.zero;
        DisplaySouth.transform.localPosition = Vector3.zero;
        DisplayNorth.transform.localPosition = Vector3.zero;
        SetDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDirection(Direction direction)
    {
        if (PoupOn)
        {
            _lastPopup.SetActive(false);
            _lastRoom.SetActive(true);
            PoupOn = false;
            //_roomController.gameObject.SetActive(true);
            _roomController.Arrows.UpdateArrow();
        }
        else
        {
            switch (direction)
            {
                case Direction.Left:
                    CurrentDirection = CurrentDirection - 1;
                    CeilDirection = false;
                    break;
                case Direction.Right:
                    CurrentDirection = CurrentDirection + 1;
                    CeilDirection = false;
                    break;
                case Direction.Up:
                    if (CeilDirection)
                    {
                        CurrentDirection = CurrentDirection + 2;
                        CeilDirection = false;
                    }
                    else
                    {
                        CeilDirection = true;
                    }

                    break;
                case Direction.Down:
                    CeilDirection = false;
                    break;
            }

            if (CurrentDirection == DisplayCategory.None)
            {
                CurrentDirection = DisplayCategory.West;
            }
            if ((int)CurrentDirection > 4)
            {
                CurrentDirection = (DisplayCategory)(((int)CurrentDirection) % 4);
            }
            SetDisplay();
        }
        Debug.Log(CurrentDirection);
    }

    private void SetDisplay()
    {
        Message.gameObject.SetActive(false);


        DisplayCeil.SetActive(false);
        DisplayEast.SetActive(false);
        DisplayWest.SetActive(false);
        DisplaySouth.SetActive(false);
        DisplayNorth.SetActive(false);

        if(CeilDirection)
        {
            DisplayCeil.SetActive(true);
        }
        else
        {
            switch (CurrentDirection)
            {
                case DisplayCategory.East:
                    DisplayEast.SetActive(true);
                    break;
                case DisplayCategory.West:
                    DisplayWest.SetActive(true);
                    break;
                case DisplayCategory.North:
                    DisplayNorth.SetActive(true);
                    break;
                case DisplayCategory.South:
                    DisplaySouth.SetActive(true);
                    break;
            }
        }

        _roomController.Arrows.UpdateArrow();
    }


    public void TouchKey()
    {
        _stage1Data.MainKey = true;
        Message.gameObject.SetActive(true);
        Message.text = "Key!!";
    }

    public bool TouchLocker()
    {
        if(!_stage1Data.MainKey)
        {
            // no key

            //show message
            Message.gameObject.SetActive(true);
            Message.text = "You don't have a key";
            return false;
        }
        else
        {
            // clear

            Message.gameObject.SetActive(true);
            Message.text = "Clear!!!";
            return true;
        }
    }

    public void TouchDrawer()
    {
        //_roomController.gameObject.SetActive(false);
        _lastPopup = _north.Popup1;
        _lastRoom = _north.Room;

        _lastPopup.SetActive(true);
        _lastRoom.SetActive(false);
        PoupOn = true;
        _roomController.Arrows.UpdateArrow();
    }
}
