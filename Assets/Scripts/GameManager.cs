using UnityEngine;
using TMPro;          // Add TextMeshPro package

public class GameManager : MonoBehaviour
{
    [SerializeField] private int marblesToWin = 20;
    [SerializeField] private TMP_Text scoreText;
    private int _score;

    public void AddScore()   // call from Marble.cs
    {
        _score++;
        scoreText.text = _score + " / " + marblesToWin;
        if (_score >= marblesToWin) LevelComplete();
    }

    private void LevelComplete()
    {
        Time.timeScale = 0;
        // TODO: show level‑complete UI, offer rewarded‑ad 2× coins
    }
}