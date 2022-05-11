using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 15;
    [SerializeField] private float fowardSpeed = 10;
    [SerializeField] private float laneDistanceX = 4;
    [Header("Jump")]
    [SerializeField] private float jumpDisatanceZ = 5;
    [SerializeField] private float jumpHeigth = 2;


    private Vector3 initialPositon;
    private float targetPositionX;
    private bool isJumping;
    private float jumpStartZ;
    private float LeftLaneX => initialPositon.x - laneDistanceX;
    private float RigthLaneX => initialPositon.x + laneDistanceX;

    private void Awake()
    {
        initialPositon = transform.position;
    }
    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessFowardMovement();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if(Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            isJumping = true;
            jumpStartZ = transform.position.z;

        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RigthLaneX);
    }
    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }
    private float ProcessFowardMovement()
    {
        return transform.position.z + fowardSpeed * Time.deltaTime;
    }
    private float ProcessJump()
    {
        float deltaY = 0;
        if (isJumping)
        {
           
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDisatanceZ;
            if(jumpPercent >= 1)
            {
                isJumping = false;
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeigth;
            }

        }
        return initialPositon.y + deltaY;
    }
}
