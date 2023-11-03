using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUMoreBalls : PowerUp {
    public GameObject ball;
    public float speedVariation = 0.2f; 
    public float angleVariation = 15f; 
    public override void SetPowerUpAction() {

        StartCoroutine(CreateExtraBall());

    }

    private IEnumerator CreateExtraBall() {
        GameObject currentBall = GameObject.FindGameObjectWithTag("Ball");
        GameObject newBall = Instantiate(ball, currentBall.transform.position, Quaternion.identity);

        // Obtener la dirección y velocidad actuales de la bola en juego
        Vector2 currentVelocity = currentBall.GetComponent<Rigidbody2D>().velocity;

        // Aplicar variaciones
        float newSpeed = currentVelocity.magnitude + Random.Range(-speedVariation, speedVariation);
        float variationAngle = Random.Range(-angleVariation, angleVariation) * Mathf.Deg2Rad; // Convierte a radianes

        // Calcular la nueva dirección basada en el ángulo de variación
        Vector2 newDirection = new Vector2(
            currentVelocity.x * Mathf.Cos(variationAngle) - currentVelocity.y * Mathf.Sin(variationAngle),
            currentVelocity.x * Mathf.Sin(variationAngle) + currentVelocity.y * Mathf.Cos(variationAngle)
        ).normalized;

        newBall.GetComponent<Rigidbody2D>().velocity = newDirection * newSpeed;

        yield return new WaitForSeconds(duration);

        Destroy(newBall);
        PowerUpSpawner.Instance.PowerUpFinished();
    }

}
