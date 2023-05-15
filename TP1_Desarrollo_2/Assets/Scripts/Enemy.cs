using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Life Enemy")]
    [SerializeField] private int life = 100;
    [SerializeField] private int enemyCount = 0;

    [Header("Materials Settings")]
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material impactMaterial;
    [SerializeField] private float timeSinceLastHit = 0f;
    [SerializeField] private float timeToRestoreMaterial = 1f;

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private float moveDelay = 2f;
    [SerializeField] private float moveDistance = 5f;

    [Header("Sound")]
    [SerializeField] private AudioSource hitEnemySound;

    [Header("Particle")]
    [SerializeField]
    private ParticleSystem particle;

    private Vector3 targetPosition;
    private float timeSinceLastMove = 0f;
    private static int enemiesKilled = 0;

    private void Start()
    {
        if (originalMaterial)
        {
            originalMaterial = GetComponent<MeshRenderer>().material;
        }

        hitEnemySound.enabled = true;

        Vector3 randomPosition = new Vector3(Random.Range(-moveDistance, moveDistance), 0f, Random.Range(-moveDistance, moveDistance));
        targetPosition = transform.position + randomPosition;
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

        // Si ha pasado suficiente tiempo desde el último movimiento, seleccionar una nueva posición aleatoria y comenzar a moverse hacia ella
        timeSinceLastMove += Time.deltaTime;
        if (timeSinceLastMove >= moveDelay)
        {
            timeSinceLastMove = 0f;
            Vector3 randomPosition = new Vector3(Random.Range(-moveDistance, moveDistance), 0f, Random.Range(-moveDistance, moveDistance));
            targetPosition = transform.position + randomPosition;

            // Rotar el objeto enemigo para que mire hacia la posición objetivo
            Vector3 targetDirection = targetPosition - transform.position;
            float step = movementSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        // Mover al enemigo hacia la posición objetivo usando MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }
}