using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    WeaponToggle toggle;


    // Start is called before the first frame update
    void Start()
    {
        toggle = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            toggle.numberOfBullets = 10;
            Destroy(gameObject);
        }
    }
}
