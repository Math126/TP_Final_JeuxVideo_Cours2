using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public List<GameObject> lstPlante;
    public List<GameObject> lstCrystal;
    public List<GameObject> lstSkull;
    public ParticleSystem smoke;

    private GameObject ChoosenPlant;
    private GameObject ChoosenCrystal;
    private GameObject ChoosenSkull;
    private bool potionReady = false;
    private string typePotion = "";

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ResetListGot();
    }

    private void Update()
    {
        if(ChoosenCrystal != null && ChoosenPlant != null && ChoosenSkull != null)
        {
            if (ChoosenPlant.CompareTag("Mushroom1") && ChoosenCrystal.CompareTag("PurpleCrystal") && ChoosenSkull.CompareTag("Skull2")) //Potion Drogue
            {
                potionReady = true;
                smoke.startColor = Color.green;
                typePotion = "Drogue";
            } 
            else if (ChoosenPlant.CompareTag("Mushroom2") && ChoosenCrystal.CompareTag("BlueCrystal") && ChoosenSkull.CompareTag("Skull1")) //Potion Mini
            {
                potionReady = true;
                smoke.startColor = Color.green;
                typePotion = "Mini";
            }
            else //Potion Raté
            {
                smoke.startColor = Color.black;
                ResetListGot();
                potionReady = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!potionReady)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Plante") && ChoosenPlant == null) //Si capte plante
            {

                if (other.CompareTag("Plant1"))
                {
                    ChoosenPlant = lstPlante[0];
                }
                else if (other.CompareTag("Plant2"))
                {
                    ChoosenPlant = lstPlante[1];
                }
                else if (other.CompareTag("Plant3"))
                {
                    ChoosenPlant = lstPlante[2];
                }
                else if (other.CompareTag("Plant4"))
                {
                    ChoosenPlant = lstPlante[3];
                }
                else if (other.CompareTag("Mushroom1"))
                {
                    ChoosenPlant = lstPlante[4];
                }
                else
                {
                    ChoosenPlant = lstPlante[5];
                }

                Destroy(other.transform.parent.gameObject);
                smoke.Play();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Crystal") && ChoosenCrystal == null) //Si capte Crystal
            {

                if (other.CompareTag("RedCrystal"))
                {
                    ChoosenCrystal = lstCrystal[0];
                    smoke.startColor = Color.red;
                }
                else if (other.CompareTag("BlueCrystal"))
                {
                    ChoosenCrystal = lstCrystal[1];
                    smoke.startColor = Color.blue;
                }
                else
                {
                    ChoosenCrystal = lstCrystal[2];
                    smoke.startColor = new Color(1.0f, 0.0f, 1.0f, 1.0f);
                }

                Destroy(other.transform.parent.gameObject);
                smoke.Play();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Skull") && ChoosenSkull == null) //Si capte Skull
            {

                if (other.CompareTag("Skull1"))
                {
                    ChoosenSkull = lstSkull[0];
                }
                else if (other.CompareTag("Skull2"))
                {
                    ChoosenSkull = lstSkull[1];
                }
                else
                {
                    ChoosenSkull = lstSkull[2];
                }

                Destroy(other.transform.parent.gameObject);
                smoke.Play();
            }
        }

        if (potionReady)
        {
            if (other.CompareTag("Potion"))
            {
                other.GetComponent<Potion>().SetPotion(typePotion);
                potionReady = false;
                ResetListGot();
            }
        }
    }

    private void ResetListGot()
    {
        ChoosenCrystal = null;
        ChoosenPlant = null;
        ChoosenSkull = null;

        smoke.Stop();
    }
}