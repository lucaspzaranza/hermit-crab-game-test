using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;

    [Header("Menus Containers")]
    [SerializeField] private GameObject _gameOverUI;

    public void UpdateHealthBar(int playerHealth)
    {
        _healthBar.SetHealthBarFillAmount(playerHealth / 100f);
    }

    public void UpdateScoreText(int newScore)
    {
        _scoreText.text = $"x{newScore}";
    }

    public void SetGameOverActivation(bool val)
    {
        _gameOverUI.SetActive(val);
        _healthBar.gameObject.SetActive(!val);
        _scoreText.gameObject.SetActive(!val);
        _gameOverScoreText.text = $"Score: {_scoreText.text}";
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
