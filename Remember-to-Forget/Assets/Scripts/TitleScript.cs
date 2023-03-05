using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void GoToNewScene()
    {
        SceneManager.LoadScene("Player");
    }
}
