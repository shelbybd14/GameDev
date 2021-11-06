using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource throwSound;
    [SerializeField] private AudioSource keyPickup;

    [Header("Variables:")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float shootingDelay = 0.2f;
    [SerializeField] private bool isShooting = false;
    public bool isFacingUp;
    public bool isFacingDown;
    public bool isFacingLeft;
    public bool isFacingRight = true;


    [Header("Other Objects/Components:")]
    [SerializeField] private GameObject banana;
    private UIManager uiManager;

    // Animation States
    const string KNIGHT_LEFT = "Knight_walk_left";
    const string KNIGHT_RIGHT = "Knight_walk_right";
    const string KNIGHT_UP = "Knight_walk_up";
    const string KNIGHT_DOWN = "Knight_walk_down";

    private void Start() {
        animator = GetComponent<Animator>();
        animator.Play(KNIGHT_RIGHT); // Start off the knight facing right

        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;

        throwSound = GetComponent<AudioSource>();
        keyPickup = GetComponent<AudioSource>();
    }

    private void Update() {
        MoveKnight();

        if (Input.GetButtonDown("Fire1")) {
            if (isShooting)
                return;

            isShooting = true;
            Shoot();
            Invoke("ResetShooting", shootingDelay);
        }
    }

    private void MoveKnight() {
        // Move Up
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            ChangeAnimation(KNIGHT_UP);
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            setFacingUp();
        }
        // Moving Left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            ChangeAnimation(KNIGHT_LEFT);
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            setFacingLeft();
        }
        // Moving Down
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            ChangeAnimation(KNIGHT_DOWN);
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            setFacingDown();
        }
        // Moving Right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            ChangeAnimation(KNIGHT_RIGHT);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            setFacingRight();
        }
    }

    /* This method is used when the player shoots. An instance of the banana is instantiated in the direction the player is facing */
    private void Shoot() {
        throwSound.Play();
        if (isFacingLeft) {
            Vector3 leftShooting = new Vector3(transform.position.x - 0.08f, transform.position.y, transform.position.x);
            Instantiate(banana, leftShooting, Quaternion.Euler(new Vector3(-1, 0, 0)));
        }
        else {
            Vector3 upDownRightShooting = new Vector3(transform.position.x + 0.08f, transform.position.y, transform.position.x);
            Instantiate(banana, upDownRightShooting, Quaternion.Euler(new Vector3(-1, 0, 0)));
        }
    }

    /* This method is used to reset the shooting boolean to false */
    private void ResetShooting() {
        isShooting = false;
    }

    /* This method changes the knights animation */
    private void ChangeAnimation(string newState) {
        animator.Play(newState);
    }

    private void setFacingUp() {
        isFacingUp = true;
        isFacingDown = false;
        isFacingLeft = false;
        isFacingRight = false;
    }

    private void setFacingDown() {
        isFacingUp = false;
        isFacingDown = true;
        isFacingLeft = false;
        isFacingRight = false;
    }

    private void setFacingLeft() {
        isFacingUp = false;
        isFacingDown = false;
        isFacingLeft = true;
        isFacingRight = false;
    }

    private void setFacingRight() {
        isFacingUp = false;
        isFacingDown = false;
        isFacingLeft = false;
        isFacingRight = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Key") {
            Destroy(collision.gameObject);
            keyPickup.Play();
            uiManager.EquipKey();
        }

        if (collision.gameObject.tag == "ExitDoor") {
            uiManager.OpenExitDoor();
        }

        if (collision.gameObject.tag == "Apple" || collision.gameObject.tag == "FireBall") {
            uiManager.RemoveLife();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "IncreaseLivesPotion") {
            if (uiManager.getLives() != 3) {
                uiManager.IncreaseLives();
                Destroy(collision.gameObject);
            }
        }
    }
}