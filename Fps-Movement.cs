/* I update this code */


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
        private Vector3 jumpVector = new Vector3(0 , 10 , 0); // jump vector
        private Vector3 crouchScale = new Vector3(1 , 0.5f , 1); // crouch scale
        private Vector3 defaultScale = new Vector3(1 , 1 , 1); // default scale

        //speed variable
        [SerializeField] private float Jumpspeed,speed,slideSpeed; 
        [SerializeField] private Rigidbody rb;
        [SerializeField] private bool jumpReady;
        [SerializeField] private int layerNumber; // add a your layer number for jumping
        [SerializeField] private PhysicMaterial floorMaterial; // for friction
        private void Awake()
        {
            // I freeze rotation in order not to fall.
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            //Friction for sliding
            floorMaterial.dynamicFriction = 1.4f;
            floorMaterial.staticFriction = 1.4f;
        }

        private void Start()
        {
            speed = 100 * Time.deltaTime; // mainly movement speed value
            Jumpspeed = 20; // mainly jump speed value
            slideSpeed = 0; // mainly start sliding speed value
            
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
            rb.AddForce(0 , -10 , 0); // change by yourself
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
                StartCoroutine(ChangeSlideSpeedC()); // Just 1 second use slide speed.
            }

            if (crouchStart) // reduce speed while squatting
            {
                speed -= 0.2f;
            }

            if (crouchend){speed += 0.2f;}
        }

        private void MovementF()
        {
            rb.velocity += new Vector3(x, 0, z) * speed; // always use Time.deltaTime for smooth movement
            rb.AddForce(transform.forward * slideSpeed , ForceMode.VelocityChange);
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
        
        private IEnumerator ChangeSlideSpeedC()
        {
            slideSpeed = 0.7f;
            yield return new WaitForSeconds(0.2f);
            slideSpeed = 0;
        }
    }
    }
    
// speed optimize is coming soon.
