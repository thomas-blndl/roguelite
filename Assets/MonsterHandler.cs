using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandler : MonoBehaviour
{

    private int hp = 10;
    private float _hitCooldown = 1.0f;
    private bool canBeHit = true;

    private Animator _anim;

    private void Awake() {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
    }

    private void CheckHP()
    {
        if(hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _anim.SetTrigger("Death");
        Destroy(gameObject, 2.0f);
    }

    public void Hit(int damages)
    {
        if(canBeHit)
        {
            hp -= damages;
            canBeHit = false;
            StartCoroutine(ResetCanBeHit());
        }
    }

    public IEnumerator ResetCanBeHit()
    {
        yield return new WaitForSeconds(_hitCooldown);
        canBeHit = true;
    }
}
