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
    [SerializeField] private float jumpLerpSpeed = 10f;

    [Header("Roll")]
    [SerializeField] private float rollDistaceZ = 5f;
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Collider rollCollider;


    private Vector3 initialPositon;
    private float targetPositionX;

    public bool IsJumping { get; private set; }
    public float JumpDuration => jumpDisatanceZ / fowardSpeed;
    private float jumpStartZ;

    public bool IsRolling { get; private set; }
    public float RollDuration => rollDistaceZ / fowardSpeed;
    private float rollStartZ;

    private float LeftLaneX => initialPositon.x - laneDistanceX;
    private float RigthLaneX => initialPositon.x + laneDistanceX;

    private bool canJump => !IsJumping;
    private bool canRoll => !IsRolling;

    private void Awake()
    {
        initialPositon = transform.position;
        StopRoll();
    }
    private void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessFowardMovement();
        ProcessRoll();

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
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            StarJump();
        }
        if (Input.GetKeyDown(KeyCode.S) && canRoll)
        {
            StartRoll();
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
    private void StarJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
    }
    private void StopJump()
    {
        IsJumping = false;
    }
    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {

            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDisatanceZ;
            if (jumpPercent >= 1)
            {
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeigth;
            }

        }
        float targetPositionY = initialPositon.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }
    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistaceZ;
            if(percent >= 1)
            {
                StopRoll();
            }
        }
    }
    private void StartRoll()
    {
        rollStartZ = transform.position.z;
        IsRolling = true;
        regularCollider.enabled = false;
        rollCollider.enabled = true;

        StopJump();
    }
    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.enabled = true;
        rollCollider.enabled = false;

    }
    public void Die()
    {
        fowardSpeed = 0;
        StopRoll();
        StopJump();
    }
}
