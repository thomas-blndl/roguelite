using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHp = 20;
    public int hp = 20;
    private bool canBeHit = true;
    private float _hitCooldown = 1.0f;
    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private TextMeshProUGUI _healthBarText;
    private ThirdPersonController thirdPersonController;
    private Animator _anim;

    [SerializeField]
    private Image _bloodOverlay;

    // Start is called before the first frame update
    void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _anim = GetComponent<Animator>();
        _bloodOverlay.canvasRenderer.SetAlpha(0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHP();
        UpdateHealthBar();   
    }

    private void CheckHP()
    {
        if (hp <= 0)
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
        _bloodOverlay.canvasRenderer.SetAlpha(1.0f);
    }

    public void Hit(int damages)
    {
        if (canBeHit)
        {
            hp -= damages;
            canBeHit = false;
            _bloodOverlay.canvasRenderer.SetAlpha(1.0f);
            _bloodOverlay.CrossFadeAlpha(0, 1.0f, true);
            _anim.SetTrigger("Impact");
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
        if (hp > 0)
        {
            _healthBar.value = Mathf.InverseLerp(0, maxHp, hp);
        }
        else
        {
            _healthBar.value = 0f;
        }
        _healthBarText.text = hp + "/"+maxHp;
    }
}
