using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    public float explosionRadius = .5f;
    Vector3 OffsetToTarget;
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, explosionRadius);
        // Draws red line in front of the object
        Gizmos.color = Color.red;
        Vector3 target = transform.TransformDirection(Vector3.forward);
        // Another line with x and y offset
        Vector3 offset = transform.TransformDirection(Vector3.forward) + new Vector3(1, 1, 0);
        Gizmos.DrawRay(transform.position, target);
        Gizmos.DrawRay(transform.position, offset);
        OffsetToTarget = (target - offset);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(offset, OffsetToTarget.normalized);
    }
}
