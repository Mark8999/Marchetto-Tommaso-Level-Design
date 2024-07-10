using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class SimplePlayer : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] private Transform cam;

    Vector3 inputVector;
    Vector3 velocityVector;
    float angle;
    float input_x;
    float input_y;
    public int forceConst = 50;
    public float JumpForce =5;
    public bool Isgrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        inputVector = new Vector3(input_x, 0f, input_y).normalized;
        angle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        if (inputVector.magnitude >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
        if (Isgrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

}

    private void Jump()
    {
  
        rb.AddForce(Vector3.up*JumpForce,ForceMode.Impulse);
        Isgrounded = false;  
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Isgrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Isgrounded = false;
        }
    }
    private void FixedUpdate()
    {
        if (inputVector.magnitude >= 0.01f)
            velocityVector = transform.forward * speed * Time.fixedDeltaTime;
        else
            velocityVector = Vector3.zero;

        rb.velocity = new Vector3(velocityVector.x, rb.velocity.y, velocityVector.z);
      
        
        
    }
}


