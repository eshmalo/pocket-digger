using UnityEngine;
using System.Collections;

public class GemCore : MonoBehaviour
{
    [Header("Gem Settings")]
    public float shimmerSpeed = 2f;
    public float revealDuration = 1.5f;
    public Color gemColor = new Color(0.5f, 0.8f, 1f, 1f); // Light blue crystal
    
    private SpriteRenderer spriteRenderer;
    private bool isRevealed = false;
    private float shimmerTime = 0f;
    
    void Awake()
    {
        CreateGemVisuals();
        gameObject.SetActive(false); // Hidden until all layers cleared
    }
    
    void CreateGemVisuals()
    {
        // Create gem sprite procedurally
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = CreateGemSprite();
        spriteRenderer.sortingOrder = -1; // Behind layers initially
        
        // Add collider for future interactions
        var collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = 0.5f;
        collider.isTrigger = true;
        
        // Scale
        transform.localScale = Vector3.one * 0.8f;
    }
    
    Sprite CreateGemSprite()
    {
        int size = 128;
        Texture2D texture = new Texture2D(size, size);
        
        Vector2 center = new Vector2(size / 2f, size / 2f);
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Vector2 pos = new Vector2(x, y);
                float dist = Vector2.Distance(pos, center) / (size / 2f);
                
                // Create faceted gem shape
                float angle = Mathf.Atan2(y - center.y, x - center.x);
                float facets = 6f; // Hexagonal gem
                float facetAngle = Mathf.Round(angle * facets / (2f * Mathf.PI)) * (2f * Mathf.PI) / facets;
                
                Vector2 facetDir = new Vector2(Mathf.Cos(facetAngle), Mathf.Sin(facetAngle));
                float facetDist = Vector2.Dot((pos - center).normalized, facetDir);
                
                // Gem shape with inner glow
                if (dist < 0.8f && facetDist > 0.3f)
                {
                    float brightness = 1f - (dist * 0.5f);
                    Color pixelColor = gemColor * brightness;
                    pixelColor.a = 1f;
                    texture.SetPixel(x, y, pixelColor);
                }
                else
                {
                    texture.SetPixel(x, y, Color.clear);
                }
            }
        }
        
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100);
    }
    
    void Update()
    {
        if (isRevealed)
        {
            // Shimmer effect
            shimmerTime += Time.deltaTime * shimmerSpeed;
            float shimmer = Mathf.Sin(shimmerTime) * 0.3f + 0.7f;
            
            if (spriteRenderer != null)
            {
                Color color = gemColor;
                color *= shimmer;
                color.a = 1f;
                spriteRenderer.color = color;
            }
            
            // Gentle rotation
            transform.Rotate(0, 0, 30f * Time.deltaTime);
        }
    }
    
    public void Reveal()
    {
        if (isRevealed) return;
        
        gameObject.SetActive(true);
        isRevealed = true;
        StartCoroutine(RevealAnimation());
    }
    
    IEnumerator RevealAnimation()
    {
        // Start small
        transform.localScale = Vector3.zero;
        spriteRenderer.sortingOrder = 10; // On top of everything
        
        // Scale up with bounce
        float elapsed = 0f;
        while (elapsed < revealDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / revealDuration;
            
            // Elastic easing for satisfying pop
            float scale = Mathf.Sin(-13f * (t + 1f) * Mathf.PI * 0.5f) * Mathf.Pow(2f, -10f * t) + 1f;
            transform.localScale = Vector3.one * scale * 1.2f;
            
            yield return null;
        }
        
        transform.localScale = Vector3.one * 1.2f;
        
        // Trigger completion
        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.OnGemRevealed();
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Could add collection logic or bonus effects
    }
}