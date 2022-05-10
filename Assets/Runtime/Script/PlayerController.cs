using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 15;
    [SerializeField] private float fowardSpeed = 10;
    [SerializeField] private float laneDistanceX = 4;

    private Vector3 initialPositon;
    private float targetPositionX;

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
}
