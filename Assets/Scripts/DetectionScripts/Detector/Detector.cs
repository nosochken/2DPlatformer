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
        Collider2D nearestEnemyCollider = null;

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        if (enemiesInRange.Length > 0)
        {
            foreach (Collider2D enemy in enemiesInRange)
            {
                float distanceToEnemySqr = (transform.position - enemy.transform.position).sqrMagnitude;

                if (distanceToEnemySqr < shortestDistanceSqr)
                {
                    shortestDistanceSqr = distanceToEnemySqr;
                    nearestEnemyCollider = enemy;
                }
            }
        }

        return nearestEnemyCollider ? nearestEnemyCollider.GetComponent<T>() : null;
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