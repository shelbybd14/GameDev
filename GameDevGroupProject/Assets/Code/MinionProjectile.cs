using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionProjectile : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private new Rigidbody2D rigidbody;
    //private Knight knight;

    [Header("Variables:")]
    [SerializeField] private float throwSpeed1 = 10f;
    [SerializeField] private float throwSpeed2 = -10f;

    private void Start() {
        rigidbody.velocity = new Vector2(throwSpeed1, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MazeEdge") {
            Destroy(gameObject);
        }
    }
}
