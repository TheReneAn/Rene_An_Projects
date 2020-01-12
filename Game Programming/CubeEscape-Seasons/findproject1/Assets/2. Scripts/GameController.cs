using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    [SerializeField] private Animator _animator;
    public GameObject MainCanvas;

    public GameObject MainMenu;
    public GameObject GameSelection;
    public GameObject OptionPanel;


    public Animator SmAnimator => _animator;

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

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {

    }

    public async void StartIntro()
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        _animator.SetTrigger("intro_done");
    }
}
