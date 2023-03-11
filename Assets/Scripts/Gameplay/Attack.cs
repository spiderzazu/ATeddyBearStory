using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject attackCollider;
    public string tagObjective;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagObjective))
        {
            Debug.Log("El tag es: " + other.transform.tag);
            other.gameObject.SendMessage("Damage", damage);
        }
            
    }
}
