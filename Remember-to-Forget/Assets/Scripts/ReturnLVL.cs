using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnLVL : MonoBehaviour
{
    void OnTriggerEnter(Collider ChangeScene)
    {
        SceneManager.LoadScene("Game");
    }
}
