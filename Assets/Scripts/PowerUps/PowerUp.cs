using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour, PowerUpInterface {
    public float duration = 5f;
    public bool inGameEnabled = false;
    public Sprite spriteImage;
    protected GameObject playerPowered;

    private void Awake() {
        spriteImage = GetComponent<SpriteRenderer>().sprite;
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        // Al colisionar con la bola activamos el efecto y movemos el item fuera de la cámara
        if (collision.gameObject.CompareTag("Ball")) {
            playerPowered = collision.gameObject.GetComponent<BallController>().lastPlayerHit;
            AudioManager.Instance.PlayHitPowerUpClip();
            UIManager.Instance.SetPowerUpImage(playerPowered.GetComponent<PlayerController>().selectedPlayerType, this);
            SetPowerUpAction();
            inGameEnabled = false;
            transform.Translate(new Vector2(-100, -100));
        }
    }

    public abstract void SetPowerUpAction();
}

interface PowerUpInterface {
    void SetPowerUpAction();
}