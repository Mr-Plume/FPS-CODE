/* I improving this code */

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
        private Vector3 jumpVector = new Vector3(0 , 30 , 0); // jump vector
        private Vector3 crouchScale = new Vector3(1 , 0.5f , 1); // crouch scale
        private Vector3 defaultScale = new Vector3(1 , 1 , 1); // default scale

        //speed variable
        [SerializeField] private float Jumpspeed,speed; 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool jumpReady;
        [SerializeField] private int layerNumber; // add a your layer number for jumping
        [SerializeField] private PhysicMaterial floorMaterial; // for friction
        private void Awake()
        {
            // I freeze rotation in order not to fall.
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            //Friction for movement
            floorMaterial.dynamicFriction = 2.5f;
            floorMaterial.staticFriction = 2.5f;
        }

        private void Start()
        {
            speed = 50 * Time.deltaTime; // mainly movement speed value
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
            rb.AddForce(0 , -20 , 0); // change by yourself
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
            
            if(crouch && Input.GetKey(KeyCode.W)) // if player press crouch and w start crouch
            {
                speed = 0;
                floorMaterial.dynamicFriction = 0.5f;
                transform.eulerAngles = new Vector3(-10 , 0 , 0);
            }

            if (crouchStart) // reduce speed while squatting
            {
                transform.localScale = crouchScale;
            }

            if (crouchend)
            {
                transform.eulerAngles = new Vector3(0 , 0 , 0);
                speed = 1;
                floorMaterial.dynamicFriction = 2f;
                transform.localScale = defaultScale;
            }
        }

        private void MovementF()
        {
            rb.velocity += new Vector3(x, 0, z) * speed; // always use Time.deltaTime for smooth movement
            if(crouch && Input.GetKey(KeyCode.W)){rb.AddForce(0 , -20 , 20);}
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
    
// i fixed comments and i add a lot of more comments.
