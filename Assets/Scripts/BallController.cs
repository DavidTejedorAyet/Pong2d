using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    private int hitCount = 0;
    private bool attachedToPlayer;
    public GameObject playerAttached;
    private PlayerType playerType;
    public GameObject lastPlayerHit;
    private Rigidbody2D rb;
    private TrailRenderer trailRenderer;
    public float forceMagnitude = 10f;
    public float speedMultiplyFactor = 1.1f;



    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update() {
        if (attachedToPlayer) {
            FollowPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Space) && attachedToPlayer) {
            PushBall();
        }
    }

    void FollowPlayer() {
        trailRenderer.startColor = playerType == PlayerType.Player1 ? CustomColor.blue : CustomColor.red;
        playerType = playerAttached.GetComponent<PlayerController>().selectedPlayerType;
        float distance = playerType == PlayerType.Player1 ? 5 : -5;
        Vector2 position = new Vector2(x: playerAttached.transform.position.x + distance, y: playerAttached.transform.position.y);
        transform.position = position;
    }

    void PushBall() {
        trailRenderer.enabled = true;
        attachedToPlayer = false;

        float angle = Random.Range(-45f, 45f); // Ángulo aleatorio entre -45 y 45 grados
        Vector2 forceDirection;
        if (playerType == PlayerType.Player1) {
            // Fuerza hacia la derecha para el Player1
            forceDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        } else {
            // Fuerza hacia la izquierda para el Player2
            forceDirection = new Vector2(-Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }

        Vector2 force = forceDirection * forceMagnitude;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void AttachToPlayer(GameObject player) {
        trailRenderer.enabled = false;
        attachedToPlayer = true;
        rb.velocity = Vector2.zero;
        playerAttached = player;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        AudioManager.Instance.PlayHitWallClip();
        hitCount++;
        if (hitCount % 5 == 0) {
            rb.velocity = rb.velocity * speedMultiplyFactor;
        }
        if (collision.gameObject.CompareTag("Player1")) {
            lastPlayerHit = GameManager.Instance.player1;
            trailRenderer.startColor = CustomColor.blue;
            PowerUpSpawner.Instance.BallDidCollide(gameObject, GameManager.Instance.player1);
        } else if (collision.gameObject.CompareTag("Player2")) {
            lastPlayerHit = GameManager.Instance.player2;
            trailRenderer.startColor = CustomColor.red;
            PowerUpSpawner.Instance.BallDidCollide(gameObject, GameManager.Instance.player2);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "leftWall") {
            AudioManager.Instance.PlayHitGoalClip();
            GameManager.Instance.Player2Scored();
        } else if (collision.tag == "rightWall") {
            AudioManager.Instance.PlayHitGoalClip();
            GameManager.Instance.Player1Scored();
        } else if ((collision.tag == "")) {
            AudioManager.Instance.PlayHitPowerUpClip();
        }
    }
}
