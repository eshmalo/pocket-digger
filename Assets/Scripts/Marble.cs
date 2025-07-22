using UnityEngine;

public class Marble : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            // simple score increment now; GameManager handles UI later
            Destroy(gameObject);
        }
    }
}