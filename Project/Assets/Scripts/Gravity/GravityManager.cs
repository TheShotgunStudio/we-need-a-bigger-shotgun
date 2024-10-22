using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityManager : MonoBehaviour
{
    [HideInInspector]
    public List<StaticGravityField> GravityFields;

    private Rigidbody _rigidBody;
    private void Awake()
    {
        this._rigidBody = GetComponent<Rigidbody>();
        this._rigidBody.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (GravityFields.Count <= 0) return;
        int highestPriority = GravityFields.Max(field => field.Priority);
        IEnumerable<StaticGravityField> highestPriorityFields = GravityFields.Where(field => field.Priority == highestPriority);
        foreach (StaticGravityField gravityField in highestPriorityFields)
        {
            _rigidBody.AddForce(gravityField.GravityForce);
        }
    }
}
