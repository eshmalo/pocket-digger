using UnityEngine;

public class SliceController : MonoBehaviour
{
    [Header("Slice Settings")]
    public float sliceRadius = 0.5f;
    public float sliceStrength = 1f;
    
    private Camera mainCamera;
    private LayerStack layerStack;
    private FeedbackFX feedbackFX;
    private bool isSlicing = false;
    private Vector3 lastSlicePos;
    private float sliceAccumulator = 0f;
    
    void Start()
    {
        mainCamera = Camera.main;
        layerStack = FindObjectOfType<LayerStack>();
        feedbackFX = FindObjectOfType<FeedbackFX>();
    }
    
    void Update()
    {
        HandleInput();
    }
    
    void HandleInput()
    {
        if (layerStack == null || layerStack.AllLayersCleared) return;
        
        // Mouse/Touch input
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndSlicing();
        }
    }
    
    void StartSlicing()
    {
        isSlicing = true;
        lastSlicePos = GetWorldMousePosition();
        sliceAccumulator = 0f;
    }
    
    void ContinueSlicing()
    {
        if (!isSlicing) return;
        
        Vector3 currentPos = GetWorldMousePosition();
        float distance = Vector3.Distance(currentPos, lastSlicePos);
        
        // Only slice if moved enough (prevents performance issues)
        if (distance > 0.05f)
        {
            PerformSlice(currentPos);
            lastSlicePos = currentPos;
            
            // Accumulate for feedback
            sliceAccumulator += distance;
            
            // Trigger feedback every certain distance
            if (sliceAccumulator > 0.2f)
            {
                TriggerSliceFeedback(currentPos);
                sliceAccumulator = 0f;
            }
        }
    }
    
    void EndSlicing()
    {
        isSlicing = false;
    }
    
    void PerformSlice(Vector3 position)
    {
        var currentLayer = layerStack.CurrentLayer;
        if (currentLayer != null)
        {
            // Perform the dig/slice
            currentLayer.DigAt(position, sliceRadius * sliceStrength);
            
            // Add particle trail
            if (feedbackFX != null)
            {
                feedbackFX.SpawnSliceParticles(position, currentLayer.fillColor);
            }
        }
    }
    
    void TriggerSliceFeedback(Vector3 position)
    {
        if (feedbackFX != null)
        {
            // Haptic feedback
            feedbackFX.PlayHaptic();
            
            // Audio feedback
            feedbackFX.PlaySliceSound(Random.Range(0.9f, 1.1f)); // Slight pitch variation
            
            // Visual feedback
            feedbackFX.SpawnSliceBurst(position);
        }
    }
    
    Vector3 GetWorldMousePosition()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    
    // Public methods for external control
    public void SetSliceRadius(float radius)
    {
        sliceRadius = Mathf.Clamp(radius, 0.1f, 2f);
    }
    
    public void SetSliceStrength(float strength)
    {
        sliceStrength = Mathf.Clamp(strength, 0.5f, 2f);
    }
}