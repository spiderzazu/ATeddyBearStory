using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameEvent fireBallHitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        fireBallHitEvent.Rise();
        Destroy(this.gameObject);
    }
}
