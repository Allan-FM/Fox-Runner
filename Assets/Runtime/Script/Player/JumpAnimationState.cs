using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAnimationState : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorClipInfo[] clips = animator.GetNextAnimatorClipInfo(layerIndex);
        if(clips.Length > 0)
        {
            AnimatorClipInfo jumpClipInfo = clips[0];
            PlayerController player = animator.transform.parent.GetComponent<PlayerController>();

            float multiplier = jumpClipInfo.clip.length / player.JumpDuration;
            animator.SetFloat(TagManager.JumpMultiplier, multiplier);
        }
    }    
}
