using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadreButton : MonoBehaviour
{
    public List<Material> Materials;
    public MeshRenderer FramePicture;
    private int indexImage = 0;

    private void OnTriggerEnter(Collider other)
    {
        indexImage++;

        if (indexImage == Materials.Count)
        {
            indexImage = 0;
        }

        UpdateFrameMaterial();
        Debug.Log(indexImage);
    }

    private void UpdateFrameMaterial()
    {
        if (Materials.Count > 0 && FramePicture != null)
        {
            int materialIndex = indexImage % Materials.Count;
            FramePicture.material = Materials[materialIndex];
        }
    }

    public int GetIndex()
    {
        return indexImage;
    }
}
