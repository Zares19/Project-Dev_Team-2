using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioScript : MonoBehaviour
{
    AudioSource _audi;

    public AudioClip[] soundFX;
    public AudioClip[] bgMusic;

    // Start is called before the first frame update
    void Start()
    {
        _audi = GetComponent<AudioSource>();
    }
}
