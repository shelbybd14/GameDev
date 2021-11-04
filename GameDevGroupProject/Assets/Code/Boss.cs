using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [Header("Components")]
    [SerializeField] private Animator animator;
    private UIManager uiManager;

    // Animation States
    const string BOSS_IDLE = "boss_idle";
    const string BOSS_RIGHT = "";
    const string BOSS_LEFT = "";
    const string BOSS_UP = "";
    const string BOSS_DOWN = "";
    const string BOSS_DEATH = "";

    [Header("Variables:")]
    [SerializeField] private int health = 300;
    [SerializeField] private float moveSpeed = 0.3f;

    private int bananaDamage = 10;
    private int bossDeathPoints = 500;

    [Header("Other Objects/Components:")]
    [SerializeField] private Transform knightTransform;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject bananaPeel;

    void Start() {
        animator = GetComponent<Animator>();
        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
    }

    void Update() {
        
    }

    /* This method changes the bosses animation */
    private void ChangeAnimation(string newState) {
        animator.Play(newState);
    }

    /* This method is used to remove health from the boss when it gets hit */
    private void RemoveHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            uiManager.IncreaseScore(bananaDamage);
            KillBoss();
        }
    }

    /* This method is used to kill the boss when it has no health left */
    private void KillBoss() {
        Destroy(gameObject);
        uiManager.IncreaseScore(bossDeathPoints);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Banana") {
            RemoveHealth(bananaDamage);
            Destroy(collision.gameObject);
            CreateBananaPeel();
        }
    }

    /* This method creates an instance of the banana peel when the enemy gets hit with a banana and then destroys it after 1 second */
    private void CreateBananaPeel() {
        Vector3 position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
        Instantiate(bananaPeel, position, transform.rotation);
        Destroy(GameObject.FindGameObjectWithTag("BananaPeel"), 1f);
    }
}
 