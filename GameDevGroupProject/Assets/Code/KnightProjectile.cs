using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightProjectile : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private new Rigidbody2D rigidbody;
    private Knight knight;

    [Header("Variables:")]
    [SerializeField] private float throwSpeed1 = 10f;
    [SerializeField] private float throwSpeed2 = -10f;

    private void Start() {
        knight = GameObject.FindObjectOfType(typeof(Knight)) as Knight;
           
        if (knight.isFacingUp) {
            rigidbody.velocity = new Vector2(0, throwSpeed1);
        } 
        if (knight.isFacingDown) {
            rigidbody.velocity = new Vector2(0, throwSpeed2);
        }
        if (knight.isFacingLeft) {
            rigidbody.velocity = new Vector2(throwSpeed2, 0);
        }
        if (knight.isFacingRight) {
            rigidbody.velocity = new Vector2(throwSpeed1, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MazeEdge") {
            Destroy(gameObject);
        }
    }
}
