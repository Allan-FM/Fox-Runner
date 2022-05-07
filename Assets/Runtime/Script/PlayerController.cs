using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 0.03f;
    [SerializeField] private float fowardSpeed = 0.5f;
    private void Update()
    {
        
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * horizontalSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * horizontalSpeed;
        }
        transform.position += Vector3.forward * fowardSpeed;
    }
}
