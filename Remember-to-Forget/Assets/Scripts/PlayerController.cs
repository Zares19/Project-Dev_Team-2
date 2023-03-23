using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7;
    public float turnSpeed = 0.2f;
    public float turnSmoothing = 0.15f;
    float turnSmoothVelocity;
    public float pGravity = -20f;

    private Animator anim;

    CharacterController charCTRL;
    Vector3 moveVelocity;
    Vector3 moveDirection;
    Vector2 moveInput;

    public bool canShoot;
    public Rigidbody smallShot;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20;
    public int numberOfBullets = 10;

    public bool isDead;

    GameManager gameManager;

    private void Awake()
    {
        charCTRL = GetComponent<CharacterController>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        isDead = false;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveVelocity = new Vector3(moveInput.x * moveSpeed, 0, moveInput.y * moveSpeed);
            moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            moveVelocity.y += pGravity;

            anim.SetFloat("MoveSpeed", moveDirection.magnitude, 0.075f, Time.deltaTime);

            if (moveDirection != Vector3.zero)
            {
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothing);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);


            }

            charCTRL.Move(moveVelocity * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canShoot & numberOfBullets > 0) StartCoroutine(PlayerShoot());
            }
        }
    }

    public void PlayerDeath()
    {
        isDead = true;
        //anim.SetTrigger("Death");
        StartCoroutine(SwitchScene());
    }

    IEnumerator PlayerShoot()
    {
        Rigidbody playerBullet;
        playerBullet = Instantiate(smallShot, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody;
        playerBullet.AddForce(bulletSpawnPoint.forward * bulletSpeed);
        canShoot = false;
        numberOfBullets--;
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(.1f);
        gameManager.SwitchScene(20);
    }
}