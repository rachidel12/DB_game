using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daggerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is an enemy
        if (collision.gameObject.CompareTag("ennemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            ScoreManager.instance.AddScore(10);
            ScoreManager.instance.UpdateScoreText();
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
