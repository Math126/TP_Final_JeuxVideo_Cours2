using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotFleur : MonoBehaviour
{
    public List<GameObject> Plants = new List<GameObject>();

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("SacPlante"))
        {
            foreach (GameObject p in Plants)
            {
                if (p.GetComponent<Fleur>().SacNom == other.tag)
                {
                    Destroy(other.gameObject);
                    GameObject newPlant = Instantiate(p);
                    newPlant.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
                }
            }
        }
    }
}
