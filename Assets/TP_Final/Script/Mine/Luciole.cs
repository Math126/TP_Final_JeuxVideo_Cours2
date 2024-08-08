using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luciole : MonoBehaviour
{
    private float amplitude = 0.01f;

    void Update()
    {

        // Calcul des déplacements aléatoires sur les axes x, y, z
        float deplacementX = Random.Range(-amplitude, amplitude);
        float deplacementY = Random.Range(-amplitude, amplitude);
        float deplacementZ = Random.Range(-amplitude, amplitude);

        // Ajout des déplacements à la position actuelle de l'objet
        transform.Translate(new Vector3(deplacementX, deplacementY, deplacementZ));
    }
}
