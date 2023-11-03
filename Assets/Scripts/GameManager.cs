using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour {
    public bool isGameRunning = false;
    public GameObject ball;
    public GameObject player1;
    public GameObject player2;

    public int pointsToWin;
    private int player1Points;
    private int player2Points;
    

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void CheckWinner() {
        if (player1Points >= pointsToWin || player2Points >= pointsToWin) {
            UIManager.Instance.winnerPanel.SetActive(true);
            UIManager.Instance.winnerPlayerText.setColor(player1Points >= pointsToWin ? CustomColor.blue : CustomColor.red);
            UIManager.Instance.winnerPlayerText.setText(player1Points >= pointsToWin ? "Jugador 1" : "Jugador 2");
            isGameRunning = false;
            PowerUpSpawner.Instance.PowerUpFinished();
        }
    }
    private void PrepareBallForGame(GameObject player) { 
        ball.GetComponent<BallController>().AttachToPlayer(player: player);
    }

    public void Player1Scored() {
        player1Points++;
        PrepareBallForGame(player2);
        UIManager.Instance.player1ScoreText.setText(text: player1Points.ToString(), withAnimation: true);
        CheckWinner();
    } 
    public void Player2Scored() {
        player2Points++;
        PrepareBallForGame(player1);
        UIManager.Instance.player2ScoreText.setText(text: player2Points.ToString(), withAnimation: true);
        CheckWinner();
    }

    public void StartGame(bool withIA) {
        player2.GetComponent<PlayerController>().IAEnabled = withIA;
        StartGame();
        
    }
    public void StartGame() {
        isGameRunning = true;
        player1Points = 0;
        player2Points = 0;
        UIManager.Instance.player1ScoreText.setText(text: player1Points.ToString(), withAnimation: false);
        UIManager.Instance.player2ScoreText.setText(text: player2Points.ToString(), withAnimation: false);

        PrepareBallForGame(player: UnityEngine.Random.Range(1, 3) == 1 ? player1 : player2);
    }
}
