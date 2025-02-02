using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State = GameState.Intro;

    public float PlayStartTime;

    public int Lives = 3;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;

    public Player PlayerScript;

    public TMP_Text ScoreText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        IntroUI.SetActive(true);
    }

    int CalculateScore()
    {
        float playTime = Time.time - PlayStartTime;
        return Mathf.FloorToInt(playTime);
    }

    void SaveHighScore()
    {
        int score = CalculateScore();
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public float CalculateGameSpeed()
    {
        if (State != GameState.Playing) {
            return 3f;
        }
        float speed = 5f + (0.5f * Mathf.Floor(CalculateScore() / 10f));
        return Mathf.Min(speed, 35f);
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Playing)
        {
            ScoreText.text = "Score: " + CalculateScore();
        }
        else if (State == GameState.Dead)
        {
            ScoreText.text = "High Score: " + GetHighScore();
        }

        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }
        if (State == GameState.Playing && Lives <= 0)
        {
            PlayerScript.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            State = GameState.Dead;
            DeadUI.SetActive(true);
            SaveHighScore();
        }
        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
