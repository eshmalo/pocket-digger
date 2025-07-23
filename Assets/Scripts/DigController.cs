using UnityEngine;

public class DigController : MonoBehaviour
{
    [SerializeField] private float digRadius = 0.5f;
    [SerializeField] private LayerMask terrainLayer;
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            
            // Simple terrain destruction using colliders
            Collider2D[] hits = Physics2D.OverlapCircleAll(pos, digRadius, terrainLayer);
            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("Terrain"))
                {
                    Destroy(hit.gameObject);
                }
            }
        }
    }
}