using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionProjectile : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private new Rigidbody2D rigidbody;
    private Minion minion;

    [Header("Variables:")]
    [SerializeField] private float throwSpeed1 = 2f;
    [SerializeField] private float throwSpeed2 = -2f;

    private void Start() {
        minion = GameObject.FindObjectOfType(typeof(Minion)) as Minion;

        if (minion.isFacingRight) {
            rigidbody.velocity = new Vector2(throwSpeed1, 0);
        } else {
            rigidbody.velocity = new Vector2(throwSpeed2, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MazeEdge" || collision.gameObject.tag == "ExitDoor") {
            Destroy(gameObject);
        }
    }
}
