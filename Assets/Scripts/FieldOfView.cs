using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Transform[] points;
    void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();

        // foreach (Transform point in points)
        //     Gizmos.DrawLine(transform.position, point.position);

        Vector2 camDir = cam.transform.forward;
        Vector2 camPos = cam.transform.position;

        float lowestDot = float.MaxValue;
        Vector2 outermost = default;
        foreach (Transform ptTf in points)
        {
            Vector2 pt = (Vector2)ptTf.position - camPos;
            Vector2 dirToPt = pt.normalized;
            float dot = Vector2.Dot( camDir, dirToPt);
            if (dot < lowestDot)
            {
                lowestDot = dot;
                outermost = ptTf.position;
            }
        }
        Gizmos.DrawLine(transform.position, outermost);
    }
}
