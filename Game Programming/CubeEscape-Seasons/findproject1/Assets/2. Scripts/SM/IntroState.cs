using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroState : StateMachineBehaviour
{
    [SerializeField] private Sprite _introSplash;
    [SerializeField] private GameObject _splashPrefab;

    private GameObject _splash;

    private GameController _controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("intro");
        _controller = animator.GetComponent<GameController>();

        _splash = Instantiate(_splashPrefab, _controller.MainCanvas.transform);
        var splashImage = _splash.GetComponent<Image>();
        splashImage.sprite = _introSplash;


        _controller.StartIntro();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(_splash);
    }
}
