using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour {

    public static StateManager instance;
    public bool gameOver = false;
    public bool startMenu = true;
    private Vector2 pausedBallVelocity;
    private bool gamePaused = true;

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

    public void GoToMainMenu() {
        Debug.Log("WORKED");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame() {
        Debug.Log("WORKED");
        Application.Quit();
    }

    public void PauseGame() {
        gamePaused = true;
        pausedBallVelocity = ball.GetComponent<Rigidbody2D>().velocity;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void UnPauseGame() {
        gamePaused = false;
        ball.GetComponent<Rigidbody2D>().velocity = pausedBallVelocity;
    }

    public bool IsGamePaused() {
        return gamePaused;
    }

    public void StartGame() {
        gamePaused = false;
        startPanel.SetActive(false);
        ball.GetComponent<Ball>().ResetBall();
    }

    void Awake() {
        SetRandomCameraEffect();
        CreateSingleton();

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
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
        gamePaused = true;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void Player1Scored() {
        ScoreManager.instance.IncrementPlayer1Score();
        gamePaused = true;
        StartCoroutine(ServeBall("Right"));
    }

    public void Player2Scored() {
        ScoreManager.instance.IncrementPlayer2Score();
        gamePaused = true;
        StartCoroutine(ServeBall("Left"));
    }

    IEnumerator ServeBall(string direction) {
        ball.transform.position = new Vector3(0f, -0.19f, 1.11f);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return new WaitForSeconds(1.5f);
        gamePaused = false;
        ball.GetComponent<Ball>().ResetBall();
    }

    public void NewGame() {
        gameOver = false;
        gameOverPanel.SetActive(false);
        ScoreManager.instance.ResetScores();
        ball.transform.position = new Vector3(0f, -0.19f, 1.11f);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(NewGameStart());
    }

    IEnumerator NewGameStart() {
        yield return new WaitForSeconds(1.5f);
        gamePaused = false;
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
