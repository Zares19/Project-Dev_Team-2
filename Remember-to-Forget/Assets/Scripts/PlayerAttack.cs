using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform spawnPoint;
    public Rigidbody swordProjectile;

    public float projectileSpeed = 1500;

    public void ThrowProjectile()
    {
        Rigidbody _sword;
        _sword = Instantiate(swordProjectile, spawnPoint.position, spawnPoint.rotation) as Rigidbody;
        _sword.AddForce(spawnPoint.forward * projectileSpeed);
    }
}
