using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField] public int playerHealth = 10;
    ControllerCharacter controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ControllerCharacter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy") || other.gameObject.tag == "Enemy_Projectile")
        {
            if (!controller.isDead)
            {
                playerHealth -= 2;

                if (playerHealth <= 0)
                {
                    controller.PlayerDeath();
                }
                else controller.PlayerHurt();
            }

        }
    }
}
