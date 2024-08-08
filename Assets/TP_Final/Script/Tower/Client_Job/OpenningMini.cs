using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenningMini : MonoBehaviour
{
    public List<MiniRune> MiniRuneList;
    private int nb = 0;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Open()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 27.5f, transform.position.z), Time.deltaTime * 0.02f);
    }

    private void Update()
    {
        foreach (MiniRune rune in MiniRuneList)
        {
            if (rune.GetGood())
            {
                nb++;
            }
        }

        if (nb == 5)
            Open();
        else
            nb = 0;
    }
}