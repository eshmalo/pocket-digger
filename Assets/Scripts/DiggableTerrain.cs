using UnityEngine;

/// <summary>Run-time sprite that can be "dug" by erasing pixels.</summary>
[RequireComponent(typeof(SpriteRenderer), typeof(PolygonCollider2D))]
public class DiggableTerrain : MonoBehaviour
{
    [Range(32, 1024)] public int textureSize = 512;
    public Color fillColor = new Color32(0x8B, 0x5A, 0x2B, 0xFF);

    private Texture2D _tex;
    private SpriteRenderer _sr;
    private PolygonCollider2D _poly;
    private bool _needsColliderUpdate = false;
    private float _colliderUpdateTimer = 0f;

    private void Awake()
    {
        _tex = new Texture2D(textureSize, textureSize, TextureFormat.RGBA32, false);
        var pixels = new Color32[textureSize * textureSize];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = fillColor;
        _tex.SetPixels32(pixels);
        _tex.Apply();

        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = Sprite.Create(
            _tex,
            new Rect(0, 0, textureSize, textureSize),
            new Vector2(0.5f, 0.5f),
            100);

        _poly = GetComponent<PolygonCollider2D>();
        UpdateCollider();
    }

    private void Update()
    {
        // Batch collider updates to improve performance
        if (_needsColliderUpdate)
        {
            _colliderUpdateTimer += Time.deltaTime;
            if (_colliderUpdateTimer > 0.1f) // Update every 100ms
            {
                UpdateCollider();
                _needsColliderUpdate = false;
                _colliderUpdateTimer = 0f;
            }
        }
    }

    public void DigAt(Vector2 worldPos, float radius)
    {
        // Convert world → texture coords
        Vector2 local = transform.InverseTransformPoint(worldPos);
        local *= 100; // pixels per unit
        local += new Vector2(textureSize / 2f, textureSize / 2f);

        int r = Mathf.CeilToInt(radius * 100);
        bool pixelsChanged = false;
        
        for (int y = -r; y <= r; y++)
        for (int x = -r; x <= r; x++)
        {
            if (x * x + y * y > r * r) continue;
            int px = (int)local.x + x;
            int py = (int)local.y + y;
            if (px < 0 || px >= textureSize || py < 0 || py >= textureSize) continue;
            _tex.SetPixel(px, py, Color.clear);
            pixelsChanged = true;
        }
        
        if (pixelsChanged)
        {
            _tex.Apply();
            _needsColliderUpdate = true;
        }
    }

    private void UpdateCollider()
    {
        // For performance, we'll use a simple box collider approach
        // A full polygon collider from texture would be too expensive
        Destroy(_poly);
        _poly = gameObject.AddComponent<PolygonCollider2D>();
        _poly.isTrigger = false;
    }
}