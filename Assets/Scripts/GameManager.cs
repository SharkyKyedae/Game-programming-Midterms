
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject prefab;
    public Button spawnButton;

    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text timerText;
    public TMP_Text gameOverText;

    int score = 0;
    int health = 100;
    float timeLeft = 30f;
    bool gameEnded = false;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        UpdateUI();
        StartCoroutine(Countdown());
    }

    public void SpawnObject()
    {
        if (gameEnded) return;

        Vector3 position = new Vector3(
            Random.Range(-4f, 4f),
            Random.Range(0.5f, 3f),
            Random.Range(3f, 8f)
        );

        Instantiate(prefab, position, Quaternion.identity);

        score++;
        health = Mathf.Max(0, health - 10);

        UpdateUI();

        if (health <= 0)
            EndGame();
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0 && !gameEnded)
        {
            timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        if (!gameEnded)
            EndGame();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        timerText.text = "Time: " + Mathf.CeilToInt(timeLeft);
    }

    void EndGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        spawnButton.interactable = false;
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "GAME OVER\nFinal Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
