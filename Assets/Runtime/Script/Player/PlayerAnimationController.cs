using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private Animator animator;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        animator.SetBool(TagManager.IsJumping, playerController.IsJumping);
        animator.SetBool(TagManager.IsRolling, playerController.IsRolling);
    }
    public void Die()
    {
        animator.SetTrigger(TagManager.DieTrigger);
    }
}
