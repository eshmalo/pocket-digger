using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int marblesToWin = 20;
    public TMP_Text scoreText;  // Made public for GameBootstrap
    private int _score;

    public void AddScore()
    {
        _score++;
        scoreText.text = $"{_score}/{marblesToWin}";
        if (_score >= marblesToWin) Invoke(nameof(LevelComplete), 0.3f);
    }

    private void LevelComplete()
    {
        // Show win UI
        SimpleWinUI winUI = FindObjectOfType<SimpleWinUI>();
        if (winUI == null)
        {
            GameObject winUIGO = new GameObject("WinUIManager");
            winUI = winUIGO.AddComponent<SimpleWinUI>();
        }
        winUI.ShowWinUI();
        
        // Restart after 2 seconds
        Invoke(nameof(RestartLevel), 2f);
    }
    
    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void Start() => scoreText.text = $"0/{marblesToWin}";

    public void SetMarblesToWin(int count)
    {
        marblesToWin = count;
        scoreText.text = $"0/{marblesToWin}";
    }
}