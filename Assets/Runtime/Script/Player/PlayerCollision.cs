using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
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
            gameMode.OnGameOver();
        }
    }
}
