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
    public bool canInteract;
    public bool isCarrying;
    public bool isDead;

    public bool canShoot;
    public Rigidbody smallShot;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 20;
    public int numberOfBullets = 10;

    GameManager gameManager;
    CharacterController charCTRL;
    //HunterAttack hunterAttack;
    Animator anim;
    public Transform checkRayHit;
    [SerializeField] GameObject pickedUpItem;
    public Transform itemSocket;
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
        //hunterAttack = GetComponent<HunterAttack>();
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

        CheckInteractive();

        if (Input.GetButtonDown("Fire2") && !isDashing)
        {
            StartCoroutine(PlayerDash());
        }

        if (!isDead && !isAttacking)
        {
            if (isCarrying) StartCoroutine(ObjectThrow());
            else
            {
                if (canInteract) StartCoroutine(ObjectHandle());
                else
                {
                    if (weaponSelect == 1)
                    {
                        anim.SetBool("hit1", true);
                        //hunterAttack.StartMachete();
                    }
                    if (weaponSelect == 2)
                    {
                        anim.SetTrigger("Shoot");
                        //hunterAttack.StartSpear();
                    }
                    /*if (weaponSelect == 3)
                    {
                        anim.SetTrigger("Hammer");
                        hunterAttack.StartHammer();
                    }
                    if (weaponSelect == 4)
                    {
                        anim.SetTrigger("BlowGun");
                        hunterAttack.StartBlowgun();
                    }*/

                   isAttacking = true;
                }
            }

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

    void CheckInteractive()
    {
        RaycastHit checkRay;
        Vector3 checkforward = transform.TransformDirection(Vector3.forward) * 0.3f;

        if (Physics.Raycast(checkRayHit.transform.position, transform.TransformDirection(Vector3.forward), out checkRay, 0.3f))
        {
            if (checkRay.collider.gameObject.tag == "PickUp")
            {
                canInteract = true;
                pickedUpItem = checkRay.collider.gameObject;
                Debug.Log("Interactable");
            }
        }
        else
        {
            canInteract = false;
            //pickedUpItem = null;
        }
        Debug.DrawRay(checkRayHit.transform.position, checkforward, Color.green);
    }

    IEnumerator ObjectHandle()
    {
        moveInput = Vector3.zero;

        if (!isCarrying)
        {
            Debug.Log("Picked Up Item");
            Rigidbody itemRB = pickedUpItem.GetComponent<Rigidbody>();
            anim.SetTrigger("PickUp");
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            itemRB.constraints = RigidbodyConstraints.FreezeAll;
            itemRB.mass = 0.001f;
            itemRB.useGravity = false;
            isAttacking = false;
            pickedUpItem.transform.SetParent(itemSocket);
            isCarrying = true;
        }
        else
        {
            Debug.Log("Dropped Item");
            Rigidbody itemRB = pickedUpItem.GetComponent<Rigidbody>();
            anim.SetTrigger("PutDown");
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            itemRB.useGravity = true;
            itemRB.mass = 1f;
            itemRB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
            pickedUpItem.transform.SetParent(null);
            isAttacking = false;
            isCarrying = false;
        }

    }

    IEnumerator ObjectThrow()
    {
        moveInput = Vector3.zero;
        isAttacking = true;
        Rigidbody itemRB = pickedUpItem.GetComponent<Rigidbody>();
        anim.SetTrigger("Throw");

        yield return new WaitForSeconds(0.15f);

        itemRB.useGravity = true;
        itemRB.mass = 1f;
        itemRB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
        pickedUpItem.transform.SetParent(null);
        itemRB.AddForce(checkRayHit.forward * 10f, ForceMode.Impulse);
        itemRB.AddForce(checkRayHit.up * 5f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
        isCarrying = false;
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
        gameManager.SwitchScene(18);
    }

}

