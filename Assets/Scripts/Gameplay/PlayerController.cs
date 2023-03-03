using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1, jumpTime, maxJump = 1;
    public float sphereDetection;
    public LayerMask groundLayer;
    private float moveInput, jumpTimeCounter, distToGround;
    private bool left = false, isGrounded = true, jumping;
    private Rigidbody rb;
    private Animator playerAnimator;
    private AnimatorStateInfo playerpunchStateInfo, playerFallingInfo;

    public PlayerInfo playerInfo;

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


        //Salto sensible
        if(Input.GetButtonDown("Jump") == true && IsGrounded())
        {
            isGrounded = false;
            rb.AddForce(this.transform.up * maxJump);
            jumping = true;
            jumpTimeCounter = jumpTime;
            playerAnimator.SetTrigger("Jump");
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
                Debug.Log("Adding force to jump");
                rb.AddForce(this.transform.up * maxJump);
                jumpTimeCounter = jumpTimeCounter - Time.deltaTime;
            }
            else
            {
                jumping = false;
            }

        }

        playerAnimator.SetFloat("FallingSpeed", rb.velocity.y);
        playerAnimator.SetBool("IsGrounded", IsGrounded());

        playerFallingInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if (!IsGrounded())
            isGrounded = false;

        if (IsGrounded() && !isGrounded)
        {
            isGrounded = true;
        }
        //Fin salto

        playerpunchStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(1);
        if (Input.GetKeyDown(KeyCode.Q) && !playerpunchStateInfo.IsName("WeakPunch") && !playerpunchStateInfo.IsName("WidePunch"))
        {
            Debug.Log("Normal punch");
            playerAnimator.SetTrigger("NormalPunch");
        }

        if (Input.GetKeyDown(KeyCode.E) && !playerpunchStateInfo.IsName("WeakPunch") && !playerpunchStateInfo.IsName("WidePunch"))
        {
            Debug.Log("Wide punch");
            playerAnimator.SetTrigger("WidePunch");
        }


    }

    private void OnDrawGizmos() //Para revisar el suelo
    {
        Gizmos.DrawSphere(transform.position, sphereDetection);
    }

    private bool IsGrounded() 
    {

        return Physics.CheckSphere(transform.position, sphereDetection, groundLayer);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "CommonBear")
    //    {
    //        Debug.Log("Oso golpeado");
    //    }
    //}
}
