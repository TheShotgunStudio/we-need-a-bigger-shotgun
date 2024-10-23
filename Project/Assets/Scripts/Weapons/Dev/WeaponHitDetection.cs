using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SpreadType
{
    FIXED,
    RANDOM
}
public class WeaponHitDetection : MonoBehaviour
{
    [Tooltip("Type of bullet spread: \nFixed spread creates a circle of ")]
    public SpreadType SpreadType;

    [Tooltip("Spread angle (In Percent) \nDetermines how much the bullet spread is.")]
    [Range(0f, 50f)]
    public float SpreadAngle = 10f;

    [Tooltip("The Amount of pellets the shotgun will shoot. \nKeep in mind that there will always be one pellet that is perfectly accurate.")]
    public float PelletCount;

    public Camera MainCamera;
    public RectTransform Crosshair;
    public Shotgun Weapon;
    public Transform BulletOriginPoint;

    public GameObject DebugPelletHitIndicator;

    /// <summary>
    /// Casts a ray from the camera to the crosshair to determine what the crosshair is pointing at
    /// </summary>
    /// <returns>The </returns>
    public Vector3 CrosshairToGunTarget()
    {
        Ray ray = MainCamera.ScreenPointToRay(Crosshair.position);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
            
        }
        return ray.direction;
    }

    
    public List<Vector3> DetermineFixedSpreadDirection(Vector3 bulletOriginPoint, Vector3 gunTarget, int pelletCount, float spreadAngle = 10)
    {
        float normalizedSpreadAngle = spreadAngle / 100;
        List<Vector3> spreadPattern = new List<Vector3>();
        Vector3 direction = (gunTarget - bulletOriginPoint).normalized;
        spreadPattern.Add(direction);

        float distance = Vector3.Distance(bulletOriginPoint, gunTarget);

        float radius = distance * normalizedSpreadAngle;

        for (int i = 0; i < pelletCount-1; i++)
        {
            float angle = i * (360f / pelletCount);

            float radians = angle * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * radius;

            Vector3 newPosition = gunTarget + offset;

            spreadPattern.Add(newPosition);
        }

        return spreadPattern;
    }

    public List<Vector3> DetermineRandomSpreadPattern(Vector3 bulletOriginPoint, Vector3 gunTarget, int pelletCount, float spreadAngle = 10)
    {
        float normalizedSpreadAngle = spreadAngle / 100;
        List<Vector3> spreadPattern = new List<Vector3>();
        Vector3 direction = (gunTarget - bulletOriginPoint).normalized;
        spreadPattern.Add(direction);

        for (int i = 0; i < pelletCount - 1; i++)
        {
            Vector3 SpreadAppliedDirection = direction += new Vector3(
                Random.Range(-normalizedSpreadAngle, normalizedSpreadAngle),
                Random.Range(-normalizedSpreadAngle, normalizedSpreadAngle),
                Random.Range(-normalizedSpreadAngle, normalizedSpreadAngle)
                );

            spreadPattern.Add(SpreadAppliedDirection.normalized);
        }
        return spreadPattern;
    }

    public List<RaycastHit> DeterminePelletHits(List<Vector3> spreadPattern, Transform bulletOriginPoint)
    {
        List<RaycastHit> hits = new List<RaycastHit>();
        foreach (Vector3 pellet in  spreadPattern)
        {
            Ray pelletRay = new Ray(bulletOriginPoint.position, pellet);
            Physics.Raycast(pelletRay, out RaycastHit connectedPellet);
            hits.Add(connectedPellet);
        }
        return hits;
    }

    public void DoOnBulletHitAction(RaycastHit target)
    {
        Instantiate(DebugPelletHitIndicator, target.point, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0)&& Weapon.ReloadTimer.IsFinished())
        {
            Vector3 gunTarget = CrosshairToGunTarget();
            List<Vector3> spreadPattern;
            if (SpreadType == SpreadType.RANDOM)
            {
                spreadPattern = DetermineRandomSpreadPattern(BulletOriginPoint.position, gunTarget, 9);
            }
            else if (SpreadType == SpreadType.FIXED)
            {
                spreadPattern = DetermineFixedSpreadDirection(BulletOriginPoint.position, gunTarget, 9);
            }
            else
            {
                Debug.LogError("Unknown Spread Type. Falling back to random");
                spreadPattern = DetermineRandomSpreadPattern(BulletOriginPoint.position, gunTarget, 9);
            }
            var hits = DeterminePelletHits(spreadPattern, BulletOriginPoint);

            foreach (var target in hits)
            {
                DoOnBulletHitAction(target);
            }

        }
    }
}
