using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Life Enemy")]
    [SerializeField] private int life = 100;
    

    [Header("Materials Settings")]
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material impactMaterial;
    [SerializeField] private float timeSinceLastHit = 0f;
    [SerializeField] private float timeToRestoreMaterial = 1f;

    [Header("Movement Settings")]
    [SerializeField] private float movementSelected = 1f;
    [SerializeField] private float forceJump = 5f;
    private float moveDelay = 0f;

    [Header("Sound")]
    [SerializeField] private AudioSource hitEnemySound;

    [Header("Particle")]
    [SerializeField]
    private ParticleSystem particle;

    private int enemyCount = 0;
    private static int enemiesKilled = 0;

    private void Start()
    {
        if (originalMaterial)
        {
            originalMaterial = GetComponent<MeshRenderer>().material;
        }

        hitEnemySound.enabled = true;
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

            if (enemiesKilled >= enemyCount)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                enemiesKilled = 0;
            }

            // Destruir el objeto enemigo
            Destroy(gameObject);
        }
        if (impactMaterial)
        {
            GetComponent<MeshRenderer>().material = impactMaterial;
        }

    }

    private void Update()
    {
        if (originalMaterial || impactMaterial)
        {
            if (GetComponent<MeshRenderer>().material != originalMaterial)
            {
                timeSinceLastHit += Time.deltaTime;
                if (timeSinceLastHit >= timeToRestoreMaterial)
                {
                    GetComponent<MeshRenderer>().material = originalMaterial;
                    timeSinceLastHit = 0f;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        moveDelay += Time.deltaTime * 1;
        if (movementSelected == 1)
        {
            Vector3 randomPosition = new Vector3(Mathf.Sin(moveDelay), 0f, 0f) * 10f;
            transform.position += randomPosition * Time.deltaTime;
        }
        else if (movementSelected == 2)
        {
            if (moveDelay >= 3)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * forceJump, ForceMode.Impulse);
                moveDelay = 0;
            }
        }
        else
        {
            transform.position += new Vector3(0, 0, 0);
        }
    }
}