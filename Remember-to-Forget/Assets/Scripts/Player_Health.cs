using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    [SerializeField] public int playerHealth = 10;
    ControllerCharacter controller;

    GameManager gameManager;
    Animator anim;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<ControllerCharacter>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy") || other.gameObject.tag == "Enemy_Projectile")
        {
            if (!isDead)
            {
                playerHealth --;

                if (playerHealth <= 0)
                {
                        isDead = true;
                        anim.SetTrigger("Death");
                        StartCoroutine(SwitchScene());
                }
                else controller.PlayerHurt();
            }

        }
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(1);
        gameManager.SwitchScene(3);
    }
}
