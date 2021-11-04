using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private int score;
    private int lives = 3;

    private int currentScene;

    [Header("Text Components:")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Other Components:")]
    [SerializeField] private Image lifeImage;
    [SerializeField] private Image keyImage;
    [SerializeField] private Sprite keySprite;

    [SerializeField] private Image transitionScene;

    [SerializeField] private Sprite level1TransitionScreen;
    [SerializeField] private Sprite level2TransitionScreen;
    [SerializeField] private Sprite level3TransitionScreen;


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

        currentScene = SceneManager.GetActiveScene().buildIndex;
        transitionScene.color = new Color(255, 255, 255, 0);
        if (currentScene == 1) {
            transitionScene.color = new Color(255, 255, 255, 100);
            transitionScene.sprite = level1TransitionScreen;
            Invoke("RemoveLevel1Screen", 5f);
        }
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

        Invoke("LoadTransitionScene", 1f);

        Invoke("LoadNextLevel", 5f);
    }

    private void RemoveLevel1Screen() {
        transitionScene.color = new Color(255, 255, 255, 0);
    }

    private void LoadTransitionScene() {
        transitionScene.color = new Color(255, 255, 255, 100);

        if (currentScene == 1) {
            transitionScene.sprite = level2TransitionScreen;
        }
        else if (currentScene == 2) {
            transitionScene.sprite = level3TransitionScreen;
        }
    }

    /* This method is used to load the next level after the player has opened the door with the key */
    private void LoadNextLevel() {
        SceneManager.LoadScene(currentScene + 1);
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
