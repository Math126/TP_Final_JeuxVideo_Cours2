using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class Owl : MonoBehaviour
{
    Vector3 Tour2 = new Vector3(42.5f, 23.5f, 0), Tour1 = new Vector3(7.5f, 33.25f, 0); //Tour1 = Client, Tour2 = Server
    Vector3 target = Vector3.zero;
    GameObject ObjTenu;
    public XRSocketInteractor socket;

    public void ResetTarget()
    {
        target = target = Vector3.zero;
        socket.enabled = false;
        ObjTenu.transform.SetParent(null);
        StartCoroutine(Delai());
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjTenu = other.gameObject;
    }

    public void onAttached()
    {
        float Distance = Vector3.Distance(transform.position, Tour1);

        if (Distance <= 0.2f)
        {
            target = Tour2;
        }
        else
        {
            target = Tour1;
        }

        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Character"))
        {
            p.GetComponent<PlayerNetwork>().OwlObjServerRpc();
            p.GetComponent<PlayerNetwork>().OwlPosServerRpc(target);
        }
    }

    public GameObject GetObjTenu()
    {
        return ObjTenu;
    }

    public GameObject GetSocketLoc()
    {
        return socket.transform.gameObject;
    }

    IEnumerator Delai()
    {
        yield return new WaitForSeconds(3);
        socket.enabled = true;
    }
}