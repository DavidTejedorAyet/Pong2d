using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUReduceSize : PowerUp {

    public float scaleIncrease = 0.5f;
    private Vector3 originalScale;

    public void IncreaseSize() {
        StartCoroutine(ScaleUpAndDown());
    }

    private IEnumerator ScaleUpAndDown() {
        GameObject target = playerPowered.GetComponent<PlayerController>().selectedPlayerType == PlayerType.Player1 ? GameManager.Instance.player2 : GameManager.Instance.player1;
        // Reduce el tamaño
        Debug.Log(target, playerPowered);

        target.transform.localScale = new Vector3(originalScale.x, originalScale.y * scaleIncrease, originalScale.z);

        // Esperar durante la duración especificada
        yield return new WaitForSeconds(duration);

        // Volver al tamaño original
        target.transform.localScale = originalScale;
        PowerUpSpawner.Instance.PowerUpFinished();
    }
    public override void SetPowerUpAction() {
        originalScale = transform.localScale;
        IncreaseSize();
    }
}
