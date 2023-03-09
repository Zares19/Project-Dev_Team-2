using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    PlayerController pCTRL;


    // Start is called before the first frame update
    void Start()
    {
        pCTRL = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pCTRL.numberOfBullets = 10;
            Destroy(gameObject);
        }
    }
}
