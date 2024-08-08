using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    private bool TienPotion = false, PotionPrete = false, AlreadyHaveEffect = false, peutRedevenirGrand = false, triggerManivelle = false, canMoveAgain = true;
    private GameObject Manivelle = null;
    private string typePotion = "";
    private Vector3 targetScale = new Vector3(2.25f, 2.25f, 2.25f);
    private float DelaiManivelle = 2f, liveTime = 0f;


    private void Start()
    {
        XRInputManager.Hand.onPrimaryChange += StartHost;
        XRInputManager.Hand.onSecondaryChange += StartClient;
        XRInputManager.Hand.onGripperStateChange += NotUse;
        XRInputManager.Hand.onTriggerStateChange += UsePotion;
        XRInputManager.Hand.onThumbstickClickStateChange += ThumbstickClick;
        XRInputManager.Hand.onThumbstickTouchStateChange += NotUse;

        XRInputManager.Hand.onGripperValueChange += NotUse;
        XRInputManager.Hand.onTriggerValueChange += NotUse;
        XRInputManager.Hand.onThumbstickValueChange += NotUse;

        XRInputManager.Hand.onHandPositionChange += NotUse;
        XRInputManager.Hand.onHandRotationChange += NotUse;
        XRInputManager.Hand.onHandSpeedChange += NotUse;
        XRInputManager.Head.onHeadPositionChange += NotUse;
        XRInputManager.Head.onHeadRotationChange += NotUse;
        XRInputManager.Head.onHeadSpeedChange += NotUse;
    }

    private void Update()
    {
        liveTime += Time.deltaTime;

        if(liveTime >= DelaiManivelle)
        {
            canMoveAgain = true;
        }

        float diffScale = Vector3.Distance(transform.localScale, targetScale);

        if (diffScale > 0.01f)
        {
            Vector3 newScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime);
            transform.localScale = newScale;
        }
    }


    private void StartHost(string hand, bool state)
    {
        if (state)
        {
            NetworkManager.Singleton.StartHost();
            Destroy(gameObject);
        }
    }

    private void StartClient(string hand, bool state)
    {
        if (state)
        {
            NetworkManager.Singleton.StartClient();
            Destroy(gameObject);
        }
    }

    private void UsePotion(string hand, bool state)
    {
        if (state && TienPotion && PotionPrete)
        {
            if (typePotion == "Drogue")
            {
                RenderSettings.ambientIntensity = 0;
                RenderSettings.reflectionIntensity = 0;
                GameObject.Find("DrogueView").SetActive(true);
            }
            else if (typePotion == "Mini")
            {
                targetScale = new Vector3(0.15f, 0.15f, 0.15f);
                transform.parent.Find("Knight").gameObject.SetActive(false);
                peutRedevenirGrand = true;
            }
            AlreadyHaveEffect = true;
        }
    }

    private void ThumbstickClick(string hand, bool state)
    {
        if (state)
        {
            if (AlreadyHaveEffect)
            {
                if (typePotion == "Drogue")
                {
                    RenderSettings.ambientIntensity = 1;
                    RenderSettings.reflectionIntensity = 1;
                    GameObject.Find("DrogueView").SetActive(false);
                }
                else if (typePotion == "Mini" && peutRedevenirGrand)
                {
                    targetScale = new Vector3(2.25f, 2.25f, 2.25f);
                    transform.parent.Find("Knight").gameObject.SetActive(true);
                }
            }
            else
            {
                if (triggerManivelle && canMoveAgain)
                {
                    Manivelle.GetComponent<ChariotManivelle>().ChangePos();
                    canMoveAgain = false;
                    liveTime = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            TienPotion = other.GetComponent<Potion>().GetTakenPotion();
            PotionPrete = other.GetComponent<Potion>().GetPotionReady();
            typePotion = other.GetComponent<Potion>().GetTypePotion();
        }
        else if (other.CompareTag("MiniMonde") && AlreadyHaveEffect)
        {
            peutRedevenirGrand = false;
        }
        else if (other.CompareTag("Manivelle"))
        {
            triggerManivelle = true;
            Manivelle = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            transform.parent.GetComponent<PlayerNetwork>().ReSpawnClientRpc();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MiniMonde") && AlreadyHaveEffect)
        {
            peutRedevenirGrand = true;
        }
        else if (other.CompareTag("Manivelle"))
        {
            triggerManivelle = false;
        }
    }

    

    private void NotUse(string hand, bool state) { }
    private void NotUse(string hand, Vector2 value) { }
    private void NotUse(string hand, Vector3 value) { }
    private void NotUse(string hand, float value) { }
}