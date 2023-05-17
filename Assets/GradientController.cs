using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradientController : MonoBehaviour
{
    private Gradient gradient;
    private GradientColorKey[] colorKey;
    private GradientAlphaKey[] alphaKey;
    private Image _image;
    private GameObject _player;
    private PlayerController playerController;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _image = GetComponent<Image>();
        playerController = _player.GetComponent<PlayerController>();
    }

    private void Start() {
        initGradient();
        _image.color = gradient.Evaluate(1);
    }
    private void Update()
    {
        Debug.Log("hp : " + (float)playerController.hp / (float)playerController.maxHp);
        Debug.Log(gradient.Evaluate((float)playerController.hp / (float)playerController.maxHp));
        _image.color = gradient.Evaluate((float)playerController.hp / (float)playerController.maxHp);
    }

    public void initGradient()
    {
        gradient = new Gradient();

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.green;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 1.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }
}
