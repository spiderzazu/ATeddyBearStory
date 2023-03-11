using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearEnemyController : MonoBehaviour
{

    private Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Attack()
    {
        enemyAnimator.SetTrigger("Punch1");
    }

    public void Damage(int damage)
    {
        Debug.Log("Enemigo herido con " + damage + " puntos de daño");
    }
}
