using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static bool DoDebugRendering;

    private static bool _hasInitialized = false;

    [SerializeField]
    public bool _doDebugRendering;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!_hasInitialized)
        {
            _hasInitialized = true;
            DoDebugRendering = _doDebugRendering;
        }
    }
}
