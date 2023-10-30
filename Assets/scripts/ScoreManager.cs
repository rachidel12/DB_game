using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager instance;

    [SerializeField] public TextMeshProUGUI scoreText;
    public int score1=0;

    public void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value) {
        score1 += value;
        UpdateScoreText();
    }

    public string UpdateScoreText() {
        scoreText.text = score1.ToString();
        // Debug.Log(score.ToString());
        return score1.ToString();
    }
}
