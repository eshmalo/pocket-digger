using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    private RectTransform progressBar;
    private Image progressFill;
    private TextMeshProUGUI progressText;
    private TextMeshProUGUI layerText;
    private float targetProgress = 1f;
    private float currentProgress = 1f;
    
    void Awake()
    {
        CreateProgressUI();
    }
    
    void CreateProgressUI()
    {
        // Create UI Canvas if not exists
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("UICanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }
        
        // Progress bar container
        GameObject progressContainer = new GameObject("ProgressBar");
        progressContainer.transform.SetParent(canvas.transform, false);
        progressBar = progressContainer.AddComponent<RectTransform>();
        
        // Position at top center
        progressBar.anchorMin = new Vector2(0.5f, 1f);
        progressBar.anchorMax = new Vector2(0.5f, 1f);
        progressBar.anchoredPosition = new Vector2(0, -50);
        progressBar.sizeDelta = new Vector2(300, 30);
        
        // Background
        Image background = progressContainer.AddComponent<Image>();
        background.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);
        
        // Fill bar
        GameObject fillGO = new GameObject("Fill");
        fillGO.transform.SetParent(progressContainer.transform, false);
        RectTransform fillRect = fillGO.AddComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = new Vector2(1f, 1f);
        fillRect.sizeDelta = Vector2.zero;
        fillRect.anchoredPosition = Vector2.zero;
        
        progressFill = fillGO.AddComponent<Image>();
        progressFill.color = Color.white;
        
        // Progress text
        GameObject textGO = new GameObject("ProgressText");
        textGO.transform.SetParent(progressContainer.transform, false);
        progressText = textGO.AddComponent<TextMeshProUGUI>();
        progressText.text = "100%";
        progressText.fontSize = 20;
        progressText.color = Color.white;
        progressText.alignment = TextAlignmentOptions.Center;
        progressText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        
        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        textRect.anchoredPosition = Vector2.zero;
        
        // Layer indicator text
        GameObject layerGO = new GameObject("LayerText");
        layerGO.transform.SetParent(canvas.transform, false);
        layerText = layerGO.AddComponent<TextMeshProUGUI>();
        layerText.text = "Layer 1/5";
        layerText.fontSize = 24;
        layerText.color = Color.white;
        layerText.alignment = TextAlignmentOptions.Center;
        layerText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        
        RectTransform layerRect = layerGO.GetComponent<RectTransform>();
        layerRect.anchorMin = new Vector2(0.5f, 1f);
        layerRect.anchorMax = new Vector2(0.5f, 1f);
        layerRect.anchoredPosition = new Vector2(0, -90);
        layerRect.sizeDelta = new Vector2(200, 40);
    }
    
    void Update()
    {
        // Smooth progress animation
        if (Mathf.Abs(currentProgress - targetProgress) > 0.01f)
        {
            currentProgress = Mathf.Lerp(currentProgress, targetProgress, Time.deltaTime * 5f);
            UpdateProgressVisual();
        }
    }
    
    public void SetProgress(float percentage, Color layerColor)
    {
        targetProgress = Mathf.Clamp01(percentage);
        
        // Update fill color to match layer
        if (progressFill != null)
        {
            progressFill.color = layerColor;
        }
        
        // Update text
        if (progressText != null)
        {
            int percent = Mathf.RoundToInt(percentage * 100f);
            progressText.text = $"{percent}%";
        }
    }
    
    public void SetLayerInfo(int current, int total)
    {
        if (layerText != null)
        {
            layerText.text = $"Layer {current}/{total}";
        }
    }
    
    void UpdateProgressVisual()
    {
        if (progressFill != null)
        {
            progressFill.rectTransform.anchorMax = new Vector2(currentProgress, 1f);
        }
    }
    
    public void ShowCompletion()
    {
        if (layerText != null)
        {
            layerText.text = "Complete!";
            layerText.fontSize = 36;
            
            // Pulse animation
            LeanTween.scale(layerText.gameObject, Vector3.one * 1.2f, 0.5f)
                .setEaseInOutSine()
                .setLoopPingPong();
        }
        
        // Hide progress bar
        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(false);
        }
    }
}