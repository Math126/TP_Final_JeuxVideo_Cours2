using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransfarableObject : MonoBehaviour
{

    public void ObjectTransfert(Vector3 Target)
    {
        transform.position = Target;
    }
}
