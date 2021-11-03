using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScenes : MonoBehaviour {

    public void PlayAgain() {
        SceneManager.LoadScene("Level 1");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Start Menu");
    }
}
