using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Ground parent;

    void Start()
    {
        parent = GetComponentInParent<Ground>();
    }


    private void OnDestroy()
    {
        parent.UnBlock();
    }
}
