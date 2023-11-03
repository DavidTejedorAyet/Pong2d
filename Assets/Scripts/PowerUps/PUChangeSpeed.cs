using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUChangeSpeed : PowerUp {
    public bool speedPowerUpActive = false;
    public float speedPowerUpAccelerationFactor;
    public float speedPowerUpDecelerationFactor;

    public override void SetPowerUpAction() {
        StartCoroutine(EnablePowerUp());
    }

    private IEnumerator EnablePowerUp() {
        Rigidbody2D ballRB = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
        speedPowerUpActive = true;
        yield return new WaitForSeconds(duration);
        speedPowerUpActive = false;
        PowerUpSpawner.Instance.PowerUpFinished();
    }

    private void ChangeSpeed(GameObject ball, GameObject playerCollided) {
        if (playerPowered.GetComponent<PlayerController>().selectedPlayerType == PlayerType.Player1) {
            // Si el jugador que recoge el powerup es el jugador 1 y golpea el jugador 1
            if (playerCollided.GetComponent<PlayerController>().selectedPlayerType == PlayerType.Player1) {
                ball.GetComponent<Rigidbody2D>().velocity *= speedPowerUpAccelerationFactor;
            } else {         // Si el jugador que recoge el powerup es el jugador 1 y golpea el jugador 2

                ball.GetComponent<Rigidbody2D>().velocity *= speedPowerUpDecelerationFactor;

            }
        } else {
            // Si el jugador que recoge el powerup es el jugador 2 y golpea el jugador 2

            if (playerCollided.GetComponent<PlayerController>().selectedPlayerType == PlayerType.Player2) {
                ball.GetComponent<Rigidbody2D>().velocity *= speedPowerUpAccelerationFactor;

            } else {         // Si el jugador que recoge el powerup es el jugador 2 y golpea el jugador 1

                ball.GetComponent<Rigidbody2D>().velocity *= speedPowerUpDecelerationFactor;

            }
        }
    }

    public void BallDidCollide(GameObject ball, GameObject playerCollided) {
        if (speedPowerUpActive) {
            ChangeSpeed(ball, playerCollided);
        }
    }
}
