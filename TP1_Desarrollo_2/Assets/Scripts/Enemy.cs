using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private int life = 100;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSelected = 1f;
    [SerializeField] private int timeMovement = 10;
    private float moveDelay = 0f;
    private float forceJump = 5f;

    [Header("Sound")]
    [SerializeField] private AudioSource hitEnemySound;

    [Header("Particle")]
    [SerializeField]
    private ParticleSystem particle;

    private int enemyCount = 0;
    private static int enemiesKilled = 0;
    private Animator anim;

    private TimerController EnemyCountController;

    private void Start()
    {
        hitEnemySound.enabled = true;
        anim = GetComponent<Animator>();
        EnemyCountController = GameObject.FindObjectOfType<TimerController>();
    }

    public void SetEnemyCount(int enemycount)
    {
        enemyCount = enemycount;
    }
    public void PlaySound()
    {
        hitEnemySound.Play();
    }

    public void TakeDamage(int damageAmount)
    {
        particle.Play();
        life -= damageAmount;
        if (life <= 0)
        {
            // El enemigo ha muerto, aumentar el contador de enemigos eliminados
            enemiesKilled++;
            EnemyCountController.EnemyKilled();

            // Destruir el objeto enemigo
            Destroy(gameObject);
        }

    }

    private void FixedUpdate()
    {
        moveDelay += Time.deltaTime * 1;
        if (movementSelected == 1)
        {
            moveDelay += Time.deltaTime * 1;
            Vector3 randomPosition = new Vector3(0f, 0f, Mathf.Sin(moveDelay)) * timeMovement;
            transform.position += randomPosition * Time.deltaTime;
            SetFloats(transform.position.x, 0);

            // Orientar el objeto hacia la dirección de movimiento
            if (randomPosition != Vector3.zero)
            {
                transform.LookAt(transform.position + randomPosition);
            }
        }
        else if (movementSelected == 2)
        {
            if (moveDelay < 3)
            {
                anim.SetBool("Jump", false);
            }
            if (moveDelay >= 3)
            {              
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                anim.SetBool("Jump", true);
                moveDelay = 0;
            }
            

        }
        else
        {
            transform.position += new Vector3(0, 0, 0);
            SetFloats(0, 0);
        }
        
        
    }

    private void SetFloats(float x, float y)
    {
        anim.SetFloat("velX", x);
        anim.SetFloat("velY", y);
    }
}