using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Knite_Rotation : NetworkBehaviour
{
    public GameObject XR_Origin;

    void Update()
    {
        transform.rotation = XR_Origin.transform.rotation;
    }
}