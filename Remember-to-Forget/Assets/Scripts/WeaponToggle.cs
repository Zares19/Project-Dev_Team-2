using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponToggle : MonoBehaviour
{
    [SerializeField] public int _weapon;
    [SerializeField] public float shootTime = .75f;
    [SerializeField] public float meleeTime = .1f;
    [SerializeField] bool canShoot;
    [SerializeField] bool canMelee;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;

    // weapon objects
    public GameObject _pipeWeapon;
    public GameObject _gun;

    public Transform bulletSpawnPoint;
    public Rigidbody smallShot;
    public float bulletSpeed = 2000f;
    public int numberOfBullets = 10;

    Animator _anim;
    // Start is called before the first frame update

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        canMelee = true;
        canShoot = true;
        _weapon = 1;
        _pipeWeapon.SetActive(true);
        _gun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _pipeWeapon.SetActive(true);
            _gun.SetActive(false);
            _weapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _weapon = 2;
            _pipeWeapon.SetActive(false);
            _gun.SetActive(true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_weapon == 1 && canMelee)
            {
                Debug.Log("You are Meleeing");
                _anim.SetBool("hit1", true);
                StartCoroutine(MeleeAttack());
            }
            if (_weapon == 2 && canShoot && numberOfBullets > 0)
            {
                Debug.Log("You are Shooting");
                _anim.SetTrigger("Shoot");
                StartCoroutine(ShootAttack());
            }
        }
    }

    IEnumerator MeleeAttack()
    {
        canMelee = false;
        yield return new WaitForSeconds(meleeTime);
        canMelee = true;
    }

    IEnumerator ShootAttack()
    {
        Rigidbody playerBullet;
        playerBullet = Instantiate(smallShot, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody;
        playerBullet.AddForce(bulletSpawnPoint.forward * bulletSpeed);
        canShoot = false;
        numberOfBullets--;
        yield return new WaitForSeconds(shootTime);
        canShoot = true;
    }
}
