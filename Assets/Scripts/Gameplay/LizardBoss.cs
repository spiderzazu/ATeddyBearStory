using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LizardBoss : MonoBehaviour
{
    public GameObject reference, rock;
    public GameEvent bossDefeatedEvent, playerGotEnergyEvent, bossDyingEvent, lizardJumpEvent, lizardKickEvent;
    public BearEnemyData lizardData;
    public SelectedSave saveInfo;
    
    public Material lizardBody, lizardArmor;
    public float rockForce;
    public int attack;

    public GameObject[] enemyZones; //6 zonas para Lizard

    public int playerZoneInd;
    public int lizardZoneInd;

    private float timeToAttack;
    private GameObject tmpRock;
    private NavMeshAgent lizardAgent;
    private PlayerInfo playerDamage;
    private int lifePoints;
    private bool blockActions = false, inmunidad = false;
    private static float timeToDie = 0.0f;
    private Animator lizardAnimator;

    // Start is called before the first frame update
    void Start()
    {
        timeToAttack = 5.5f;
        lizardAgent = GetComponent<NavMeshAgent>();
        playerDamage = saveInfo.saveFiles[saveInfo.selection];
        lizardAnimator = GetComponent<Animator>();
        timeToDie = 0.0f;
        lifePoints = lizardData.initialLifePoints;
        lizardData.damageInflicted = 2;
    }

    // Lizard Testing
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    JumpAttack(enemyZones[attack]);
        //}
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    KickAttack(enemyZones[0]);
        //}
    }

    public void StartFight()
    {
        lizardAnimator.SetTrigger("StartFight");
        StartCoroutine(FightSequence());
    }

    IEnumerator FightSequence()
    {
        if (lifePoints < 10)
            timeToAttack = 3.5f;
        yield return new WaitForSeconds(timeToAttack);
        NextMovement();
        if (gameObject.activeSelf)
            yield return FightSequence();
    }

    private void NextMovement()
    {
        int tmpZoneCheck = lizardZoneInd - playerZoneInd;
        if(Mathf.Abs(tmpZoneCheck) > 2)
            KickAttack(enemyZones[playerZoneInd]);
        else if(Mathf.Abs(tmpZoneCheck) <= 2 && lizardZoneInd != playerZoneInd)
            JumpAttack(enemyZones[playerZoneInd]);
        else
        {
            JumpAttack(enemyZones[Random.Range(0, 5)]);
        }

    }

    public void KickAttack(GameObject zoneToAttack)
    {
        lizardKickEvent.Rise();
        lizardAgent.isStopped = true;
        transform.LookAt(zoneToAttack.transform.position);
        lizardAnimator.SetTrigger("Kick");
    }

    public void JumpAttack(GameObject zoneToAttack)
    {
        lizardJumpEvent.Rise();
        inmunidad = true;
        lizardAgent.isStopped = false;
        transform.LookAt(zoneToAttack.transform);
        lizardAnimator.SetTrigger("JumpAttack");
        if (gameObject.activeSelf)
            lizardAgent.SetDestination(zoneToAttack.transform.position);
    }

    IEnumerator FakeDead()
    {
        StopCoroutine(FightSequence());
        lizardAnimator.SetTrigger("Afk");
        StartCoroutine(MaterialDestroy());
        yield return new WaitForSeconds(3.0f);
        blockActions = false;
        inmunidad = false;
        timeToAttack = 5.0f;
        lifePoints = lizardData.initialLifePoints;
        lizardBody.SetFloat("_DissolveRate", 0.0f);
        lizardArmor.SetFloat("_DissolveRate", 0.0f);
        gameObject.SetActive(false);
    }

    IEnumerator MaterialDestroy()
    {
        timeToDie += 0.05f;
        lizardBody.SetFloat("_DissolveRate", timeToDie);
        lizardArmor.SetFloat("_DissolveRate", timeToDie);
        if (timeToDie < 1)
        {
            yield return new WaitForSeconds(0.2f);
            yield return MaterialDestroy();
        }
        else
        {
            Debug.Log("Entra aquí?");
            yield return new WaitForSeconds(0.5f);
            bossDefeatedEvent.Rise();
        }
    }

    IEnumerator TerminaInmunidad()
    {
        yield return new WaitForSeconds(0.5f);
        inmunidad = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        int damage = 0;
        if (collision.gameObject.CompareTag("WidePunch") && !blockActions && !inmunidad)
        {
            damage = playerDamage.widePunchDamage;
            if (playerDamage.totalAbilityPoints > playerDamage.currentAbilityPoints)
            {
                playerDamage.currentAbilityPoints++;
                playerGotEnergyEvent.Rise();
            }
            lifePoints = lifePoints - damage;
            inmunidad = true;
            StartCoroutine(TerminaInmunidad());
        }
        else if (collision.gameObject.CompareTag("SimplePunch") && !blockActions && !inmunidad)
        {
            damage = playerDamage.normalPunchDamage;
            if (playerDamage.totalAbilityPoints > playerDamage.currentAbilityPoints)
            {
                playerDamage.currentAbilityPoints++;
                playerGotEnergyEvent.Rise();
            }

            lifePoints = lifePoints - damage;
            inmunidad = true;
            StartCoroutine(TerminaInmunidad());
        }
        else if (collision.gameObject.CompareTag("FireBall") && !blockActions && !inmunidad)
        {
            damage = playerDamage.fireBallDamage;
            lifePoints = lifePoints - damage;
            inmunidad = true;
            StartCoroutine(TerminaInmunidad());
        }
        if (lifePoints <= 0)
        {
            blockActions = true;
            bossDyingEvent.Rise();
            StartCoroutine(FakeDead());
        }
    }

    public void ShootRocks()
    {
        inmunidad = false;
        //Debug.Log("Lanzando rocas a los lados fuaaa");
        tmpRock = Instantiate(rock, reference.transform.position, Quaternion.identity);
        tmpRock.GetComponent<Rigidbody>().AddForce(Vector3.left * rockForce, ForceMode.Impulse);
        tmpRock = Instantiate(rock, reference.transform.position, Quaternion.identity);
        tmpRock.GetComponent<Rigidbody>().AddForce(Vector3.right * rockForce, ForceMode.Impulse);
    }

    public void ThrowRock()
    {
        //Debug.Log("A donde papu");
        tmpRock = Instantiate(rock, reference.transform.position, Quaternion.identity);
        tmpRock.GetComponent<Rigidbody>().AddForce(reference.transform.forward * rockForce, ForceMode.Impulse);
    }

    public void PlayerChecker(int area)
    {
        playerZoneInd = area;
    }

    public void LizardChecker(int area)
    {
        lizardZoneInd = area;
    }

    public void PlayerLeft()
    {
        StopAllCoroutines();
        lizardAnimator.SetTrigger("Afk");
    }

    public void PlayerSaved()
    {
        lifePoints = lizardData.initialLifePoints;
    }

}
