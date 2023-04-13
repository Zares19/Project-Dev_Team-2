using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyHolder : MonoBehaviour
{
    private List<KeyScript.KeyType> keyList;

    [SerializeField] public TMP_Text keyText;
    [SerializeField] public TMP_Text doorText;

    private void Awake()
    {
        keyList = new List<KeyScript.KeyType>();
    }

    private void Start()
    {
        keyText.gameObject.SetActive(false);
        doorText.gameObject.SetActive(false);
    }

    public void AddKey(KeyScript.KeyType keyType)
    {
        Debug.Log("Added Key: " + keyType);
        keyList.Add(keyType);
    }

    public void RemoveKey(KeyScript.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(KeyScript.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider other)
    {
        KeyScript key = other.GetComponent<KeyScript>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
            keyText.gameObject.SetActive(true);
        }

        KeyDoor keyDoor = other.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                //RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
                doorText.gameObject.SetActive(false);
            }

            if (!ContainsKey(keyDoor.GetKeyType()))
            {
                doorText.gameObject.SetActive(true);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        KeyScript key = other.GetComponent<KeyScript>();
        if (key != null)
        {
            keyText.gameObject.SetActive(false);
        }
        KeyDoor keyDoor = other.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (!ContainsKey(keyDoor.GetKeyType()))
            {
                doorText.gameObject.SetActive(false);
            }
        }
    }
}
