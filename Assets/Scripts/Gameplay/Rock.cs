using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameEvent rockHitEvent;

    private void OnCollisionEnter(Collision collision)
    {
        rockHitEvent.Rise();
        //Debug.Log("Choque con: " + collision.gameObject.name);
        Destroy(this.gameObject);
    }
}
