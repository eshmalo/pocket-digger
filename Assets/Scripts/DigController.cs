using UnityEngine;
using Destructible2D;

public class DigController : MonoBehaviour
{
    private float digRadius = 0.5f;
    private Camera mainCamera;
    public D2dDestroyer digTool;  // Will be set by GameBootstrap

    void Start()
    {
        mainCamera = Camera.main;
        
        // If digTool wasn't set, create a default one
        if (digTool == null)
        {
            var toolGO = new GameObject("DigTool");
            digTool = toolGO.AddComponent<D2dDestroyer>();
            digTool.StampTex = CreateCircleTexture(64);
            digTool.StampPaint = D2dDestructible.PaintType.Subtractive;
            digTool.StampSize = new Vector2(digRadius * 2, digRadius * 2);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && digTool != null)
        {
            Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            
            // Move the dig tool to mouse position and stamp
            digTool.transform.position = mousePos;
            digTool.StampEnabled = true;
            digTool.UpdateStamp();
        }
    }
    
    private Texture2D CreateCircleTexture(int size)
    {
        var tex = new Texture2D(size, size);
        var center = size / 2f;
        
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var dist = Vector2.Distance(new Vector2(x, y), new Vector2(center, center));
                var alpha = dist <= center ? 1f : 0f;
                tex.SetPixel(x, y, new Color(1, 1, 1, alpha));
            }
        }
        
        tex.Apply();
        return tex;
    }
}