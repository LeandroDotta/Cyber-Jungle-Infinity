using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10;

    [Header("Sound Effects")]
    [SerializeField] private SoundEffect soundShot;
    [SerializeField] private SoundEffect soundDamage;
    [SerializeField] private SoundEffect soundLoose;

    private GameManager gameManager;
    private Animator animator;
    private Collider2D coll;
    private PlayerController controller;
    private Gun gun;
    private SoundEffectPlayer soundEffectPlayer;


    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        gun = GetComponentInChildren<Gun>();
        soundEffectPlayer = new SoundEffectPlayer(GetComponent<AudioSource>());
    }

    private void OnDisable()
    {
        coll.enabled = false;
        gun.enabled = false;
        controller.enabled = false;
    }

    private void Update()
    {
        Move();
        ClampPositionToScreen();
        UpdateAnimations(controller.Direction);
    }

    private void OnHealthChange(int health)
    {
        if (health == 0)
        {
            Loose();
            return;
        }

        animator.SetTrigger("take_damage");
        soundEffectPlayer.Play(soundDamage);
    }

    private void OnInvulnerableStart()
    {
        animator.SetBool("blinking", true);
    }

    private void OnInvulnerableEnd()
    {
        animator.SetBool("blinking", false);
    }

    private void OnShoot()
    {
        soundEffectPlayer.Play(soundShot);
    }

    private void UpdateAnimations(Vector2 direction)
    {
        animator.SetFloat("direction_horizontal", direction.x);
        animator.SetFloat("direction_vertical", direction.y);
    }   

    private void Loose()
    {
        enabled = false;
        animator.SetTrigger("explode");
        soundEffectPlayer.Play(soundLoose);
        
        Invoke("ShowGameOver", 2f);
    }

    private void ShowGameOver()
    {
        gameManager.GameOver();
    }

    private void Move()
    {
        transform.Translate(speed * Time.deltaTime * controller.Direction);
    }

    private void ClampPositionToScreen()
    {
        Vector3 maxPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 minPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        Vector2 extents = coll.bounds.extents;

        minPosition.x += extents.x;
        minPosition.y += extents.y;

        maxPosition.x -= extents.x;
        maxPosition.y -= extents.y;

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minPosition.x, maxPosition.x);
        position.y = Mathf.Clamp(position.y, minPosition.y, maxPosition.y);
        transform.position = position;
    }
}
