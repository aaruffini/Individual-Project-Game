using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class PlayerController : MonoBehaviour
{
    [Header("Refrerences")]
    public Rigidbody rb;
    public Transform head;
    public Camera camera;

    [Header("Configurations")]
    public float walkSpeed;
    public float runSpeed;



    // called at 'first frame update' 
    void Start()
    {

        
    }
    // called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
     
        Vector3 newVelocity = Vector3.up * rb.linearVelocity.y; // retains vertical of ridgid vody
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;
        rb.linearVelocity = newVelocity;
    }
    
}
