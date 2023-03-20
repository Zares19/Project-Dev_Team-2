using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20;

    public Rigidbody smallShot;
    public bool canShoot;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            //bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            /*Rigidbody playerBullet;
            playerBullet = Instantiate(smallShot, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody;
            playerBullet.AddForce(bulletSpawnPoint.forward * bulletSpeed);
            canShoot = false;
            yield return new WaitForSeconds(0.2f);
            canShoot = true;*/
        }
    }

}
