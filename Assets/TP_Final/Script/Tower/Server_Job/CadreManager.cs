using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CadreManager : MonoBehaviour
{
    public List<GameObject> Button;
    public List<GameObject> Door;
    private string Val = "XXXX";
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Val = "";
        foreach (GameObject button in Button)
        {
            Val += button.GetComponent<CadreButton>().GetIndex();
        }

        if (Val == "1302")
        {
            foreach (GameObject door in Door)
            {
                gameManager.EndTower();
            }
        }
    }
}
