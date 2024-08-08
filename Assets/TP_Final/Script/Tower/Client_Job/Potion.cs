using UnityEngine;

public class Potion : MonoBehaviour
{
    private string type = "";
    private bool canBeUse = false, isTaken = false;

    public void SetPotion(string nom)
    {
        type = nom;
        if (nom != "")
        {
            transform.Find("Liquid").gameObject.SetActive(true);
            transform.Find("Bubble").gameObject.SetActive(true);
            canBeUse = true;
        }
        else
        {
            transform.Find("Liquid").gameObject.SetActive(false);
            transform.Find("Bubble").gameObject.SetActive(false);
            canBeUse = false;
        }
    }

    public bool GetPotionReady()
    {
        return canBeUse;
    }

    public bool GetTakenPotion()
    {
        return isTaken;
    }

    public string GetTypePotion()
    {
        return type;
    }

    public void Selected()
    {
        isTaken = true;
    }

    public void Release()
    {
        isTaken = false;
    }
}