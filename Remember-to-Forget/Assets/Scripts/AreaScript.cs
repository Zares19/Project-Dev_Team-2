using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaScript : MonoBehaviour
{
    public GameObject[] floors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other .gameObject.name == "Trig_Main")
        {
            floors[1].SetActive(true);
            floors[0].SetActive(false);
        }

        if (other.gameObject.name == "Trig_Basement")
        {
            floors[0].SetActive(true);
            floors[1].SetActive(false);
        }
    }
}
