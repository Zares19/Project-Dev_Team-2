using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    WeaponToggle weaponToggle;
    ControllerCharacter controllerCharacter;
    EnemyScript enemyScript;

    void Awake()
    {
        weaponToggle = GetComponentInParent<WeaponToggle>();
        controllerCharacter = GetComponentInParent<ControllerCharacter>();
        enemyScript = GetComponentInParent<EnemyScript>();
    }
    
    public void ShootSound()
    {
        weaponToggle.Shoot();
    }

    public void MeleeHit()
    {
        weaponToggle.Melee();
    }

    public void Dodge()
    {
        controllerCharacter.Roll();
    }

    public void Hurt()
    {
        controllerCharacter.PHurt();
    }

    public void Death()
    {
        controllerCharacter.PDeath();
    }

    public void EnemyDamaged()
    {
        enemyScript.EHit();
    }

    public void EnemyMelee()
    {
        enemyScript.EMelee();
    }

    public void EnemySpit()
    {
        enemyScript.ESpit();
    }

    public void EnemyDefeat()
    {
        enemyScript.EDeath();
    }
}
