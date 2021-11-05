using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowfaceProjectile : MonoBehaviour {

    [Header("Components:")]
    [SerializeField] private new Rigidbody2D rigidbody;
    private CrowFace crowFace;

    [Header("Variables:")]
    [SerializeField] private float throwSpeed1 = 2f;
    [SerializeField] private float throwSpeed2 = -2f;

    private void Start() {
        crowFace = GameObject.FindObjectOfType(typeof(CrowFace)) as CrowFace;

        if (crowFace.isFacingRight) {
            rigidbody.velocity = new Vector2(throwSpeed1, 0);
        }
        else {
            rigidbody.velocity = new Vector2(throwSpeed2, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MazeEdge") {
            Destroy(gameObject);
        }
    }
}
