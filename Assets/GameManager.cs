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

    public bool IsSinglePlayer() {
        return gameType == GameType.SinglePlayer;
    }

    public bool IsTwoPlayer() {
        return gameType == GameType.TwoPlayer;
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
        gameType = GameType.SinglePlayer;
        Debug.Log("Game Type: " + gameType.ToString());
        MakeSingleton();
    }

    void Update() {

    }

    void Start() {

    }

}
