using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float gameTime = 0f;
    private bool isGameComplete = false;
    private LayerStack layerStack;
    private UIProgress progressUI;
    
    void Start()
    {
        layerStack = FindObjectOfType<LayerStack>();
        progressUI = FindObjectOfType<UIProgress>();
        
        if (progressUI != null && layerStack != null)
        {
            progressUI.SetLayerInfo(1, layerStack.GetTotalLayers());
        }
    }
    
    void Update()
    {
        if (!isGameComplete)
        {
            gameTime += Time.deltaTime;
            
            // Update layer info
            if (progressUI != null && layerStack != null)
            {
                int currentLayer = layerStack.GetCurrentLayerIndex() + 1;
                progressUI.SetLayerInfo(currentLayer, layerStack.GetTotalLayers());
            }
        }
    }
    
    public void OnGemRevealed()
    {
        if (isGameComplete) return;
        
        isGameComplete = true;
        LevelComplete();
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
    
    public string GetCompletionGrade()
    {
        // Grade based on completion time
        if (gameTime < 30f) return "S";
        if (gameTime < 45f) return "A";
        if (gameTime < 60f) return "B";
        return "C";
    }
}