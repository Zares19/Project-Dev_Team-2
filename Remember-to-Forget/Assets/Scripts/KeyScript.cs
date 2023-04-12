using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    
    public enum KeyType
    {
        Red,
        Green,
        Blue,
    }
    
    public KeyType GetKeyType()
    {
        return keyType;
    }
}
