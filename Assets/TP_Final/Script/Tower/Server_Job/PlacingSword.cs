using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingSword : MonoBehaviour
{
    private int nbPlaced = 0;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (nbPlaced == 3)
        {
            GameObject top = transform.Find("Lid").gameObject;

            top.transform.rotation = Quaternion.Slerp(top.transform.rotation, Quaternion.Euler(0, 90, -60), 5 * Time.deltaTime);
        }
    }

    public void OnePlaced()
    {
        nbPlaced++;
    }

    public void OneOut()
    {
        nbPlaced--;
    }
}