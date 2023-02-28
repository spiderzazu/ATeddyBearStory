using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1, jumpTime, maxJump = 1;
    private float moveInput, jumpTimeCounter, distToGround;
    private bool left = false, isGrounded = true, jumping;
    private Rigidbody rb;
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        distToGround = this.GetComponent<CapsuleCollider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movimiento
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput < 0 && !left)  //Orientacion
        {
            this.transform.rotation = Quaternion.Euler(0, 270, 0);
            left = true;
        }else if(moveInput > 0 && left)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            left = false;
        }
        
        //Debug.Log("Velocidad: " + moveInput);
        //Avance hacia adelante y animación
        rb.AddForce(Mathf.Abs(moveInput) * this.transform.forward * speed);
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveInput)*speed);



        if(Input.GetButtonDown("Jump") == true && IsGrounded())
        {
            rb.AddForce(this.transform.up * maxJump);
            jumping = true;
            jumpTimeCounter = jumpTime;
        }
        if (Input.GetButtonUp("Jump") == true)
        {
            jumping = false;
        }

        if (Input.GetButton("Jump") && jumping == true)
        {
            //Sensibility Jump
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(this.transform.up * maxJump);
                jumpTimeCounter = jumpTimeCounter - Time.deltaTime;
            }
            else
            {
                jumping = false;
            }

        }

    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
