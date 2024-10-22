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
        Debug.Log("highest Priority: "+ highestPriority);
        IEnumerable<StaticGravityField> highestPriorityFields = GravityFields.Where(field => field.Priority == highestPriority);
        foreach (StaticGravityField gravityField in highestPriorityFields)
        {
            Debug.Log(gravityField.name + " Priority: " + gravityField.Priority + "Gravity Force: " + gravityField.GravityForce);
            _rigidBody.AddForce(gravityField.GravityForce);
        }
    }
}
