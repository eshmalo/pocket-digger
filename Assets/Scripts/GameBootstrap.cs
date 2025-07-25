using UnityEngine;
using TMPro;
using Destructible2D;

public static class GameBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Launch()
    {
        BuildCamera();
        var scoreText = BuildUI();
        var gm = BuildGameManager(scoreText);
        var marblePrefab = BuildMarblePrefab();
        var terrain = BuildTerrain();
        var bucket = BuildBucket();
        BuildDigController(terrain);
        BuildMarbleSpawner(marblePrefab);
        
        // Add win UI manager
        BuildWinUI();
        
        Debug.Log("Pocket Digger bootstrapped entirely from code.");
    }

    /* ------------------------------------------------------------ */

    private static void BuildCamera()
    {
        var cam = new GameObject("Main Camera").AddComponent<Camera>();
        cam.orthographic = true;
        cam.orthographicSize = 5;
        cam.backgroundColor = new Color32(0xC8, 0xE7, 0xFF, 0xFF);  // sky blue
        cam.transform.position = new Vector3(0, 0, -10);
        cam.tag = "MainCamera";
        cam.gameObject.AddComponent<AudioListener>();
    }

    private static TMP_Text BuildUI()
    {
        var canvasGO = new GameObject("Canvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
        canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        
        var text = new GameObject("ScoreText").AddComponent<TextMeshProUGUI>();
        text.transform.SetParent(canvasGO.transform, false);
        text.text = "0/20";
        text.fontSize = 48;
        text.color = Color.white;
        text.alignment = TextAlignmentOptions.Top;
        text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/LiberationSans SDF");
        
        var rectTransform = text.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector2(0, -50);
        rectTransform.sizeDelta = new Vector2(200, 60);
        
        return text;
    }

    private static GameManager BuildGameManager(TMP_Text scoreText)
    {
        var gmObject = new GameObject("GameManager");
        var gm = gmObject.AddComponent<GameManager>();
        gm.scoreText = scoreText;
        gm.SetMarblesToWin(20);
        return gm;
    }
    
    private static void BuildWinUI()
    {
        var winUIGO = new GameObject("WinUIManager");
        winUIGO.AddComponent<SimpleWinUI>();
    }

    /* ---------- prefab factories -------------------------------- */

    private static GameObject BuildMarblePrefab()
    {
        var go = new GameObject("MarblePrefab");
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = RuntimeSpriteFactory.MakeCircleSprite(32, Color.white);
        sr.sortingOrder = 1;
        
        var rb = go.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        rb.mass = 0.5f;
        
        var col = go.AddComponent<CircleCollider2D>();
        col.radius = 0.16f;
        
        go.AddComponent<Marble>();
        go.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        
        Object.DontDestroyOnLoad(go);  // persist as a template
        go.SetActive(false);  // hide the template
        return go;
    }

    private static GameObject BuildTerrain()
    {
        var go = new GameObject("Terrain");
        go.tag = "Terrain";
        
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = RuntimeSpriteFactory.MakeSquareSprite(512, new Color32(0x8B, 0x5A, 0x2B, 0xFF)); // brown
        
        go.transform.localScale = new Vector3(10, 8, 1);
        
        // Add Destructible2D components
        var d2d = go.AddComponent<D2dDestructible>();
        go.AddComponent<D2dCollider>();
        
        return go;
    }

    private static GameObject BuildBucket()
    {
        var go = new GameObject("GoalBucket");
        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = RuntimeSpriteFactory.MakeSquareSprite(128, new Color32(0x64, 0x3E, 0x17, 0xFF));
        sr.sortingOrder = 2;
        
        go.transform.localScale = new Vector3(2f, 0.5f, 1);
        
        var bc = go.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        bc.size = Vector2.one;
        
        go.tag = "Goal";
        go.transform.position = new Vector3(0, -3.8f, 0);
        
        return go;
    }

    private static void BuildDigController(GameObject terrain)
    {
        var dc = new GameObject("DigController").AddComponent<DigController>();
        // DigController will handle its own setup
    }

    private static void BuildMarbleSpawner(GameObject marblePrefab)
    {
        var spawner = new GameObject("MarbleSpawner").AddComponent<MarbleSpawner>();
        spawner.marblePrefab = marblePrefab;
        spawner.spawnInterval = 0.4f;
        spawner.transform.position = new Vector3(0, 4.5f, 0);
    }
}

/* ---------------- helper class ---------------- */

internal static class RuntimeSpriteFactory
{
    public static Sprite MakeSquareSprite(int size, Color color)
    {
        var tex = new Texture2D(size, size);
        var pixels = tex.GetPixels32();
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
        tex.SetPixels32(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100);
    }

    public static Sprite MakeCircleSprite(int size, Color color)
    {
        var tex = new Texture2D(size, size);
        var center = size / 2f;
        for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
            {
                var dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                tex.SetPixel(x, y, dist <= center ? color : Color.clear);
            }
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100);
    }
}