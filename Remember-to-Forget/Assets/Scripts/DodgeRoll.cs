using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll : MonoBehaviour
{
    private Health Hp;
    private CharacterController charCTRL;

    private Animator anim;

    public float DelayBeforeInvinsible = 0.2f;
    public float InvinsibleDuration = 0.5f;

    public float DodgeCoolDown = 1;
    private float ActCooldown;

    public float PushAmt = 3;
    bool Roll;

    void Start()
    {
        Hp = GetComponent<Health>();
        charCTRL = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Roll = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !Roll) Dodge();
    }

    void Dodge()
    {
        ActCooldown = DodgeCoolDown;
        //Hp.Invinsible(DelayBeforeInvinsible, InvinsibleDuration);

        //charCTRL.AddForce(transform.forward * PushAmt, ForceMode.Force);

        anim.SetTrigger("Roll");
        StartCoroutine(CanRoll());
    }

    IEnumerator CanRoll()
    {
        Roll = true;    
        yield return new WaitForSeconds(1);
        Roll = false;
    }

}
