using UnityEngine;
using System.Collections.Generic;

public class LayerStack : MonoBehaviour
{
    [Header("Layer Configuration")]
    public int layerCount = 5;
    public float layerOffset = 0.05f;
    public float layerSize = 8f;
    
    // Pastel rainbow colors for ASMR aesthetic
    private readonly Color[] layerColors = new Color[]
    {
        new Color(1f, 0.7f, 0.7f, 1f),      // Pastel Pink
        new Color(1f, 0.85f, 0.7f, 1f),     // Pastel Peach
        new Color(1f, 1f, 0.7f, 1f),        // Pastel Yellow
        new Color(0.7f, 1f, 0.7f, 1f),      // Pastel Green
        new Color(0.7f, 0.85f, 1f, 1f)      // Pastel Blue
    };
    
    private List<DiggableTerrain> layers = new List<DiggableTerrain>();
    private int currentLayerIndex = 0;
    private UIProgress progressUI;
    private FeedbackFX feedbackFX;
    
    public DiggableTerrain CurrentLayer => currentLayerIndex < layers.Count ? layers[currentLayerIndex] : null;
    public bool AllLayersCleared => currentLayerIndex >= layers.Count;
    public float CurrentLayerProgress => CurrentLayer != null ? CurrentLayer.GetRemainingPixelPercentage() : 0f;
    
    void Start()
    {
        progressUI = FindObjectOfType<UIProgress>();
        feedbackFX = FindObjectOfType<FeedbackFX>();
        CreateLayers();
        UpdateLayerVisuals();
    }
    
    void CreateLayers()
    {
        // Create layers from bottom to top
        for (int i = 0; i < layerCount; i++)
        {
            GameObject layerGO = new GameObject($"Layer_{i}");
            layerGO.transform.SetParent(transform);
            layerGO.transform.localPosition = new Vector3(0, i * layerOffset, 0);
            layerGO.transform.localScale = new Vector3(layerSize, layerSize, 1);
            
            // Add components
            var sr = layerGO.AddComponent<SpriteRenderer>();
            sr.sortingOrder = i;
            
            layerGO.AddComponent<PolygonCollider2D>();
            
            var terrain = layerGO.AddComponent<DiggableTerrain>();
            terrain.textureSize = 256; // Smaller for better performance with multiple layers
            terrain.fillColor = layerColors[i % layerColors.Length];
            
            layers.Add(terrain);
            
            // Hide all but top layer initially
            if (i < layerCount - 1)
            {
                layerGO.SetActive(false);
            }
        }
        
        // Reverse so we work from top to bottom
        layers.Reverse();
    }
    
    void Update()
    {
        if (CurrentLayer != null)
        {
            float remaining = CurrentLayer.GetRemainingPixelPercentage();
            
            // Update progress UI
            if (progressUI != null)
            {
                progressUI.SetProgress(remaining, layerColors[currentLayerIndex % layerColors.Length]);
            }
            
            // Check if layer is cleared (less than 5% pixels remaining)
            if (remaining < 0.05f)
            {
                OnLayerCleared();
            }
        }
    }
    
    void OnLayerCleared()
    {
        if (CurrentLayer == null) return;
        
        // Trigger celebration effects
        if (feedbackFX != null)
        {
            feedbackFX.PlayLayerClearEffect(CurrentLayer.transform.position, 
                layerColors[currentLayerIndex % layerColors.Length]);
        }
        
        // Deactivate current layer
        CurrentLayer.gameObject.SetActive(false);
        
        // Move to next layer
        currentLayerIndex++;
        
        // Activate next layer if exists
        if (currentLayerIndex < layers.Count)
        {
            layers[currentLayerIndex].gameObject.SetActive(true);
            UpdateLayerVisuals();
        }
        else
        {
            // All layers cleared - trigger gem reveal
            OnAllLayersCleared();
        }
    }
    
    void UpdateLayerVisuals()
    {
        // Could add subtle animations or effects when new layer is revealed
        if (CurrentLayer != null)
        {
            // Add a subtle scale animation
            CurrentLayer.transform.localScale = Vector3.one * layerSize * 0.95f;
            LeanTween.scale(CurrentLayer.gameObject, Vector3.one * layerSize, 0.3f)
                .setEaseOutBack();
        }
    }
    
    void OnAllLayersCleared()
    {
        // Find and activate gem
        GemCore gem = FindObjectOfType<GemCore>(true);
        if (gem != null)
        {
            gem.Reveal();
        }
        
        // Trigger victory effects
        if (feedbackFX != null)
        {
            feedbackFX.PlayVictoryEffect();
        }
    }
    
    public int GetCurrentLayerIndex() => currentLayerIndex;
    public int GetTotalLayers() => layerCount;
}