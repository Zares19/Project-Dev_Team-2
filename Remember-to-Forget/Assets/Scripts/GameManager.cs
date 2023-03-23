using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image healthBar;
    Player_Health playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth.playerHealth * 0.01f;
    }

    public void SwitchScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }
}
