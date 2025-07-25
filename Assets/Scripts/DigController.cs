using UnityEngine;

public class DigController : MonoBehaviour
{
    public float brushRadius = 0.35f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 wp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            wp.z = 0;
            
            var diggable = FindObjectOfType<DiggableTerrain>();
            if (diggable != null)
            {
                diggable.DigAt(wp, brushRadius);
            }
        }
    }
}