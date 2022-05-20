using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAnimationState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);

        PlayerController player = animator.transform.parent.GetComponent<PlayerController>();
        if(player != null && clips.Length > 0)
        {
            AnimatorClipInfo clipeInfo = clips[0];
            float multiplier = clipeInfo.clip.length / player.RollDuration;
            animator.SetFloat(TagManager.RollMultiplier, multiplier);
        }
    }


}
