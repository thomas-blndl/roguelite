using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{

    public ThirdPersonController thirdPersonController;

    [SerializeField]
    private GameObject _hitParticles;
    private GameObject _particle;
    private int _weaponDamages = 3;

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Enemy" && thirdPersonController.isAttacking)
        {
            MonsterHandler monsterHandler = other.GetComponent<MonsterHandler>();
            if (monsterHandler)
            {
                if (monsterHandler.canBeHit)
                {
                    //particles
                    _particle = Instantiate(_hitParticles,
                        new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), Quaternion.identity);

                    Destroy(_particle, 0.5f);

                    monsterHandler.Hit(_weaponDamages);
                }

            }
        }
    }
}
