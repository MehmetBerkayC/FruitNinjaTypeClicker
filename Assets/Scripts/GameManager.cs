using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI volumeComponents;

    private AudioSource music;

    private int score;
    private float spawnRate = 2;
    private int lives = 3;
    private bool paused;

    public bool isGameOver;
    

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }


    public void UpdateLives(int liveToAdd)
    {
        if(lives > 0)
        {
            lives += liveToAdd;
            livesText.text = "Lives: " + lives;
        }
        else
        {
            livesText.text = "Lives: " + lives;
            GameOver();
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        // take the currentle active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        // Hide Title Screen
        titleScreen.SetActive(false);

        isGameOver = false;

        // Reset score
        score = 0;

        // Difficulty by spawnrate
        spawnRate /= difficulty;

        // HUD Changes 
        scoreText.gameObject.SetActive(true);
        livesText.gameObject.SetActive(true);
        volumeComponents.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());

        UpdateLives(0);
        UpdateScore(0);
    }

    private void ChangePaused()
    {
        if (!paused)
        {
            // Pause and the game functions
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            music.Pause();
        }
        else 
        { 
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            music.UnPause();
        }
    }
}
