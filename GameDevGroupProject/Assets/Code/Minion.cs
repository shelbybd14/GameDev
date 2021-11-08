using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    // Animation States
    const string MINION_UP = "WalkUp";
    const string MINION_DOWN = "WalkDown";
    const string MINION_LEFT = "WalkLeft";
    const string MINION_RIGHT = "WalkRight";

    [Header("Variables:")]
    [SerializeField] private int health = 100;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] public bool isFacingRight = true;
    private int bananaDamage = 50;
    private int minionDeathPoints = 10;

    [SerializeField] private float attackDistance = 0.05f;
    //[SerializeField] private float followDistance = 0.1f;

    private float shootingCoolDown;
    private float shootingCoolDownTimer;

    private float rightPoint;
    private float leftPoint;

    [Header("Other Components:")]
    [SerializeField] private Transform knightTransform;
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject bananaPeel;
    private UIManager uiManager;

    private void Start() {
        animator = GetComponent<Animator>();
        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;

        ChangeAnimation(MINION_RIGHT);

        rightPoint = transform.position.x + 0.1f;
        leftPoint = transform.position.x - 0.1f;

        shootingCoolDown = RandomNumberGenerator();
    }


    private void Update() {
        if (CheckAttackDistance(knightTransform.position.x, transform.position.x)) {
            moveSpeed = 0;
            if (knightTransform.position.x > transform.position.x) {
                spriteRenderer.flipX = false;
            }
            else if (knightTransform.position.x < transform.position.x) {
                spriteRenderer.flipX = true;
            }
            Shoot();
        }
        else {
            moveSpeed = 0.3f;
            MoveMinion();
        }
    }

    /* This method is used to handle the minions shooting */
    private void Shoot() {
        shootingCoolDownTimer -= Time.deltaTime;
        if (shootingCoolDownTimer > 0) {
            return;
        }

        shootingCoolDownTimer = shootingCoolDown;

        if (isFacingRight) {
            Vector3 rightShooting = new Vector3(transform.position.x + 0.08f, transform.position.y, transform.position.x);
            Instantiate(apple, rightShooting, Quaternion.Euler(new Vector3(-1, 0, 0)));
        }
        else {
            Vector3 leftShooting = new Vector3(transform.position.x - 0.08f, transform.position.y, transform.position.x);
            Instantiate(apple, leftShooting, Quaternion.Euler(new Vector3(-1, 0, 0)));
        }
    }

    /* This method is used to handle the minions movement */
    private void MoveMinion() {
        if (isFacingRight) {
            ChangeAnimation(MINION_RIGHT);
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            isFacingRight = true;
        }
        else {
            ChangeAnimation(MINION_LEFT);
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            isFacingRight = false;
        }

        if (transform.position.x > rightPoint)
            isFacingRight = false;

        if (transform.position.x < leftPoint)
            isFacingRight = true;
    }

    private float RandomNumberGenerator() {
        return Random.Range(1.5f, 3f);
    }

    /* This method is used to check if the player is within attacking distance of the minion */
    private bool CheckAttackDistance(float knightPosition, float minionPosition) {
        if (Mathf.Abs(knightPosition - minionPosition) < attackDistance)
            return true;

        return false;
    }

    /* This method removes a life from the minion when it gets hit */
    private void RemoveHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            KillMinion();
        }
    }

    /* This method is used to destroy the minion when it has no health left */
    private void KillMinion() {
        Destroy(gameObject);
        uiManager.IncreaseScore(minionDeathPoints);
    }

    /* This method is used to change the minions animation */
    private void ChangeAnimation(string newState) {
        animator.Play(newState);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Banana") {
            RemoveHealth(bananaDamage);
            Destroy(collision.gameObject);
            CreateBananaPeel();
        }
    }

    /* This method creates an instance of the banana peel prefab when the minion gets hits and then destroys it one second later */
    private void CreateBananaPeel() {
        Vector3 position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
        Instantiate(bananaPeel, position, transform.rotation);
        Destroy(GameObject.FindGameObjectWithTag("BananaPeel"), 1f);
    }

}
