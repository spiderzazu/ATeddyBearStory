using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafUpgrade : MonoBehaviour
{
    public PlayerInfo playerInfo;
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
        if (other.transform.CompareTag("Player"))
        {
            playerInfo.leafFragments++;
            //Aquí llamo al evento de recogida de fragmento de hoja 
            //Se revisa cuántos fragmentos tiene
        }
    }
}
