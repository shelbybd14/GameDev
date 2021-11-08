using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaLavaDivider : MonoBehaviour {

    private UIManager uiManager;

    private void Start() {
        uiManager = GameObject.FindObjectOfType(typeof(UIManager)) as UIManager;
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Knight") {
            uiManager.RemoveLife();
        }
    }

}
