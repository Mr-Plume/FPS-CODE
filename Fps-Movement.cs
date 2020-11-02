/* I update this code */


using System;
using UnityEngine;

namespace DefaultNamespace.Scenes
{
    public class Movement : MonoBehaviour
    {
        private float x, z; // x and y value for Vertical And Horizontal move
        private bool jump , crouch , crouchend;
        private Vector3 jumpVector = new Vector3(0 , 50 , 0); // jump vector
        private Vector3 crouchScale = new Vector3(1 , 0.5f , 1); // crouch scale
        private Vector3 defaultScale = new Vector3(1 , 1 , 1); // default scale
        [SerializeField] private float Jumpspeed,speed; 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool jumpReady;
        [SerializeField] private GameObject floor; // add a your floor for jumping
        private void Awake()
        {
            // I freeze rotation in order not to fall.
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void Start()
        {
            speed = 1; // mainly speed value
            Jumpspeed = 5; // mainly jump speed value
            
            // recommended setting (don't close gravity)
            rb.useGravity = true;
        }

        private void Update()
        {
            // Input Function
            MyInputF();  
            
            // Crouch Function
            CrouchF();
        }

        private void FixedUpdate()
        {
            // Movement Horizontal and Vertical Function
            MovementF();
            
            //extra gravity for realistic movement
            rb.AddForce(0 , -0.1f , 0); // change by yourself
        }

        private void MyInputF()
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            jump = Input.GetKeyDown(KeyCode.Space);
            if(jump && jumpReady) {rb.AddForce(jumpVector * Jumpspeed);} // if jump and jumpReady is true can jump

            crouch = Input.GetKey(KeyCode.LeftControl); // crouch start
            crouchend = Input.GetKeyUp(KeyCode.LeftControl); // crouch end
        }

        private void MovementF()
        {
            rb.velocity += new Vector3(x, rb.velocity.y, z) * (Time.deltaTime * speed); // always use Time.deltaTime for smooth movement
        }

        // Crouch Function
        private void CrouchF()
        {
            if (crouch) // if crouch is true set a transform.localScale
            {
                transform.localScale = crouchScale;
            }
            
            if(crouchend)
            {
                transform.localScale = defaultScale;
            }
        }

        private void OnCollisionEnter(Collision other) // can jump if it touches the ground
        {
            if (other.gameObject == floor)
            {
                jumpReady = true;
            }
        }

        private void OnCollisionExit(Collision other) // but if not touches, can't jump
        {
            jumpReady = false;
        }
    }
    }
    
    // Sliding is coing soon.
