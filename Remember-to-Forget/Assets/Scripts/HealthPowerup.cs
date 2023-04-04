using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : MonoBehaviour
{
    Player_Health pHealth;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pHealth.playerHealth = 3;
            Destroy(gameObject);
        }
    }
}
