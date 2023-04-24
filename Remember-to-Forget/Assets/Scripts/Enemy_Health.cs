using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] public int enemyHealth = 5;
    EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GetComponent<EnemyScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player_Projectile")
        {
            if (!enemyScript.isDead)
            {
                enemyHealth -= 2;

                if (enemyHealth <= 0)
                {
                    enemyScript.EnemyDeath();
                }
                else enemyScript.EnemyHurt();
            }

        }
    }
}
