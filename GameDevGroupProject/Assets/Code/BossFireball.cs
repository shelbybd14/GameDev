using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireball : MonoBehaviour {

    [Header("Componenets:")]
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Variables")]
    [SerializeField] private float speed = -3f;

    private void Start() {
        speed = -3f;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody.velocity = new Vector2(speed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "MazeEdge") {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Banana") {
            Destroy(collision.gameObject);
        }
    }
}
