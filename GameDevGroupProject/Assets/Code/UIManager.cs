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
    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite keySprite;

    [SerializeField] private SpriteRenderer door;
    [SerializeField] private Sprite exitDoor;

    [Header("Life Sprites:")]
    [SerializeField] private Sprite threeLives;
    [SerializeField] private Sprite twoLives;
    [SerializeField] private Sprite oneLife;
    [SerializeField] private Sprite zeroLives;

    private void Start() {
        scoreText.text = "Score: 0";
        door = GameObject.FindGameObjectWithTag("ExitDoor").GetComponent<SpriteRenderer>();
        keyImage.color = new Color(255, 255, 255, 0); // Remove the opacity on the blank image
    }

    private void Update() {
        scoreText.text = "Score: " + score;
    }

    /* This method is used to increase the players score */
    public void IncreaseScore(int amount) {
        score += amount;
    }

    /* This methos is used show the key sprite in the UI when the player collects it */
    public void EquipKey() {
        keyImage.color = new Color(255, 255, 255, 100); // Add 100% opacity to the image so the key sprite shows
        keyImage.sprite = keySprite;
    }

    /* This method is used when the player collides with the door while the key is equipped */
    public void OpenExitDoor() {
        door.sprite = exitDoor;
        keyImage.color = new Color(255, 255, 255, 0);
        keyImage.sprite = null;
        Invoke("LoadNextLevel", 3f);
    }

    /* This method is used to load the next level after the player has opened the door with the key */
    private void LoadNextLevel() {
        //int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* This method returns the current number of lives */
    public int getLives() {
        return lives;
    }

    /* This method increases the players lives when thye pickup the increase lives potion */
    public void IncreaseLives() {
        lives++;
        switch (lives) {
            case 3:
                lifeImage.sprite = threeLives;
                break;
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

    /* This method removes a life from the player when they get hit by an enemy projectile */
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

    /* This method is called when the play has no lives left and the Game Over Lose Scene is set to active */
    private void EndGame() {
        SceneManager.LoadScene("Game Over Lose");
    }
}
