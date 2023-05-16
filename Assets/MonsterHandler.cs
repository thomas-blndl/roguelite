using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterHandler : MonoBehaviour
{
    private int _maxHp = 10;
    private int _hp = 10;
    private float _hitCooldown = 1.0f;
    [HideInInspector]
    public bool canBeHit = true;
    private Animator _anim;

    [SerializeField]
    private Slider _healthBar;
    private GameObject _player;
    private NavMeshAgent _agent;
    private Rigidbody _rb;

    public float attackRange = 2f;
    public float attackCooldown = 2f;
    private bool canAttack = true;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _rb = GetComponent<Rigidbody>();
        _healthBar.value = 1;
        _hp = _maxHp;
        GetComponent<SphereCollider>().radius = attackRange + 2.0f;
        _agent.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        UpdateHealthBar();
        WalkTowardPlayer();
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
        canAttack = false;
        _healthBar.transform.parent.gameObject.SetActive(false);
        _agent.enabled = false;
        _anim.SetTrigger("Death");
        Destroy(gameObject, 2.0f);

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

    private void WalkTowardPlayer()
    {
        if (_agent.isActiveAndEnabled)
        {
            _agent.SetDestination(_player.transform.position);
        }

        if (_agent.velocity.magnitude < 1)
        {
            _anim.SetBool("Idle", true);
        }
        else
        {
            _anim.SetBool("Idle", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            AttackAnimation();
        }
    }

    private void AttackAnimation()
    {
        _anim.ResetTrigger("Attack1");
        _anim.ResetTrigger("Attack2");
        if (canAttack)
        {
            canAttack = false;
            int random = Random.Range(1, 3);
            _anim.SetTrigger("Attack" + random);
            StartCoroutine(ResetCanAttack());
        }
    }

    public IEnumerator ResetCanAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void OnAttack()
    {
        _player.GetComponent<PlayerController>().Hit(5);
    }
}
