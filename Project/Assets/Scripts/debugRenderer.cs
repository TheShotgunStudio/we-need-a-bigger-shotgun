using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugRenderer : MonoBehaviour
{

    public List<MeshRenderer> DebugRenderers;
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManager.DoDebugRendering)
        {
            foreach (var ren in DebugRenderers)
            {
                ren.enabled = false;
            }
        }
    }

}
