using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text createdText;
    private int score;
    private bool gameOver;
    private bool restart;
    private bool win;

	void Start () {
        gameOver = false;
        restart = false;
        win = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        createdText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
	}

    void Update() {
        if (restart) {
            if (Input.GetKeyDown(KeyCode.T)) {
                SceneManager.LoadScene("Main");
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while (true) {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver) {
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }

            if (win) {
                createdText.text = "Created By: Christopher Couto";
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
        if (score >= 100) {
            winText.text = "You Win!";
            win = true;
        }
    }

    void UpdateScore() {
        ScoreText.text = "Points: " + score;
    }

    public void GameOver() {
        if (win == false)
        {
            gameOverText.text = "Game Over";
            gameOver = true;
        }
    }
}
