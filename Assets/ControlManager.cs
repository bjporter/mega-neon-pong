using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour {

    public static ControlManager instance;

    private float paddleSpeed = 18f;
    private float borderTop = 4.6f;
    private float borderBottom = -7.26f;

    [SerializeField]
    private GameObject leftPaddleP1, rightPaddleP2, renderTexture, pausePanel, startPanel, gameOverPanel;

    [SerializeField]
    private AudioClip menuClick;

    [SerializeField]
    private AudioSource menuClickSource;

    void PlayMenuClick() {
        menuClickSource.PlayOneShot(menuClick);
    }

    private void CreateSingleton() {
        if(instance == null) {
            instance = this;
        }
    }

    void Awake() {
        CreateSingleton();
    }

	void Start () {
	
	}

    float BorderTopCheck(Vector3 t) {
        if (t.y + paddleSpeed * Time.deltaTime < borderTop) {
            t.y += paddleSpeed * Time.deltaTime;
            return t.y;
        }
        else {
            return borderTop;
        }
    }

    float BorderBottomCheck(Vector3 t) {
        if (t.y - paddleSpeed * Time.deltaTime > borderBottom) {
            t.y -= paddleSpeed * Time.deltaTime;
            return t.y;
        }
        else {
            return borderBottom;
        }
    }

    void Update () {
        //if(Input.GetKeyDown(KeyCode.Escape)) {
          //  Application.Quit();
        //}

        //GAME OVER PANEL
        if(StateManager.instance.gameOver) {
            if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
                StateManager.instance.NewGame();
                PlayMenuClick();
            }
        }

        //CHANGE STYLE
        if (Input.GetKeyDown(KeyCode.Y)) {
            StateManager.instance.NextCameraEffect();
            PlayMenuClick();
        }

        if (!StateManager.instance.gameOver) {

            //GAME START PANEL
            if (startPanel.activeInHierarchy) {
                if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
                    StateManager.instance.StartGame();
                    PlayMenuClick();
                }
            }

            //OPTIONS / MENU PANEL
            if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P)) {
                if (!pausePanel.activeInHierarchy) {
                    pausePanel.SetActive(true);
                    StateManager.instance.PauseGame();
                    PlayMenuClick();
                }
            }

            if(pausePanel.activeInHierarchy) {
                if((Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))) {
                    PlayMenuClick();
                    pausePanel.SetActive(false);
                    StateManager.instance.UnPauseGame();
                } else if(Input.GetKeyUp(KeyCode.M)) {
                    PlayMenuClick();
                    pausePanel.SetActive(false);
                    StateManager.instance.GoToMainMenu();
                }
            }

            if(!startPanel.activeInHierarchy && !gameOverPanel.activeInHierarchy && !pausePanel.activeInHierarchy) { 
                if (Input.GetKey(KeyCode.W)) {
                    Debug.Log("player 1 up");
                    Vector3 t = leftPaddleP1.transform.position;
                    t.y = BorderTopCheck(t);
                    leftPaddleP1.transform.position = t;
                }
                else if (Input.GetKey(KeyCode.S)) {
                    Debug.Log("palyer 1 down");
                    Vector3 t = leftPaddleP1.transform.position;
                    t.y = BorderBottomCheck(t);
                    leftPaddleP1.transform.position = t;
                }

                if (GameManager.instance.IsTwoPlayer()) {
                    if (Input.GetKey(KeyCode.UpArrow)) {
                        Debug.Log("player 2 up");
                        Vector3 t = rightPaddleP2.transform.position;
                        t.y = BorderTopCheck(t);
                        rightPaddleP2.transform.position = t;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow)) {
                        Debug.Log("player 2 down");
                        Vector3 t = rightPaddleP2.transform.position;
                        t.y = BorderBottomCheck(t);
                        rightPaddleP2.transform.position = t;
                    }
                }
            }

           /*if (Input.GetKey(KeyCode.Minus)) {
                Debug.Log("render texture scale down");
                Vector3 t = renderTexture.transform.localScale;
                t *= (0.99975f * Time.deltaTime);
                renderTexture.transform.localScale = t;
            }
            else if (Input.GetKey(KeyCode.Equals)) {
                Debug.Log("render texture scale up");
                Vector3 t = renderTexture.transform.localScale;
                t = (t * 1.000125f);
                renderTexture.transform.localScale = t;
            }*/
        }
    }
}
