using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionButton;

    // button sound clip array
    public AudioClip[] audioClip;

    // Start is called before the first frame update
    void Start()
    {
        _playButton.onClick.AddListener(GameStart);
        _optionButton.onClick.AddListener(OptionButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void GameStart()
    {
        Debug.Log("start button clicked");
        // button sound create
        SoundManager.Instance.PlaySfx(audioClip[0]);
        GameController.Instance.SmAnimator.SetTrigger("goto_game_select");
    }

    void OptionButtonClicked()
    {
        Debug.Log("option button clicked");
        // button sound create
        SoundManager.Instance.PlaySfx(audioClip[0]);
        GameController.Instance.SmAnimator.SetTrigger("option_open");
    }
}
