using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSelectionController : MonoBehaviour
{
    [SerializeField] private Button _springButton;
    // button sound clip array
    public AudioClip[] audioClip;
    private void Awake()
    {
        _springButton.onClick.AddListener(SpringStart);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpringStart()
    {
        SoundManager.Instance.PlaySfx(audioClip[0]);
        GameController.Instance.SmAnimator.SetTrigger("goto_ingame");
    }

}
