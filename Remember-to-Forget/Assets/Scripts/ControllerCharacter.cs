using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class ControllerCharacter : MonoBehaviour
{
    public float moveSpeed = 5;
    public float turnSpeed = 0.2f;
    public float rotateSpeed = 90;
    public float pGravity = -20f;
    public float playerHurtTime = 0;

    public float dashSpeed = 20;
    public float dashTime = 0.5f;
    [SerializeField] float turnSmoothing = 0.15f;
    [SerializeField] int weaponSelect = 1;
    public bool isRunning;
    public bool isDashing;
    public bool isAttacking;
    public bool isDead;

    public bool canShoot;
    public Rigidbody smallShot;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20;
    public int numberOfBullets = 10;

    GameManager gameManager;
    CharacterController charCTRL;
    Animator anim;
    Vector2 moveInput;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    [SerializeField] Vector3 moveDirection;
    //[SerializeField] float smoothInputSpeed = 0.2f;
    [SerializeField] float turnSmoothVelocity;

    private void Awake()
    {
        canShoot = true;
        isDead = false;
        charCTRL = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
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
                if (canShoot & numberOfBullets > 0)
                {
                    anim.SetTrigger("Shoot");
                    StartCoroutine(PlayerShoot());
                }
            }
        }
        playerHurtTime -= Time.deltaTime;
        if (playerHurtTime < 0) ; playerHurtTime = 0;

      

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressing Dodge button");
            StartCoroutine(PlayerDash());
        }

        if (!isDead && !isAttacking)
        {
            

        }
    }

    public void PlayerDeath()
    {
        isDead = true;
        anim.SetTrigger("Death");
        StartCoroutine(SwitchScene());
    }

    IEnumerator PlayerDash()
    {
        float startTime = Time.time;
        isDashing = true;
        anim.SetTrigger("Roll");
        while (Time.time < startTime + dashTime)
        {
            charCTRL.Move(moveDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
    }

    IEnumerator PlayerShoot()
    {
        Rigidbody playerBullet;
        playerBullet = Instantiate(smallShot, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as Rigidbody;
        playerBullet.AddForce(bulletSpawnPoint.forward * bulletSpeed);
        canShoot = false;
        numberOfBullets--;
        yield return new WaitForSeconds(.75f);
        canShoot = true;
    }

    IEnumerator SwitchScene()
    {
        yield return new WaitForSeconds(.5f);
        gameManager.SwitchScene(3);
    }

    public void PlayerHurt()
    {
        playerHurtTime = 0.75f;
        anim.SetTrigger("Hurt");
    }

}

