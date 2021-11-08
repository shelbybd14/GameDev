using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    [Header("Components")]
    [SerializeField] private Animator animator;
    private UIManager uiManager;

    // Animation States
    const string BOSS_IDLE = "Boss_idle";
    const string BOSS_RIGHT = "Boss_right";
    const string BOSS_LEFT = "Boss_left";
    const string Boss_attack = "Boss_attack";


    [Header("Variables:")]
    [SerializeField] private int health = 300;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private bool isMovingUp;

    private int bananaDamage = 10;
    private int bossDeathPoints = 500;

    private float shootingCoolDown;
    private float shootingCoolDownTimer;

    private float topPoint;
    private float bottomPoint;

    [Header("Other Objects/Components:")]
    [SerializeField] private Transform knightTransform;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject bananaPeel;

    private void Start() {
        animator = GetComponent<Animator>();
        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;

        topPoint = transform.position.y + 0.1f;
        bottomPoint = transform.position.y - 0.1f;

        shootingCoolDown = RandomNumberGenerator();
    }

    private void Update() {
        MoveBoss();
        Shoot();
    }

    /* This method is used to handle the bosses movement */
    private void MoveBoss() {
        if (isMovingUp) {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            isMovingUp = true;
        }
        else {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            isMovingUp = false;
        }

        if (transform.position.y > topPoint) {
            isMovingUp = false;
        }
        if (transform.position.y < bottomPoint) {
            isMovingUp = true;
        }
    }
    
    /* This method is used to handle the bosses shooting */
    private void Shoot() {
        shootingCoolDownTimer -= Time.deltaTime;
        if (shootingCoolDownTimer > 0) {
            return;
        }

        shootingCoolDownTimer = shootingCoolDown;

        Vector3 shootingPosition = new Vector3(transform.position.x - 0.08f, transform.position.y, transform.position.x);
        Instantiate(fireBall, shootingPosition, transform.rotation);
    }

    private float RandomNumberGenerator() {
        return Random.Range(.8f, 1.5f);
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
        uiManager.WinGame();
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
        Vector3 position = new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z);
        Instantiate(bananaPeel, position, transform.rotation);
        Destroy(GameObject.FindGameObjectWithTag("BananaPeel"), 1f);
    }
}
 