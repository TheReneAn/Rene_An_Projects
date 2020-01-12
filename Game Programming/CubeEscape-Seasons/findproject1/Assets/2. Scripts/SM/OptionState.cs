using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionState : StateMachineBehaviour
{
    private GameController _controller;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller = animator.GetComponent<GameController>();
        _controller.OptionPanel.SetActive(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _controller.OptionPanel.SetActive(false);
    }

}
