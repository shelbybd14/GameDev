using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private int score;
    private int lives = 3;

    [Header("Text Components:")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Other Components:")]
    [SerializeField] private Image lifeImage;

    [Header("Life Sprites:")]
    [SerializeField] private Sprite threeLives;
    [SerializeField] private Sprite twoLives;
    [SerializeField] private Sprite oneLife;
    [SerializeField] private Sprite zeroLives;

    private void Start() {
        scoreText.text = "Score: 0";
    }

    private void Update() {
        scoreText.text = "Score: " + score;
    }

    public void IncreaseScore(int amount) {
        score += amount;
    }

    public void RemoveLife() {
        lives--;
        switch (lives) {
            case 2:
                lifeImage.sprite = twoLives;
                break;
            case 1:
                lifeImage.sprite = oneLife;
                break;
            case 0:
                lifeImage.sprite = zeroLives;
                EndGame();
                break;
        }
    }

    private void EndGame() {
        Debug.Log("Game Ended");
    }
}
