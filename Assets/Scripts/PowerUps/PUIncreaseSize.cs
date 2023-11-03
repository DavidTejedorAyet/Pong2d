using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUIncreaseSize : PowerUp {

    public float scaleIncrease = 2f;
    private Vector3 originalScale;

    public void IncreaseSize() {
        StartCoroutine(ScaleUpAndDown());
    }

    private IEnumerator ScaleUpAndDown() {
        // Aumentar el tamaño
        playerPowered.transform.localScale = new Vector3(originalScale.x, originalScale.y * scaleIncrease, originalScale.z);

        // Esperar durante la duración especificada
        yield return new WaitForSeconds(duration);

        // Volver al tamaño original
        playerPowered.transform.localScale = originalScale;
        PowerUpSpawner.Instance.PowerUpFinished();

    }
    public override void SetPowerUpAction() {
        originalScale = transform.localScale;
        IncreaseSize();
    }
}

