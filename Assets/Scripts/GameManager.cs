using UnityEngine;
using UnityEngine.UI;  // Use standard UI for now

public class GameManager : MonoBehaviour
{
    [SerializeField] private int marblesToWin = 20;
    [SerializeField] private Text scoreText;  // Regular UI Text
    private int _score;

    public void AddScore()   // call from Marble.cs
    {
        _score++;
        if (scoreText != null)
            scoreText.text = _score + " / " + marblesToWin;
        if (_score >= marblesToWin) LevelComplete();
    }

    private void LevelComplete()
    {
        Time.timeScale = 0;
        Debug.Log("Level Complete! Score: " + _score);
        // TODO: show level‑complete UI, offer rewarded‑ad 2× coins
    }
}