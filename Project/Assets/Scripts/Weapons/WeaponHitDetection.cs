using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHitDetection : MonoBehaviour
{
    [Tooltip("Type of bullet spread: \nFixed spread creates a circle of pellets around one perfectly accurate pellet. \nRandom will create a random spread of pellets around one perfectly accurate pellet.\nNone will create one pellet that is perfectly accurate.")]
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
    /// <returns>The impact Point in World space. The Ray's direction if the ray hit nothing</returns>
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

    /// <summary>
    /// Will determine a bullet spread for the gun based on how many pellets it shoots. 
    /// It will put one pellet in the middle, and then a ring of pellets around it. 
    /// </summary>
    /// <param name="bulletOriginPoint">The point from which the bullet will originate. generally should be the end of a gun. </param>
    /// <param name="gunTarget">The point the crosshair is aiming at</param>
    /// <param name="pelletCount"> the amount of pellets the gun will fire a once</param>
    /// <param name="spreadAngle">The spread angle. Default is 10.</param>
    /// <returns>A list of Vector3 that comprises the direction of each shot</returns>
    public List<Vector3> DetermineFixedSpreadDirection(Vector3 bulletOriginPoint, Vector3 gunTarget, int pelletCount, float spreadAngle = 10)
    {
        List<Vector3> spreadPattern = new List<Vector3>();
        float normalizedSpreadAngle = spreadAngle / 100;

        Vector3 baseDirection = (gunTarget - bulletOriginPoint).normalized;
        spreadPattern.Add(baseDirection);

        Vector3 right = Vector3.Cross(baseDirection, Vector3.up).normalized;
        Vector3 up = Vector3.up;

        for (int i = 0; i < pelletCount-1; i++)
        {
            float angle = i * (360f / (pelletCount-1));
            float radians = angle * Mathf.Deg2Rad;

            Vector3 deviation = (right * Mathf.Cos(radians) + up * Mathf.Sin(radians)) * normalizedSpreadAngle;

            Vector3 adjustedDirection = (baseDirection + deviation).normalized;

            // Add the adjusted direction to the result list
            spreadPattern.Add(adjustedDirection);
        }

        return spreadPattern;


    }
    /// <summary>
    /// Will determine a bullet spread for the gun based on how many pellets it shoots. 
    /// Will put one pellet in the middle, then randomly deviate the other pellets
    /// </summary>
    /// <param name="bulletOriginPoint">The point from which the bullet will originate. generally should be the end of a gun. </param>
    /// <param name="gunTarget">The point the crosshair is aiming at</param>
    /// <param name="pelletCount"> the amount of pellets the gun will fire a once</param>
    /// <param name="spreadAngle">The spread angle. Default is 10.</param>
    /// <returns>A list of Vector3 that comprises the direction of each shot</returns>
    public List<Vector3> DetermineRandomSpreadPattern(Vector3 bulletOriginPoint, Vector3 gunTarget, int pelletCount, float spreadAngle = 10)
    {
        float normalizedSpreadAngle = spreadAngle / 500;
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
    /// <summary>
    /// Will return a bullet spread pattern. It consists of only one perfectly accurate bullet. Generally only used for testing.
    /// </summary>
    /// <param name="bulletOriginPoint">The point from which the bullet will originate. generally should be the end of a gun. </param>
    /// <param name="gunTarget">The point the crosshair is aiming at</param>
    /// <returns>A list of Vector 3. which will only contain one Vector3. a perfectly accurate one.</returns>
    public List<Vector3> NoSpreadPattern(Vector3 bulletOriginPoint, Vector3 gunTarget)
    {
        List<Vector3> spreadPattern = new List<Vector3>();
        var direction = (gunTarget - bulletOriginPoint).normalized;
        spreadPattern.Add(direction);
        return spreadPattern;
    }

    /// <summary>
    /// Does raycasts for each Vector3 in the spread pattern, This is when the actual "Shot" happens.
    /// </summary>
    /// <param name="spreadPattern">A list of Vector3 that </param>
    /// <param name="bulletOriginPoint">The Bullet origin point. usually the end of a gun.</param>
    /// <returns>A List of RaycastHit. Which contains every pellet and whatever it hit.</returns>
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

    
    /// <summary>
    /// Intended to be used to trigger whatever the intended effect is whenever a bullet hits an object. 
    /// I.E. Enemies will take damage, Explosive barrels will explode, Etc. 
    /// No convention exists for this yet. so it is empty aside from a debug shot indicator function.
    /// </summary>
    /// <param name="target">A bullet that hit something.</param>
    public void DoOnBulletHitAction(RaycastHit target)
    {
        //YOU SHOULD CALL A FUNCTION HERE THAT 

        //Creates a "DebugPelletHitIndicator" wherever the bullet hits. Was used in testing. Is disabled but left in for convenience. 
        //Instantiate(DebugPelletHitIndicator, target.point, Quaternion.identity);
    }
    void DoHitReg()
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
        else if(SpreadType == SpreadType.NONE)
        {
            spreadPattern = NoSpreadPattern(BulletOriginPoint.position, gunTarget);
        }
        else
        {
            Debug.LogError("Unknown Spread Type. No spread will be added");
            spreadPattern = NoSpreadPattern(BulletOriginPoint.position, gunTarget);
        }
        var hits = DeterminePelletHits(spreadPattern, BulletOriginPoint);

        foreach (var target in hits)
        {
            DoOnBulletHitAction(target);
        }
    }
}
