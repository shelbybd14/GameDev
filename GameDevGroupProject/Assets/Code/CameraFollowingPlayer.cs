using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour {

    [Header("Components/Game Objects:")]
    [SerializeField] private Transform playerTransform;

    private void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Knight").transform;
    }

    private void LateUpdate() {
        Vector3 temp = transform.position;
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;
        transform.position = temp;
    }
}
