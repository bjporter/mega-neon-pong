using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private int player1Score = 0;
    private int player2Score = 0;
    private int maxScore = 5;

    public static ScoreManager instance;

    [SerializeField]
    private Text player1ScoreText, player2ScoreText, player1ScoreTextBack, player2ScoreTextBack;

    private void CreateSingleton() {
        if (instance == null) {
            instance = this;
        }
    }

    public void ResetScores() {
        player1Score = 0;
        player2Score = 0;

        player1ScoreText.text = player1Score + "";
        player2ScoreText.text = player2Score + "";

        player1ScoreTextBack.text = player1Score + "";
        player2ScoreTextBack.text = player2Score + "";
    }

    public int GetPlayer1Score() {
        return player1Score;
    }

    public int GetPlayer2Score() {
        return player2Score;
    }

    void Awake() {
        CreateSingleton();
        //Debug.Log("Testing menu variable: " + GameManager.instance.gameType);
    }

    void Start() {
        ResetScores();
    }

    public void IncrementPlayer2Score() {
        if(player2Score == maxScore) {
            return;
        }

        player2Score += 1;
        player2ScoreText.text = player2Score + "";
        player2ScoreTextBack.text = player2Score + "";
    }

    public void IncrementPlayer1Score() {
        if (player1Score == maxScore) {
            return;
        }

        player1Score += 1;
        player1ScoreText.text = player1Score + "";
        player1ScoreTextBack.text = player1Score + "";
    }

}
