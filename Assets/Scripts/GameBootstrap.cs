using UnityEngine;
using TMPro;

public static class GameBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Launch()
    {
        Debug.Log("Bootstrapping Layer Slice game...");
        
        BuildCamera();
        BuildManagers();
        BuildLayerStack();
        BuildGem();
        BuildSliceController();
        BuildUI();
        
        Debug.Log("Layer Slice bootstrapped entirely from code.");
    }

    private static void BuildCamera()
    {
        var cam = new GameObject("Main Camera").AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 6;
        cam.backgroundColor = new Color32(0x2C, 0x3E, 0x50, 0xFF); // Dark blue-gray
        cam.transform.position = new Vector3(0, 0, -10);
        cam.tag = "MainCamera";
        cam.gameObject.AddComponent<AudioListener>();
    }

    private static void BuildManagers()
    {
        // Game Manager
        var gmObject = new GameObject("GameManager");
        gmObject.AddComponent<GameManager>();
        
        // Win UI Manager
        var winUIGO = new GameObject("WinUIManager");
        winUIGO.AddComponent<SimpleWinUI>();
        
        // Feedback FX Manager
        var fxGO = new GameObject("FeedbackFX");
        fxGO.AddComponent<FeedbackFX>();
    }

    private static void BuildLayerStack()
    {
        var stackGO = new GameObject("LayerStack");
        stackGO.transform.position = Vector3.zero;
        var stack = stackGO.AddComponent<LayerStack>();
        stack.layerCount = 5;
        stack.layerOffset = 0.05f;
        stack.layerSize = 8f;
    }

    private static void BuildGem()
    {
        var gemGO = new GameObject("GemCore");
        gemGO.transform.position = new Vector3(0, -0.5f, 0); // Below the layers
        var gem = gemGO.AddComponent<GemCore>();
        gem.shimmerSpeed = 2f;
        gem.revealDuration = 1.5f;
        gem.gemColor = new Color(0.3f, 0.7f, 1f, 1f); // Light blue crystal
    }

    private static void BuildSliceController()
    {
        var controllerGO = new GameObject("SliceController");
        var controller = controllerGO.AddComponent<SliceController>();
        controller.sliceRadius = 0.4f;
        controller.sliceStrength = 1f;
    }

    private static void BuildUI()
    {
        // Progress UI
        var progressGO = new GameObject("UIProgress");
        progressGO.AddComponent<UIProgress>();
    }
}

// Remove LeanTween dependency for now
public static class LeanTween
{
    public static LTDescr scale(GameObject gameObject, Vector3 to, float time)
    {
        return new LTDescr();
    }
}

public class LTDescr
{
    public LTDescr setEaseOutBack() { return this; }
    public LTDescr setEaseInOutSine() { return this; }
    public LTDescr setLoopPingPong() { return this; }
}