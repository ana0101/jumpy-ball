using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private GameObject gameOverCanvas;
    [SerializeField]
    private GameObject newHighScoreCanvas;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text highScoreText;
    [SerializeField]
    private AudioSource gameStartSFX;
    [SerializeField]
    private AudioSource gameOverSFX;
    [SerializeField]
    private AudioSource newHighScoreSFX;

    private float score = 0;
    private float highScore;
    private bool ballIsAlive = true;

    private float timer = 0;
    private float interval = 5;
    private float obstacleSpeed = 4;
    private float lowSpawnRate = 1;
    private float highSpawnRate = 3;


    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("high_score"))
        {
            highScore = PlayerPrefs.GetFloat("high_score");
            highScoreText.text = "High Score: " + highScore.ToString();
        }
        else
        {
            highScore = 0;
        }

        AudioScript.Instance.PlaySFX(gameStartSFX);
    }

    // Update is called once per frame
    private void Update()
    {
        AddScore(ballIsAlive);

        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer -= interval;
            obstacleSpeed += 0.5f;

            if (lowSpawnRate - 0.2f > 0.1f)
            {
                lowSpawnRate -= 0.2f;
            }

            if (highSpawnRate - 0.2f > 0.2f)
            {
                highSpawnRate -= 0.2f;
            }
        }
    }

    private void AddScore(bool ballIsAlive)
    {
        if (ballIsAlive)
        {
            score += Time.deltaTime;
            score = Mathf.Round(score * 1000f) / 1000f;
            scoreText.text = score.ToString();
        }
    }

    public float GetObstacleSpeed()
    {
        return obstacleSpeed;
    }

    public float GetLowSpawnRate()
    {
        return lowSpawnRate;
    }

    public float GetHighSpawnRate()
    {
        return highSpawnRate;
    }

    public void GameOver()
    {
        ballIsAlive = false;

        if (score > highScore)
        {
            NewHighScore();
        }
        else
        {
            AudioScript.Instance.PlaySFX(gameOverSFX);
        }

        gameOverCanvas.SetActive(true);
        ObstacleSpawnerScript obstacleSpawnerScript = GameObject.FindGameObjectWithTag("spawner").GetComponent<ObstacleSpawnerScript>();
        obstacleSpawnerScript.SetBallIsAlive(false);
    }

    private void NewHighScore()
    {
        highScore = score;
        highScoreText.text = "High Score: " + highScore.ToString();
        PlayerPrefs.SetFloat("high_score", highScore);
        newHighScoreCanvas.SetActive(true);
        AudioScript.Instance.PlaySFX(newHighScoreSFX);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
