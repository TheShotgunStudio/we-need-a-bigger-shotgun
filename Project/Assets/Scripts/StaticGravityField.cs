using UnityEngine;

public enum GravityDirection
{
    TOWARDS_X,
    TOWARDS_Y,
    TOWARDS_Z,
    FROM_Z,
    FROM_X,
    FROM_Y
}
public enum GravityMode
{
    ROTATIONAL,
    CARDINAL
}

public class StaticGravityField : MonoBehaviour
{
    [Tooltip("A Rotational Gravity field's direction is controlled by its rotation.\n\nA Cardinal Gravity field will let you pick one of 6 directions. \nTHIS WILL RESET ITS ROTATION TO 0,0,0!! Use Rotational if you want to rotate the object to control its gravity")]
    public GravityMode Mode;

    [Tooltip("Priority handles the calculation priority of gravity. Fields with Priority 0 will not be applied to an object if the object is inside of a field with priority 1.")]
    public int priority;

    [Tooltip("Facing Direction of the gravity field. Uses the XYZ indicator in Unity's scene view (top right of the scene view) as reference for direction. \n\n(Towards-X would mean the gravity would face towards the red X arrow.)")]
    public GravityDirection GravityFacingDirection = GravityDirection.FROM_Y;

    [Tooltip("Gravity strength, Defaults to 1G (9.80665)")]
    public float GravityStrength = 9.80665f;

    private Vector3 _gravityForceDirection;

    [Tooltip("Press this button while the game is running to recalculate the gravity direction.")]
    [InspectorButton(nameof(ReassignGravityForceDirection), ButtonWidth = 200)]
    public bool RecalculateGravityDirection;

    private void OnDrawGizmos()
    {
        Gizmos.color = PickColorBasedOnDirection();
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
        if (Mode == GravityMode.ROTATIONAL)
        {
            //Arrow indicator for rotational gravity fields
            Gizmos.DrawLine(Vector3.zero, Vector3.down);
        }
    }
    private void ReassignGravityForceDirection()
    {
        if (!Application.isPlaying) return;
        this._gravityForceDirection = DetermineGravityForceDirection();
        Color fieldFill = PickColorBasedOnDirection();
        MeshRenderer debugRenderer = GetComponent<MeshRenderer>();
        Material tempMaterial = new Material(debugRenderer.material);
        tempMaterial.color = fieldFill;
        debugRenderer.material = tempMaterial;
    }
    private void Awake()
    {
        Color fieldFill = PickColorBasedOnDirection();
        //set gravity force direction
        _gravityForceDirection = DetermineGravityForceDirection();
        //make debug renderer
        MeshRenderer debugRenderer = GetComponent<MeshRenderer>();
        Material tempMaterial = new Material(debugRenderer.material);
        tempMaterial.color = fieldFill;
        debugRenderer.material = tempMaterial;
        Debug.Log($"{gameObject.name} Gravity force direction: {_gravityForceDirection}");
    }
    private void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.AddForce(_gravityForceDirection);
    }
    /// <summary>
    /// Determines the Direction in which gravity will pull its player.
    /// </summary>
    /// <returns>A Vector3 That is to be used as a direction </returns>
    private Vector3 DetermineGravityForceDirection()
    {
        Debug.Log("YEEY");
        if (Mode == GravityMode.CARDINAL)
        {
            gameObject.transform.rotation = Quaternion.identity;
            switch (GravityFacingDirection)
            {
                case GravityDirection.TOWARDS_X:
                    return new Vector3(GravityStrength, 0, 0);
                case GravityDirection.TOWARDS_Y:
                    return new Vector3(0, GravityStrength, 0);
                case GravityDirection.TOWARDS_Z:
                    return new Vector3(0, 0, GravityStrength);
                case GravityDirection.FROM_X:
                    return new Vector3(-GravityStrength, 0, 0);
                case GravityDirection.FROM_Y:
                    return new Vector3(0, -GravityStrength, 0);
                case GravityDirection.FROM_Z:
                    return new Vector3(0, 0, -GravityStrength);
                default:
                    Debug.LogError("Unknown Gravity Direction, falling back to Default: Rotational"); return -transform.up;
            }
        }
        else if (Mode == GravityMode.ROTATIONAL)
        {
            return -transform.up;
        }
        else
        {
            Debug.LogError("Unknown Gravity Direction, falling back to Default: Rotational");
            return -transform.up;
        }
    }
    /// <summary>
    /// Determines which colour the Gravity field needs to be depending on the Mode. 
    /// </summary>
    /// <returns>A Colour</returns>
    private Color PickColorBasedOnDirection()
    {
        var opacity = 0.2f; // Opacity for Gravity fields. 

        if (Mode == GravityMode.CARDINAL)
        {
            // Assign colors based  on the selected GravityDirection
            switch (this.GravityFacingDirection)
            {
                case GravityDirection.TOWARDS_Y:
                    return new Color(0f, 1f, 0f, opacity);
                case GravityDirection.FROM_Y:
                    return new Color(1f, 0f, 1f, opacity);
                case GravityDirection.TOWARDS_X:
                    return new Color(1f, 0f, 0f, opacity);
                case GravityDirection.FROM_X:
                    return new Color(0f, 1f, 1f, opacity);
                case GravityDirection.TOWARDS_Z:
                    return new Color(0f, 0f, 1f, opacity);
                case GravityDirection.FROM_Z:
                    return new Color(1f, 1f, 0f, opacity);
            }
        }
        else if (Mode == GravityMode.ROTATIONAL)
        {
            Color colorUp = new Color(0f, 1f, 0f, opacity);
            Color colorDown = new Color(1f, 0f, 1f, opacity);
            Color colorRight = new Color(1f, 0f, 0f, opacity);
            Color colorLeft = new Color(0f, 1f, 1f, opacity);
            Color colorForward = new Color(0f, 0f, 1f, opacity);
            Color colorBackward = new Color(1f, 1f, 0f, opacity);

            var downDirection = -transform.up;

            float dotUp = Mathf.Max(0, Vector3.Dot(downDirection, Vector3.up));         
            float dotDown = Mathf.Max(0, Vector3.Dot(downDirection, Vector3.down));     
            float dotRight = Mathf.Max(0, Vector3.Dot(downDirection, Vector3.right));   
            float dotLeft = Mathf.Max(0, Vector3.Dot(downDirection, Vector3.left));     
            float dotForward = Mathf.Max(0, Vector3.Dot(downDirection, Vector3.forward));
            float dotBackward = Mathf.Max(0, Vector3.Dot(downDirection, Vector3.back)); 

            // Sum of all the dot products (for normalization)
            float sum = dotUp + dotDown + dotRight + dotLeft + dotForward + dotBackward;

            // If the sum is 0, return a default color (shouldn't happen unless the object has no down direction)
            if (sum <= 0.001f)
            {
                return new Color(1f, 1f, 1f, opacity);
            }

            // Interpolate colors based on their weights (dot products normalized by sum)
            Color interpolatedColor =
                (dotUp * colorUp +
                dotDown * colorDown +
                dotRight * colorRight +
                dotLeft * colorLeft +
                dotForward * colorForward +
                dotBackward * colorBackward) / sum;

            return interpolatedColor;
        }
        
        return new Color(1f, 1f, 1f, opacity);
    }

}
