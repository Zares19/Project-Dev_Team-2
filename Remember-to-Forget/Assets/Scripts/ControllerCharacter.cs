using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]


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
    public bool isDashing;
    public bool isDead;

    Player_Health playerHealth;
    GameManager gameManager;
    CharacterController charCTRL;
    Animator anim;
    Vector2 moveInput;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    [SerializeField] Vector3 moveDirection;
    //[SerializeField] float smoothInputSpeed = 0.2f;
    [SerializeField] float turnSmoothVelocity;
    AudioScript audioScript;
    AudioSource _audi;

    private void Awake()
    {
        isDead = false;
        charCTRL = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        audioScript = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioScript>();
        _audi = GetComponent<AudioSource>();
        playerHealth = GetComponent<Player_Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.isDead)
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
        }
        playerHurtTime -= Time.deltaTime;
        if (playerHurtTime < 0) ; playerHurtTime = 0;

      

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressing Dodge button");
            StartCoroutine(PlayerDash());
        }
    }

    public void PDeath()
    {
        _audi.PlayOneShot(audioScript.soundFX[3]);
    }

    public void Roll()
    {
        _audi.PlayOneShot(audioScript.soundFX[5]);
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

    public void PlayerHurt()
    {
        playerHurtTime = 0.75f;
        anim.SetTrigger("Hurt");
    }

    public void PHurt()
    {
        _audi.PlayOneShot(audioScript.soundFX[4]);
    }

}

