using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public bool IAEnabled;
    private Rigidbody2D rb;
    GameObject ball;
    public PlayerType selectedPlayerType;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update() {
        if (!GameManager.Instance.isGameRunning) { return; }

        if (IAEnabled && selectedPlayerType == PlayerType.Player2) {
            AutomaticMovement();
        } else {
            PlayerMovement();
        }
    }

    void PlayerMovement() {
        float verticalMovement = 0f;

        if (Input.GetKey(selectedPlayerType == PlayerType.Player1 ? KeyCode.W : KeyCode.O)) {
            verticalMovement = 1f;
        } else if (Input.GetKey(selectedPlayerType == PlayerType.Player1 ? KeyCode.S : KeyCode.L)) {
            verticalMovement = -1f;
        }
        Vector2 movement = new Vector2(0f, verticalMovement) * speed;
        rb.velocity = new Vector2(rb.velocity.x, movement.y);
    }

    void AutomaticMovement() {
        if (ball != null) {
            float verticalMovement = 0f;

            if (ball.transform.position.y - transform.position.y > 2) {
                verticalMovement = 1;
            } else if (ball.transform.position.y - transform.position.y < -2) {
                verticalMovement = -1;
            }
            
            Vector2 movement = new Vector2(0f, verticalMovement) * speed;
            rb.velocity = new Vector2(rb.velocity.x, movement.y);
        }
    }
}
