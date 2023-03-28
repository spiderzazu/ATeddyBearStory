using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainMesh, bearCannon, fireBall, cannonRef;
    public float speed = 1, jumpTime, maxJump = 1;
    public float sphereDetection, fireBallspeed = 1;
    public LayerMask groundLayer;
    public GameObject wideAttackPoint, simpleAttackPoint;
    public GameEvent widePunchEvent, normalPunchEvent, playerDamageEvent, gameOverEvent, healEvent, newLeafEvent, bearFireEvent;
    public BearEnemyData enemyData, lizardData;

    private Material healMaterial;
    private Animator wideAnim, simpleAnim;

    private float moveInput, jumpTimeCounter, distToGround;
    private bool left = false, isGrounded = true, jumping, movementBlocker = false, inmune = false;
    private Rigidbody rb;
    private Animator playerAnimator;
    private AnimatorStateInfo playerpunchStateInfo, playerFallingInfo;
    private Vector3 jumpVector;
    private GameObject tmpFireBall;

    public SelectedSave selectedSave;
    private PlayerInfo playerInfo;

    //private void Awake()
    //{
    //    mainMesh.GetComponent<SkinnedMeshRenderer>().material = new Material(mainMesh.GetComponent<SkinnedMeshRenderer>().material);
    //    healMaterial = mainMesh.GetComponent<SkinnedMeshRenderer>().material;
    //    rb = GetComponent<Rigidbody>();
    //    playerAnimator = GetComponent<Animator>();
    //    distToGround = this.GetComponent<CapsuleCollider>().bounds.extents.y;
    //    wideAnim = wideAttackPoint.GetComponent<Animator>();
    //    simpleAnim = simpleAttackPoint.GetComponent<Animator>();
    //    movementBlocker = false;
    //    inmune = false;
    //    SetSaveData();
    //    healMaterial.SetFloat("_SpeedAura", 0);
    //}

    // Start is called before the first frame update
    void Start()
    {
        mainMesh.GetComponent<SkinnedMeshRenderer>().material = new Material(mainMesh.GetComponent<SkinnedMeshRenderer>().material);
        healMaterial = mainMesh.GetComponent<SkinnedMeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        distToGround = this.GetComponent<CapsuleCollider>().bounds.extents.y;
        wideAnim = wideAttackPoint.GetComponent<Animator>();
        simpleAnim = simpleAttackPoint.GetComponent<Animator>();
        movementBlocker = false;
        inmune = false;
        SetSaveData();
        if (!playerInfo.widePunch)
            bearCannon.SetActive(true);
        else
            bearCannon.SetActive(false);
        healMaterial.SetFloat("_SpeedAura", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Movimiento
        if(!movementBlocker)
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
        //rb.AddForce(Mathf.Abs(moveInput) * this.transform.forward * speed);
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveInput)*speed);


        //Salto sensible
        if(Input.GetButtonDown("Jump") == true && IsGrounded() && !movementBlocker)
        {
            isGrounded = false;
            //jumpVector = transform.up * maxJump;
            rb.AddForce(this.transform.up * maxJump * Time.deltaTime);
            jumping = true;
            jumpTimeCounter = jumpTime;
            playerAnimator.SetTrigger("Jump");
        }
        if (Input.GetButtonUp("Jump") == true)
        {
            jumping = false;
        }

        if (Input.GetButton("Jump") && jumping == true && !movementBlocker)
        {
            //Sensibility Jump
            if (jumpTimeCounter > 0)
            {
                //Debug.Log("Adding force to jump");
                rb.AddForce(this.transform.up * maxJump * Time.deltaTime);
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
        if (Input.GetMouseButtonDown(0) && !playerpunchStateInfo.IsName("WeakPunch") && !playerpunchStateInfo.IsName("WidePunch") && !movementBlocker && Time.timeScale != 0)
        {
            //Debug.Log("Normal punch");
            playerAnimator.SetTrigger("NormalPunch");
            normalPunchEvent.Rise();
            simpleAttackPoint.SetActive(true);
            simpleAnim.SetTrigger("NormalAttack");
        }

        if (Input.GetMouseButtonDown(1) && !playerpunchStateInfo.IsName("WeakPunch") && !playerpunchStateInfo.IsName("WidePunch")
                                        && !playerpunchStateInfo.IsName("Pointing") && !movementBlocker && Time.timeScale != 0)
        {
            if (playerInfo.widePunch)
            {
                playerAnimator.SetTrigger("WidePunch");
                widePunchEvent.Rise();
                wideAttackPoint.SetActive(true);
                wideAnim.SetTrigger("WideAttack");
            }
            else if(playerInfo.currentAbilityPoints >= 3)
            {
                playerInfo.currentAbilityPoints -= 3;
                playerAnimator.SetTrigger("BearFire");
                bearFireEvent.Rise();
                bearFire();
            }
            //Debug.Log("Wide punch");
            
        }
        //Utilizar energía para recuperar puntos de vida
        if (Input.GetKeyDown(KeyCode.E) && Time.timeScale != 0)
        {
            if(playerInfo.currentLifePoints < playerInfo.totalLifePoints)
            {
                if(playerInfo.currentAbilityPoints >= 2)
                {
                    healMaterial.SetFloat("_SpeedAura", 1.0f);
                    StartCoroutine(FinishHealAura());
                    playerInfo.currentAbilityPoints = playerInfo.currentAbilityPoints - 2;
                    playerInfo.currentLifePoints++;
                    healEvent.Rise();
                }
            }
            
        }

    }

    public void bearFire()
    {
        Debug.Log("Rotacion: " + cannonRef.transform.rotation.y);
        tmpFireBall = Instantiate(fireBall, cannonRef.transform.position, this.transform.rotation);
        tmpFireBall.GetComponent<Rigidbody>().AddForce(cannonRef.transform.forward * fireBallspeed, ForceMode.Impulse);
    }

    IEnumerator FinishHealAura()
    {
        yield return new WaitForSeconds(1);
        healMaterial.SetFloat("_SpeedAura", 0f);
    }

    private void FixedUpdate()
    {
        //rb.AddForce(Mathf.Abs(moveInput) * this.transform.forward * speed);
        rb.velocity = (Mathf.Abs(moveInput) * this.transform.forward * speed) + (rb.velocity.y * Vector3.up);
    }
    private void OnDrawGizmos() //Para revisar el suelo
    {
        Gizmos.DrawSphere(transform.position, sphereDetection);
    }

    private bool IsGrounded() 
    {
        return Physics.CheckSphere(transform.position, sphereDetection, groundLayer);
    }


    public void AddLeafPower()
    {
        BlockMovement();
        playerInfo.leafFragments++;
        if(playerInfo.leafFragments >= 3 && playerInfo.totalAbilityPoints < 5)
        {
            playerInfo.totalAbilityPoints++;
            playerInfo.currentAbilityPoints++;
            playerInfo.leafFragments -= 3;
            newLeafEvent.Rise();
        }
        
        playerAnimator.SetTrigger("Upgrading");
    }

    //public void AddLifePoint()
    //{
    //    BlockMovement();
    //    playerInfo.lifePointsCollected++;
    //    playerInfo.totalLifePoints++;
    //    playerInfo.currentLifePoints++;
    //    playerAnimator.SetTrigger("Upgrading");
    //}

    public void BlockMovement()
    {
        moveInput = 0;
        movementBlocker = true;
    }

    public void UnBlockMovement()
    {
        movementBlocker = false;
        playerAnimator.SetTrigger("FinishUpgrade");
        //Debug.Log("Terminó la mejora");
    }

    public void SetSaveData()
    {
        playerInfo = selectedSave.saveFiles[selectedSave.selection];
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        playerInfo.currentLifePoints = playerInfo.totalLifePoints;
        BlockMovement();
        gameOverEvent.Rise();
    }

    IEnumerator StopImmunity()
    {
        yield return new WaitForSeconds(0.5f);
        inmune = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Recibir golpe
        if (collision.gameObject.CompareTag("EnemyAttack") && !inmune)
        {
            playerAnimator.SetTrigger("Reaction");
            //Debug.Log("Golpe de " + enemyData.damageInflicted + " puntos");
            playerInfo.currentLifePoints -= enemyData.damageInflicted;
            playerDamageEvent.Rise();
            inmune = true;
            StartCoroutine(StopImmunity());
            if(playerInfo.currentLifePoints <= 0)
            {
                GameOver();
            }
            //damage = playerDamage.widePunchDamage;
            //Debug.Log("Daño = " + damage);
        }
        if (collision.gameObject.CompareTag("Rock") && !inmune)
        {
            playerAnimator.SetTrigger("Reaction");
            //Debug.Log("Golpe de " + lizardData.damageInflicted + " puntos");
            playerInfo.currentLifePoints -= lizardData.damageInflicted;
            playerDamageEvent.Rise();
            inmune = true;
            StartCoroutine(StopImmunity());
            if (playerInfo.currentLifePoints <= 0)
            {
                GameOver();
            }
            //damage = playerDamage.widePunchDamage;
            //Debug.Log("Daño = " + damage);
        }
    }

}
