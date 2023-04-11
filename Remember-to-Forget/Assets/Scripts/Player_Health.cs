using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField] public int playerHealth = 10;
    PlayerController playerCTRL;

    // Start is called before the first frame update
    void Start()
    {
        playerCTRL = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy") || other.gameObject.tag == "Enemy_Projectile")
        {
            if (!playerCTRL.isDead)
            {
                playerHealth--;

                if (playerHealth <= 0)
                {
                    playerCTRL.PlayerDeath();
                }
                else playerCTRL.PlayerHurt();
            }

        }
    }
}
