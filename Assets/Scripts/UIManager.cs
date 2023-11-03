using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject winnerPanel;
    public GameObject mainMenuPanel;

    public Image powerUpPlayer1;
    public Image powerUpPlayer2;

    public CustomText player1ScoreText;
    public CustomText player2ScoreText;
    public CustomText winnerPlayerText;
    public static UIManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    public void playAgainBtnTapped() {
        winnerPanel.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void onePlayerBtnTapped() {
        mainMenuPanel.SetActive(false);
        GameManager.Instance.StartGame(withIA: true);
    }
    public void twoPlayersBtnTapped() {
        mainMenuPanel.SetActive(false);
        GameManager.Instance.StartGame(withIA: false);
    }

    public void SetPowerUpImage(PlayerType player, PowerUp powerUp) {
        if (player == PlayerType.Player1) {
            powerUpPlayer1.gameObject.SetActive(true);
            powerUpPlayer1.sprite = powerUp.spriteImage;
        } else {
            powerUpPlayer2.gameObject.SetActive(true);
            powerUpPlayer2.sprite = powerUp.spriteImage;
        }
    }

    public void HidePowerUpIcons() {
        powerUpPlayer1.gameObject.SetActive(false);
        powerUpPlayer2.gameObject.SetActive(false);
    }
}
