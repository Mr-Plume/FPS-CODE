/* I gonna add comment coming soon */

using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace DefaultNamespace // change namespace dont forget
{
    public class Movement : MonoBehaviour // change class name
    {
        private float x, z; // x and y value for Vertical And Horizontal move
        private bool jump, crouch, crouchend, isSlide, slideStart , crouchStart;
        private Vector3 jumpVector = new Vector3(0 , 50 , 0); // jump vector
        private Vector3 crouchScale = new Vector3(1 , 0.5f , 1); // crouch scale
        private Vector3 defaultScale = new Vector3(1 , 1 , 1); // default scale
        private RotateCamera rotateCamera;

        //speed variable
        [SerializeField] private float Jumpspeed,speed; 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool jumpReady;
        [SerializeField] private int layerNumber; // add a your layer number for jumping
        [SerializeField] private PhysicMaterial floorMaterial; // for friction
        [SerializeField] private GameObject camera;
        [SerializeField] private Transform playerTransform;

        private void Awake()
        {
            //Friction for movement
            floorMaterial.dynamicFriction = 1f;
            floorMaterial.staticFriction = 1f;
        }

        private void Start()
        {
            speed = 100 * Time.fixedDeltaTime; // mainly movement speed value
            Jumpspeed = 20; // mainly jump speed value
            
            // recommended setting (don't close gravity)
            rb.useGravity = true;
            
            //Default scale 
            transform.localScale = defaultScale;
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
            rb.AddForce(0 , -100 , 0); // change by yourself
        }

        private void MyInputF()
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");

            jump = Input.GetKeyDown(KeyCode.Space);
            if(jump && jumpReady){rb.AddForce(jumpVector * Jumpspeed);} // if jump and jumpReady is true can jump

            crouch = Input.GetKey(KeyCode.LeftControl); // crouch
            crouchStart = Input.GetKeyDown(KeyCode.LeftControl); // Start Crouch
            crouchend = Input.GetKeyUp(KeyCode.LeftControl); // crouch end
        }

        private void MovementF()
        {
            rb.velocity += new Vector3(x, 0 , 0) * speed;
            rb.velocity += transform.forward * (z * speed);
            if(crouch && Input.GetKey(KeyCode.W)){rb.AddForce(0 , 0 , 5);}
        }

        // Crouch Function
        private void CrouchF()
        {
            
            if(crouch && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.W) && crouch) // if player press crouch and w start crouch
            {
                x = 0;
                speed = 0.1f;
                floorMaterial.dynamicFriction = 0.5f;
            }

            if (crouchStart && !Input.GetKey(KeyCode.W)) // reduce speed while squatting
            {
                transform.localScale = crouchScale;
            }

            if (crouchend)
            {
                x = Input.GetAxis("Horizontal");

                for (float i = 0; i < 2.0f; i += 0.1f) // i increase speed but after 1 second 
                {
                    speed += 0.1f;
                }
                
                floorMaterial.dynamicFriction = 1f;
                transform.localScale = defaultScale;
            }
        }

        private void OnCollisionEnter(Collision other) // can jump if it touches the ground
        {
            if (other.gameObject.layer == layerNumber) // Add a layer number
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
