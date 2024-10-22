using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public List<StaticGravityField> GravityFields;

    private void FixedUpdate()
    {
        foreach (StaticGravityField gravityField in GravityFields)
        {

        }
    }
}
