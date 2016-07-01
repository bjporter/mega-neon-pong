using UnityEngine;
using System.Collections;

public class AIManager : MonoBehaviour {

    private float gameSecondsStart;
    private bool lerpStarted = false;
    private float randomLerpSpeed;
    private Vector3 startPosition;

    [SerializeField]
    private GameObject ball, leftPaddle, rightPaddle;

	void Start () {
        lerpStarted = true;
	}

    float CenterYOfPaddle(GameObject paddle) {
        return paddle.GetComponent<Renderer>().bounds.center.y;
    }
	
	void Update () {
	    if(GameManager.instance.IsSinglePlayer() && !StateManager.instance.IsGamePaused()) {
            rightPaddle.transform.position = Vector3.Lerp(startPosition, new Vector3(startPosition.x, ball.transform.position.y, startPosition.z), randomLerpSpeed);

            if (gameSecondsStart >= 1.5f) {
                //Vector3 t = rightPaddle.transform.position;
                //t.y = ball.transform.position.y;
                //rightPaddle.transform.position = t;

                //rightPaddle.transform.position = Vector3.Lerp(startPosition, ball.transform.position, 0.05f);
                gameSecondsStart = 0;
                lerpStarted = true;
            }
        }

        gameSecondsStart += Time.deltaTime;

        if(lerpStarted) {
            randomLerpSpeed = Random.Range(0.5f, 1f);
            startPosition = rightPaddle.transform.position;
            lerpStarted = false;
        }

	}
}
