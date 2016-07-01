using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {

    public static StateManager instance;
    public bool gameOver = false;
    public bool startMenu = true;

    public int currentCameraEffect;

    [SerializeField]
    private GameObject leftPaddle, rightPaddle, ball, gameOverPanel, startPanel, pausePanel;
    
    [SerializeField]
    private GameObject[] cameraEffects;

    private void CreateSingleton() {
        if (instance == null) {
            instance = this;
        }
    }

    void Awake() {
        SetRandomCameraEffect();
        CreateSingleton();

        startPanel.SetActive(true);
    }

    private void DeactivateAllCameraEffects() {
        for(int i = 0; i < cameraEffects.Length; i++) {
            cameraEffects[i].SetActive(false);
        }
    }

    public void SetRandomCameraEffect() {
        DeactivateAllCameraEffects();
        currentCameraEffect = Random.Range(0, cameraEffects.Length - 1);

        cameraEffects[currentCameraEffect].SetActive(true);
    }

    public void NextCameraEffect() {
        if (currentCameraEffect == cameraEffects.Length - 1) {
            currentCameraEffect = 0;
        } else {
            currentCameraEffect++;
        }

        DeactivateAllCameraEffects();
        cameraEffects[currentCameraEffect].SetActive(true);
    }

    void GameOver() {
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameOver = true;
        gameOverPanel.SetActive(true);
    }


    public void NewGame() {
        gameOver = false;
        gameOverPanel.SetActive(false);
        ScoreManager.instance.ResetScores();
        ball.transform.position = new Vector3(0f, -0.19f, 1.11f);
        ball.GetComponent<Ball>().ResetBall();
    }

    void CheckForEndGameScores() {
        if(ScoreManager.instance.GetPlayer1Score() == 11) {
            GameOver();
        } else if(ScoreManager.instance.GetPlayer2Score() == 11) {
            GameOver();
        }
    }

    void Update() {
        CheckForEndGameScores();
    }

    public bool isLocalCoop = true;

}
