using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {
    public PowerUp[] powerUps; 
    public Vector2 timeRange = new Vector2(); // Rango de tiempo para la instanciación.
    public Vector2 xBounds = new Vector2(); // Límites en X del área.
    public Vector2 yBounds = new Vector2(); // Límites en Y del área.
    private PowerUp activePowerUp;
    public static PowerUpSpawner Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private void Start() {
        StartCoroutine(SpawnPowerUp());
    }

    private IEnumerator SpawnPowerUp() {
        while (true) {
            // Si hay un powerUp activo, esperar y continuar con la siguiente iteración.
            if (activePowerUp != null && activePowerUp.inGameEnabled || !GameManager.Instance.isGameRunning) {
                yield return null;
                continue;
            }

            // Esperar un tiempo aleatorio.
            float waitTime = Random.Range(timeRange.x, timeRange.y);
            yield return new WaitForSeconds(waitTime);

            // Seleccionar un powerUp aleatorio y activarlo.
            int randomIndex = Random.Range(0, powerUps.Length);
            activePowerUp = powerUps[randomIndex];
            activePowerUp.inGameEnabled = true;

            // Configurar una posición aleatoria dentro del área definida.
            float randomX = Random.Range(xBounds.x, xBounds.y);
            float randomY = Random.Range(yBounds.x, yBounds.y);
            activePowerUp.transform.position = new Vector3(randomX, randomY, 0);
        }
    }

    public void BallDidCollide(GameObject ball, GameObject playerCollided) {
        if (activePowerUp is PUChangeSpeed powerUp) {
            powerUp.BallDidCollide(ball, playerCollided);
        }
    }

    public void PowerUpFinished() {
        activePowerUp.inGameEnabled = false;
        UIManager.Instance.HidePowerUpIcons();
    }
}
