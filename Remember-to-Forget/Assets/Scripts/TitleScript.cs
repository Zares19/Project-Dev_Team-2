using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class TitleScript : MonoBehaviour
{
    AudioScript audioScript;
    AudioSource _audi;

    private void Awake()
    {
        audioScript = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioScript>();
        _audi = GetComponent<AudioSource>();
    }

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
        _audi.PlayOneShot(audioScript.soundFX[0]);
    }

    public void GoToNewScene()
    {
        SceneManager.LoadScene("Game");
        _audi.PlayOneShot(audioScript.soundFX[0]);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
        _audi.PlayOneShot(audioScript.soundFX[0]);
    }
}
