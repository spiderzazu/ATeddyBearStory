using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearEnemyController : MonoBehaviour
{
    private int lifePoints, damage = 0;
    private Animator enemyAnimator, bearAttackPointAnimator;
    private PlayerInfo playerDamage;
    private bool blockActions = false;
    private Vector3 position;
    private AnimatorStateInfo enemyStateInfo;

    public NavMeshAgent enemyAgent;
    public SelectedSave saveInfo;
    public GameEvent bearAttackEvent, bearDyingEvent;
    public GameObject bearAttackPoint;
    public BearEnemyData enemyData;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        playerDamage = saveInfo.saveFiles[saveInfo.selection];
        bearAttackPointAnimator = bearAttackPoint.GetComponent<Animator>();
        lifePoints = enemyData.initialLifePoints;
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        enemyAnimator.SetFloat("Speed", enemyAgent.velocity.sqrMagnitude);
        //AttackTest
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Attack();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Patrol();
        }
    }

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
        Vector3 tempVec = transform.position;
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

    public void Dying()
    {

        blockActions = true;
        //Activar el shader de derretir
        //Con Invoke llamar a disable
        Invoke("DisableBear", 2f);
        Debug.Log("Kumaaaa");
        CancelInvoke("Patrol");
        bearDyingEvent.Rise();
    }

    public void DisableBear()
    {
        blockActions = false;
        lifePoints = enemyData.initialLifePoints;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("WidePunch") && !blockActions)
        {
            damage = playerDamage.widePunchDamage;
            lifePoints = lifePoints - damage;
            enemyAnimator.SetTrigger("Reaction");
        }
        else if (collision.gameObject.CompareTag("SimplePunch") && !blockActions)
        {
            damage = playerDamage.normalPunchDamage;
            lifePoints = lifePoints - damage;
            enemyAnimator.SetTrigger("Reaction");
        }
        if(lifePoints <= 0)
        {
            Dying();
        }
    }
}
