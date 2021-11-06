using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowFace : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource deathSound;

    // Animations States
    const string CROWFACE_IDLE = "Idle";
    const string CROWFACE_WALK_UP = "WalkUp";
    const string CROWFACE_WALK_DOWN = "WalkDown";
    const string CROWFACE_WALK_LEFT = "WalkLeft";
    const string CROWFACE_WALK_RIGHT = "WalkRight";

    [Header("Variables:")]
    [SerializeField] public bool isFacingRight = true;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private float attackDistance;
    [SerializeField] private float followDistance;
    [SerializeField] private int health = 100;
    private int bananaDamage = 20;
    private int crowfaceDeathPoints = 50;

    private float shootingCoolDown;
    private float shootingCoolDownTimer;

    private float rightPoint;
    private float leftPoint;


    [Header("Other Objects/Components:")]
    [SerializeField] private Transform knightTransform;
    [SerializeField] private GameObject apple;
    [SerializeField] private GameObject bananaPeel;
    [SerializeField] private GameObject key;
    private UIManager uiManager;

    private void Start() {
        animator = GetComponent<Animator>();
        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
        deathSound = GetComponent<AudioSource>();

        ChangeAnimation(CROWFACE_IDLE);

        rightPoint = transform.position.x + 0.1f;
        leftPoint = transform.position.x - 0.1f;

        shootingCoolDown = RandomNumberGenerator();
    }


    private void Update() {
        if (CheckAttackDistance(knightTransform.position.x, transform.position.x)) {
                moveSpeed = 0;
            if (knightTransform.position.x > transform.position.x) {
                //moveSpeed = 0;
                spriteRenderer.flipX = false;
            }
            else if (knightTransform.position.x < transform.position.x) {
                spriteRenderer.flipX = true;
                //Shoot();
            }
            Shoot();
        }
        else {
            moveSpeed = 0.3f;
            MoveCrowface();
        }
    }

    /* This method is used to move the crowface between two points while it is not in attacking range */
    private void MoveCrowface() {
        if (isFacingRight) {
            ChangeAnimation(CROWFACE_WALK_RIGHT);
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            isFacingRight = true;
        }
        else {
            ChangeAnimation(CROWFACE_WALK_LEFT);
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            isFacingRight = false;
        }

        if (transform.position.x > rightPoint)
            isFacingRight = false;

        if (transform.position.x < leftPoint)
            isFacingRight = true;
    }

    private void Shoot() {
        shootingCoolDownTimer -= Time.deltaTime;
        if (shootingCoolDownTimer > 0) {
            return;
        }

        shootingCoolDownTimer = shootingCoolDown;

        if (isFacingRight) {
            Vector3 rightShooting = new Vector3(transform.position.x + 0.08f, transform.position.y, transform.position.x);
            Instantiate(apple, rightShooting, Quaternion.Euler(new Vector3(-1, 0, 0)));
        } else {
            Vector3 leftShooting = new Vector3(transform.position.x - 0.08f, transform.position.y, transform.position.x);
            Instantiate(apple, leftShooting, Quaternion.Euler(new Vector3(-1, 0, 0)));
        }
    }

    private float RandomNumberGenerator() {
        return Random.Range(2f, 4f);
    }

    private bool CheckFollowDistance(float knightPosition, float crowfacePosition) {
        if (Mathf.Abs(knightPosition - crowfacePosition) < followDistance)
            return true;

        return false;
    }

    private bool CheckAttackDistance(float knightPosition, float crowfacePosition) {
        if (Mathf.Abs(knightPosition - crowfacePosition) < attackDistance)
            return true;

        return false;
    }

    /* This method is used to remove health from the crowface when it gets hit */
    private void RemoveHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            deathSound.Play();
            KillCrowface();
        }
    }

    /* This method is used to kill the crowface when it has no health left */
    private void KillCrowface() {
        Destroy(gameObject);
        uiManager.IncreaseScore(crowfaceDeathPoints);
        CreateKey();
    }

    /* This method changes the crowface's animation */
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

    /* This method creates an instance of the banana peel when the enemy gets hit with a banana and then destroys it after 1 second */
    private void CreateBananaPeel() {
        Vector3 position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
        Instantiate(bananaPeel, position, transform.rotation);
        Destroy(GameObject.FindGameObjectWithTag("BananaPeel"), 1f);
    }

    /* This method creates an instance of the key when the crowface gets killed */
    private void CreateKey() {
        Vector3 position = new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z);
        Instantiate(key, position, transform.rotation);
    }
}
