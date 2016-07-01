using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    [SerializeField]
    private AudioClip quit, click;

    [SerializeField]
    private AudioSource quitSource, clickSource;

    IEnumerator QuitAudio() {
        quitSource.PlayOneShot(quit);
        yield return new WaitForSeconds(3f);
        Application.Quit();
    }

    IEnumerator ClickAudio() {
        clickSource.PlayOneShot(click);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }


    public void Quit() {
        StartCoroutine(QuitAudio());
    }

    public void PlayTwoPlayerGame() {
        GameManager.instance.SetTwoPlayer();
        StartCoroutine(ClickAudio());
    }

    public void PlayOnePlayerGame() {
        GameManager.instance.SetSinglePlayer();
        StartCoroutine(ClickAudio());
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
