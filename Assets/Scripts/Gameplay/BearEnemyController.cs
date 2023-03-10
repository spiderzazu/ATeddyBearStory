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

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Golpeado por: " + other);
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Ahm?? " + collision.gameObject.name);
    //}
}
