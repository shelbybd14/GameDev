using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private Animator animator;

    // Animation States
    const string MINION_UP = "WalkUp";
    const string MINION_DOWN = "WalkDown";
    const string MINION_LEFT = "WalkLeft";
    const string MINION_RIGHT = "WalkRight";

    [Header("Variables:")]
    [SerializeField] private int health = 100;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool isShooting = false;
    private float shootingDelay = 0.5f;
    private int bananaDamage = 50;
    private int minionDeathPoints = 10;

    [SerializeField] private float attackDistance = 0.05f;
    [SerializeField] private float followDistance = 0.1f;

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
    }


    private void Update() {
        //if (CheckAttackDistance(knightTransform.position.x, transform.position.x)) {
        //    if (isShooting) {
        //        return;
        //    }
        //    else {
        //        Shoot();
        //    }
        //}
        //else {
        //    MoveMinion();
        //}
        MoveMinion();
    }

    private void FollowPlayer() {
        
    }

    private void Shoot() {
        Instantiate(apple, transform.position, transform.rotation);
        isShooting = false;

        Invoke("ResetShooting", 2000f);
    }

    private void ResetShooting() {
        isShooting = true;
    }

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

    private bool CheckFollowDistance(float knightPosition, float minionPosition) {
        if (Mathf.Abs(knightPosition - minionPosition) < followDistance)
            return true;

        return false;
    }

    private bool CheckAttackDistance(float knightPosition, float minionPosition) {
        if (Mathf.Abs(knightPosition - minionPosition) < attackDistance)
            return true;

        return false;
    }

    private void RemoveHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            KillMinion();
        }
    }

    private void KillMinion() {
        Destroy(gameObject);
        uiManager.IncreaseScore(minionDeathPoints);
    }

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

    private void CreateBananaPeel() {
        Vector3 position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
        Instantiate(bananaPeel, position, transform.rotation);
        Destroy(GameObject.FindGameObjectWithTag("BananaPeel"), 1f);
    }

}
