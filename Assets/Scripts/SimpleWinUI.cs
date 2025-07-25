using UnityEngine;
using TMPro;
using System.Collections;

public class SimpleWinUI : MonoBehaviour
{
    private Canvas winCanvas;
    private TextMeshProUGUI winText;
    private CanvasGroup canvasGroup;

    void Start()
    {
        CreateWinUI();
        HideWinUI();
    }

    void CreateWinUI()
    {
        // Create Canvas
        GameObject canvasGO = new GameObject("WinCanvas");
        winCanvas = canvasGO.AddComponent<Canvas>();
        winCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        
        // Add CanvasGroup for fade control
        canvasGroup = canvasGO.AddComponent<CanvasGroup>();
        
        // Create Text
        GameObject textGO = new GameObject("WinText");
        textGO.transform.SetParent(canvasGO.transform, false);
        
        winText = textGO.AddComponent<TextMeshProUGUI>();
        winText.text = "Level Complete!";
        winText.fontSize = 72;
        winText.color = Color.white;
        winText.alignment = TextAlignmentOptions.Center;
        winText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        
        // Position in center
        RectTransform rectTransform = textGO.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(800, 200);
    }

    public void ShowWinUI()
    {
        canvasGroup.alpha = 1;
    }

    public void HideWinUI()
    {
        canvasGroup.alpha = 0;
    }
}