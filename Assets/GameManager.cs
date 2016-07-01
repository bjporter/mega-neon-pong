using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public GameType gameType;

    public int test;

    public enum GameType {
        SinglePlayer = 1,
        TwoPlayer = 2,
        None = 3
    }

    public void SetSinglePlayer() {
        gameType = GameType.SinglePlayer;
    }

    public void SetTwoPlayer() {
        gameType = GameType.TwoPlayer;
    }

    void MakeSingleton() {
        if (instance != null) {
            Destroy(gameObject);
        }else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Awake() {
        MakeSingleton();
    }

    void Update() {

    }

    void Start() {

    }

}
