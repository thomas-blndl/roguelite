using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private int _maxHp = 20;
    private int _hp = 20;
    private bool canBeHit = true;
    private float _hitCooldown = 1.0f;
    [SerializeField]
    private Slider _healthBar;
    private ThirdPersonController thirdPersonController;
    private Animator _anim;

    // Start is called before the first frame update
    void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        UpdateHealthBar();   
    }

    private void CheckHP()
    {
        if (_hp <= 0)
        {
            canBeHit = false;
            Die();
        }
    }

    private void Die()
    {
        thirdPersonController.enabled = false;
        GetComponent<CharacterController>().enabled = false;
        _anim.SetTrigger("Death");
    }

    public void Hit(int damages)
    {
        if (canBeHit)
        {
            _hp -= damages;
            canBeHit = false;
            StartCoroutine(ResetCanBeHit());
        }
    }

    public IEnumerator ResetCanBeHit()
    {
        yield return new WaitForSeconds(_hitCooldown);
        canBeHit = true;
    }

    private void UpdateHealthBar()
    {
        if (_hp > 0)
        {
            _healthBar.value = Mathf.InverseLerp(0, _maxHp, _hp);
        }
        else
        {
            _healthBar.value = 0f;
        }
    }
}
