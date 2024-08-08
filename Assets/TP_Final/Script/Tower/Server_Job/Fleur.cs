using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleur : MonoBehaviour
{
    public Vector3 tailleVoulue = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 tailleInitiale = new Vector3(1.0f, 1.0f, 1.0f);
    public string SacNom;

    private float vitesseCroissance = 0.1f;

    private void Start()
    {
        transform.localScale = tailleInitiale;
    }

    void Update()
    {
        float diffTaille = Vector3.Distance(transform.localScale, tailleVoulue);

        if (diffTaille > 0.005f)
        {
            Vector3 nouvelleTaille = Vector3.Lerp(transform.localScale, tailleVoulue, vitesseCroissance * Time.deltaTime);
            transform.localScale = nouvelleTaille;
        }
    }
}
