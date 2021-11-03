using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private Animator animator;

    // Animation States
    const string MINION_UP = "";
    const string MINION_DOWN = "";
    const string MINION_LEFT = "";
    const string MINION_RIGHT = "";

    [Header("Variables:")]
    [SerializeField] private int health = 100;
    [SerializeField] private float moveSpeed = 0.5f;
    private int bananaDamage = 20;
    private int minionDeathPoints = 10;

    [Header("Other Components:")]
    [SerializeField] private Transform knightTransform;
    [SerializeField] private GameObject bananaPeel;
    private UIManager uiManager;

    private void Start() {
        animator = GetComponent<Animator>();
        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
    }


    private void Update() {
        
    }

    private void RemoveHealth(int amount) {
        health -= amount;
        if (health <= 0) {
            uiManager.IncreaseScore(bananaDamage);
            KillMinion();
        }
    }

    private void KillMinion() {
        Destroy(gameObject);
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
        GameObject bananaPeelInstance = (GameObject) Instantiate(bananaPeel, transform.position, transform.rotation);
        Destroy(bananaPeelInstance, 0.5f);
    }
}
