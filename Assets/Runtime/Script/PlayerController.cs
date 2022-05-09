using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.03f;
    [SerializeField] private float fowardSpeed = 0.5f;
    [SerializeField] private float targetPositionX = 3f;
    private void Update()
    {
        Vector3 targetPosition = transform.position;
        if(Input.GetKey(KeyCode.A))
        {
            targetPositionX += Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPositionX += Mathf.Lerp(transform.position.x, -targetPositionX, Time.deltaTime * horizontalSpeed);

        }
        targetPosition += Vector3.forward * fowardSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }
}
