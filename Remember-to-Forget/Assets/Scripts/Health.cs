using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float Hp;
    private float InvinsibleAmt;

    // Start is called before the first frame update
    void Start()
    {
        if (InvinsibleAmt > 0)
        {
            InvinsibleAmt -= Time.deltaTime;
        }
    }

    public void Invinsible(float delay, float invinsibleLength)
    {
        if (delay > 0)
        {
            StartCoroutine(StartInvinsible(delay, invinsibleLength));
        }
        else
        {
            InvinsibleAmt = invinsibleLength;
        }
    }

    IEnumerator StartInvinsible(float dly, float invsLength)
    {
        yield return new WaitForSeconds(dly);
        Debug.Log("Invinsible");
        InvinsibleAmt = invsLength;
    }

    public void TakeDamage(float Amt)
    {
        if (InvinsibleAmt <= 0)
        {
            Hp -= Amt;
            Debug.Log("Taken Damage");
        }
    }

}
