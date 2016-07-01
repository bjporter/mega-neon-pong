using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void Quit() {
        Application.Quit();
    }

    public void PlayTwoPlayerGame() {
        GameManager.instance.SetTwoPlayer();
        SceneManager.LoadScene("Game");
    }

    public void PlayOnePlayerGame() {
        GameManager.instance.SetSinglePlayer();
        SceneManager.LoadScene("Game");
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
