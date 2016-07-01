using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    private Rigidbody2D rigidBody;

    private float glitchSeconds = 0.2f;
    private bool glitchStarted = false;

    [SerializeField]
    private Kino.AnalogGlitch glitch;

    [SerializeField]
    private RippleEffectEdit rippleEffect;

    [SerializeField]
    private AudioSource wallSoundSource, leftPaddleHitSource, rightPaddleHitSource;

    [SerializeField]
    private AudioClip wallSoundClip, leftPaddleHitClip, rightPaddleHitClip;

/*    void OnCollisionStay2D(Collision2D collision) {
        Debug.Log("Stay Collision with " + collision.gameObject.tag + ", " + collision.gameObject.name);

        if (collision.gameObject.tag == "Paddle") {
            glitch.enabled = true;
            Debug.Log("Starting glitch");
            Debug.Log(glitchSeconds);
            StartCoroutine(ScreenGlitch(glitchSeconds));
        }

        if (collision.gameObject.tag == "Border") {
            Debug.Log("stay Border - length: " + collision.contacts.Length);

            //Vector2 pointOfContactNormal = collision.contacts[0].normal;

            for (int i = 0; i < collision.contacts.Length; i++) {
                Debug.Log("stay collision " + collision.contacts[i].normal);
                Vector2 pointOfContactNormal = collision.contacts[i].normal;

                if (pointOfContactNormal == Vector2.right) {
                    ScoreManager.instance.IncrementPlayer2Score();
                }
                else if (pointOfContactNormal == Vector2.left) {
                    ScoreManager.instance.IncrementPlayer1Score();
                }
            }
        }
    }*/

    void OnCollisionEnter2D(Collision2D collision) {
        //Produce a ripple

        Debug.Log("Collision with " + collision.gameObject.tag + ", " + collision.gameObject.name);

        if(collision.gameObject.tag == "Paddle") {
            //rippleEffect.MakeRipple();

            if(collision.gameObject.name == "Left Paddle") {
                leftPaddleHitSource.PlayOneShot(leftPaddleHitClip);
            } else {
                rightPaddleHitSource.PlayOneShot(rightPaddleHitClip);
            }

            glitch.enabled = true;
            Debug.Log("Starting glitch");
            Debug.Log(glitchSeconds);



            StartCoroutine(ScreenGlitch(glitchSeconds));
        }
        Debug.Log("contacts length: " + collision.contacts.Length);

        if (collision.gameObject.tag == "Border") {
            rippleEffect.MakeRipple();

            //Vector2 pointOfContactNormal = collision.contacts[0].normal;

            for (int i = 0; i < collision.contacts.Length; i++) {
                Debug.Log("collision " + collision.contacts[i].normal);
                Vector2 pointOfContactNormal = collision.contacts[i].normal;

                if (pointOfContactNormal == Vector2.right) {
                    StateManager.instance.Player2Scored();
                } else if (pointOfContactNormal == Vector2.left) {
                    StateManager.instance.Player1Scored();
                } else if(pointOfContactNormal == Vector2.up) {
                    wallSoundSource.PlayOneShot(wallSoundClip);
                } else if(pointOfContactNormal == Vector2.down) {
                    wallSoundSource.PlayOneShot(wallSoundClip);
                }
            }
        }
    }

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void ResetBall() {
        transform.position = new Vector3(0f, -0.19f, 1.11f);

        float randStartY = Random.Range(-45, 45);
        //rigidBody.AddForce(new Vector2(-750f, randStartY));
        if (randStartY <= 0.5f && randStartY >= -0.5f) {
            randStartY = (randStartY / randStartY) * 0.6f;
        }
        rigidBody.AddForce(new Vector2(-750f, randStartY));
    }

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator ScreenGlitch(float time) {
        Debug.Log(time);
        yield return new WaitForSeconds(time);
        Debug.Log("Ending glitch");
        glitch.enabled = false;
    }

}
