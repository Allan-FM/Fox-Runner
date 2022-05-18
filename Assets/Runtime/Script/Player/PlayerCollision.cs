using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerAnimationController playerAnimationController;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if(obstacle != null)
        {
            playerController.Die();
            playerAnimationController.Die();
        }
    }
}
