using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillScript : MonoBehaviour
{

    public float killTime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, killTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Enviroment") Destroy(gameObject);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enviroment") Destroy(gameObject);
        if (other.gameObject.tag == "Enemy") Destroy(gameObject);
    }
}
