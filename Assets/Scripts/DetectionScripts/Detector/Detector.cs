using System;
using UnityEngine;

public class Detector<T> : MonoBehaviour where T : MonoBehaviour, IDetectable
{
    public event Action Detected;
    public event Action<T> DetectedWitDetected;
    public event Action<Collision2D> DetectedWithCollisionInformation;

    public event Action Missed;

    public T FindNearestDetectable(LayerMask layerMask, float radius)
    {
        float shortestDistanceSqr = Mathf.Infinity;
        Collider2D nearestDetectableCollider = null;

        Collider2D[] detectablesInRange = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        if (detectablesInRange.Length > 0)
        {
            foreach (Collider2D detectable in detectablesInRange)
            {
                float distanceToDetectablesSqr = (transform.position - detectable.transform.position).sqrMagnitude;

                if (distanceToDetectablesSqr < shortestDistanceSqr)
                {
                    shortestDistanceSqr = distanceToDetectablesSqr;
                    nearestDetectableCollider = detectable;
                }
            }
        }

        return nearestDetectableCollider ? nearestDetectableCollider.GetComponent<T>() : null;
    }

    public T DetectViaRaycast(int layerMask, Vector2 origin, Vector2 direction, float maxDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, layerMask);

        if (hit.collider != null && hit.collider.TryGetComponent(out T detection))
            return detection;

        return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T detection))
        {
            Detected?.Invoke();
            DetectedWitDetected?.Invoke(detection);
            DetectedWithCollisionInformation?.Invoke(collision);

            detection.WasDetectedBy(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T detection))
            Missed?.Invoke();
    }
}