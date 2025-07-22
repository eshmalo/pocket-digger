using UnityEngine;
using Destructible2D;

public class DigController : MonoBehaviour
{
    [SerializeField] private D2dDestroyer digTool;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            digTool.ManualHit(pos);      // erodes alpha mask at cursor
        }
    }
}