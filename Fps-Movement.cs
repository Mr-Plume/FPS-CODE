using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour
{
    public bool Okey = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Properties.Speed);
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Properties.Speed);
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Properties.Speed);
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Properties.Speed);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            Properties.Speed = 0.3f;
        }


        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
             Properties.Speed = 0.1f;
        }

        if(Input.GetKey(KeyCode.Space) && Okey == true)
        {
            StartCoroutine(Jump());
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1 , 0.5f , 1);
        }

        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1 , 1 , 1);
        }

        IEnumerator Jump()
        {
            transform.Translate(Vector3.up * Properties.JumpSpeed);
            Okey = false;
            yield return new WaitForSeconds(1);
            Okey = true;
        }
    }
}
