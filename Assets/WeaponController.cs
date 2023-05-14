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
    private int _weaponDamages = 5;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("collision");
        Debug.Log(thirdPersonController.isAttacking);
        if(other.transform.tag == "Enemy" && thirdPersonController.isAttacking)
        {
            //particles
            _particle = Instantiate(_hitParticles, 
                new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), Quaternion.identity);
            Destroy(_particle, 0.5f);

            //hit
            if(other.GetComponent<MonsterHandler>())
            {
                other.GetComponent<MonsterHandler>().Hit(_weaponDamages);
            }
        }
    }
}
