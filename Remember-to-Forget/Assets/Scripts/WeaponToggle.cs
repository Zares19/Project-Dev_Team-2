using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

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
    public GameObject meleeHitBox;

    public Transform bulletSpawnPoint;
    public Rigidbody smallShot;
    public float bulletSpeed = 2000f;
    public int numberOfBullets = 10;

    Animator _anim;
    AudioScript audioScript;
    AudioSource _audi;
    // Start is called before the first frame update

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        audioScript = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioScript>();
        _audi = GetComponent<AudioSource>();
        meleeHitBox.SetActive(false);
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
        meleeHitBox.SetActive(true);
        _anim.SetTrigger("hit1");
        yield return new WaitForSeconds(meleeTime);
        canMelee = true;
        meleeHitBox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        meleeHitBox.SetActive(false);
    }

    public void Shoot()
    {
        _audi.PlayOneShot(audioScript.soundFX[2]);
    }

    public void Melee()
    {
        _audi.PlayOneShot(audioScript.soundFX[1]);
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
