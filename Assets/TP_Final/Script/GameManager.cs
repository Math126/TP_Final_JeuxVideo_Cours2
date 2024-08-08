using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void EndGame()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Character"))
        {
            p.GetComponent<PlayerNetwork>().EndGameClientRpc();

            StartCoroutine(DelaiEnd());
        }
    }

    public void EndTower()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Character"))
        {
            p.GetComponent<PlayerNetwork>().EndTowerClientRpc();
        }
    }

    IEnumerator DelaiEnd()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}