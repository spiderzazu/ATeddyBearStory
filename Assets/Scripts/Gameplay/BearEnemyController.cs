using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearEnemyController : MonoBehaviour
{
    private int lifePoints, damage = 0;
    private float timeToDie = 0.0f;
    private Animator enemyAnimator, bearAttackPointAnimator;
    private PlayerInfo playerDamage;
    private bool blockActions = false, inmune = false;
    private Vector3 position;
    private AnimatorStateInfo enemyStateInfo;
    private Material destroyMaterial;

    public GameObject mainMesh;
    public NavMeshAgent enemyAgent;
    public SelectedSave saveInfo;
    public GameEvent bearAttackEvent, bearDyingEvent, playerGotEnergyEvent;
    public GameObject bearAttackPoint;
    public BearEnemyData enemyData;

    // Start is called before the first frame update
    void Start()
    {
        timeToDie = 0.0f;
        mainMesh.GetComponent<SkinnedMeshRenderer>().material = new Material(mainMesh.GetComponent<SkinnedMeshRenderer>().material);
        destroyMaterial = mainMesh.GetComponent<SkinnedMeshRenderer>().material;
        enemyAnimator = GetComponent<Animator>();
        playerDamage = saveInfo.saveFiles[saveInfo.selection];
        bearAttackPointAnimator = bearAttackPoint.GetComponent<Animator>();
        lifePoints = enemyData.initialLifePoints;
        Patrol();
    }

    // Bear testing
    //void Update()
    //{
    //    //enemyAnimator.SetFloat("Speed", enemyAgent.velocity.sqrMagnitude);
    //    ////AttackTest
    //    //if (Input.GetKeyDown(KeyCode.T))
    //    //{
    //    //    //Attack();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.Y))
    //    //{
    //    //    Patrol();
    //    //}
    //}

    public void Chase(GameObject player)
    {
        CancelInvoke("Patrol");
        enemyStateInfo = enemyAnimator.GetCurrentAnimatorStateInfo(0);
        if (!enemyStateInfo.IsName("SlowPunch1") && gameObject.activeSelf)
            enemyAgent.SetDestination(player.transform.position);
    }

    public void Patrol()
    {
        InvokeRepeating("PatrolDestiny", 0f, 20f);
    }

    public void PatrolDestiny()
    {
        position = transform.position + new Vector3(Random.Range(-5, 5), 0f, 0f);
        if(gameObject.activeSelf)
            enemyAgent.SetDestination(position);
        //if (tempVec.x < position.x)
        //    gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        //else
        //    gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    public void Attack(GameObject player)
    {
        //Añadir que voltee hacia el player al inicio
        transform.LookAt(player.transform);
        if (!blockActions)
        {
            enemyAnimator.SetTrigger("Punch1");
            bearAttackEvent.Rise();
            bearAttackPoint.SetActive(true);
            bearAttackPointAnimator.SetTrigger("SlowAttack");
        }
    }

    IEnumerator FakeDead()
    {
        StartCoroutine(MaterialDestroy());
        yield return new WaitForSeconds(2.0f);
        blockActions = false;
        inmune = false;
        lifePoints = enemyData.initialLifePoints;
        destroyMaterial.SetFloat("_DissolveRate", 0.0f);
        gameObject.SetActive(false);
    }

    IEnumerator MaterialDestroy()
    {
        timeToDie += 0.05f;
        destroyMaterial.SetFloat("_DissolveRate", timeToDie);
        if (timeToDie < 1)
        {
            yield return new WaitForSeconds(0.2f);
            yield return MaterialDestroy();
        }
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator TerminaInmunidad()
    {
        yield return new WaitForSeconds(0.5f);
        inmune = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WidePunch") && !blockActions && !inmune)
        {
            damage = playerDamage.widePunchDamage;
            if(playerDamage.totalAbilityPoints > playerDamage.currentAbilityPoints)
            {
                playerDamage.currentAbilityPoints++;
                playerGotEnergyEvent.Rise();
            }
            lifePoints = lifePoints - damage;
            enemyAnimator.SetTrigger("Reaction");
            inmune = true;
            StartCoroutine(TerminaInmunidad());
        }
        else if (collision.gameObject.CompareTag("SimplePunch") && !blockActions && !inmune)
        {
            damage = playerDamage.normalPunchDamage;
            if (playerDamage.totalAbilityPoints > playerDamage.currentAbilityPoints)
            {
                playerDamage.currentAbilityPoints++;
                playerGotEnergyEvent.Rise();
            }
                
            lifePoints = lifePoints - damage;
            enemyAnimator.SetTrigger("Reaction");
            inmune = true;
            StartCoroutine(TerminaInmunidad());
        }
        else if (collision.gameObject.CompareTag("FireBall") && !blockActions && !inmune)
        {
            damage = playerDamage.fireBallDamage;
            lifePoints = lifePoints - damage;
            enemyAnimator.SetTrigger("Reaction");
            inmune = true;
            StartCoroutine(TerminaInmunidad());
        }
        if (lifePoints <= 0)
        {
            blockActions = true;
            bearDyingEvent.Rise();
            StartCoroutine(FakeDead());
        }
    }

    public void PlayerSaved()
    {
        gameObject.SetActive(true);
    }
}
