using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] private float speed = 500f;   // Player movement speed
    [SerializeField] public GameObject daggerPrefab;   // Prefab for the dagger object
    [SerializeField] private float daggerSpeed = 500f;   // Speed at which the dagger moves
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float minX, maxX, minY, maxY; // Range of where the enemies can spawn
    [SerializeField] private float spawnInterval = 2f;
    

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject dagger = Instantiate(daggerPrefab, transform.position, Quaternion.identity);
            Rigidbody2D daggerRb = dagger.GetComponent<Rigidbody2D>();
            daggerRb.velocity = new Vector2(daggerRb.velocity.x, 2 * daggerSpeed); 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ennemy"))   // If the collided object is an enemy
        {
            Debug.Log("you touched ennemy");
            ScoreManager.instance.UpdateScoreText();
            gamePanel.SetActive(false);
            gameoverPanel.SetActive(true);
            
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
