using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image healthBar;
    public Text bulletCount;
    Player_Health playerHealth;

    WeaponToggle weaponToggle;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
        weaponToggle = GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponToggle>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth.playerHealth * .1f;
        bulletCount.text = weaponToggle.numberOfBullets.ToString();
    }

    public void SwitchScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }
}
