using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private Animator animator;

    [Header("Variables:")]
    [SerializeField] private float moveSpeed = 5f;

    // Animation States
    const string KNIGHT_LEFT = "Knight_walk_left";
    const string KNIGHT_RIGHT = "Knight_walk_right";
    const string KNIGHT_UP = "Knight_walk_up";
    const string KNIGHT_DOWN = "Knight_walk_down";

    private void Start() {
        animator = GetComponent<Animator>();
        animator.Play(KNIGHT_RIGHT); // Start off the knight facing right
    }

    private void Update() {
        MoveKnight();
    }

    private void MoveKnight() {
        float xPosition = transform.position.x;
        float yPosition = transform.position.y;
        float xMax = 1.38f;
        float xMin = -1.3f;
        float yMax = 0.72f;
        float yMin = -0.72f;

        // Move Up
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            ChangeAnimation(KNIGHT_UP);
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        // Moving Left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            ChangeAnimation(KNIGHT_LEFT);
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        // Moving Down
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            ChangeAnimation(KNIGHT_DOWN);
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
        // Moving Right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            ChangeAnimation(KNIGHT_RIGHT);
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

        // Boundary Checking
        if (xPosition > xMax) {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        } else if (xPosition < xMin) {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        } else if (yPosition > yMax) {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        } else if (yPosition < yMin) {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }

    }

    /* This method changes the knights animation */
    private void ChangeAnimation(string newState) {
        animator.Play(newState);
    }
}
