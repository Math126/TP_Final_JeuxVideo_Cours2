using System;
using System.Collections;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlayerNetwork : NetworkBehaviour
{
    private Vector3 spawnPos;
    private GameManager gameManager;
    private Vector3 targetOwl;


    //Variable Network
    private NetworkVariable<DataConnection> dataConnection = new NetworkVariable<DataConnection>(
        new DataConnection
        {
            idJoueur = 0,
            role = ""
        }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private NetworkVariable<int> NbJoueur = new NetworkVariable<int>(0);

    public struct DataConnection : INetworkSerializable
    {
        public int idJoueur;
        public string role;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref idJoueur);
            serializer.SerializeValue(ref role);
        }
    }

    //Function
    public override void OnNetworkSpawn()
    {
        if (NbJoueur.Value < 2)
        {
            base.OnNetworkSpawn();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            if (IsOwner)
            {
                if (IsHost)
                {
                    dataConnection.Value = new DataConnection
                    {
                        idJoueur = 0,
                        role = "Host"
                    };

                    SpawnServerRpc( (ulong) dataConnection.Value.idJoueur);
                }
                else
                {
                    dataConnection.Value = new DataConnection
                    {
                        idJoueur = 1,
                        role = "Client"
                    };

                    SpawnServerRpc( (ulong) dataConnection.Value.idJoueur);
                }

                gameObject.transform.position = spawnPos;
                gameObject.transform.Rotate(new Vector3(0,-90,0));
                OnSpawnServerRpc();
                RenderSettings.ambientIntensity = 0.7f;

            }
            else
            {
                gameObject.transform.Find("XR Rig").gameObject.SetActive(false);
            }

            if(GameObject.Find("Menu_View") != null)
                Destroy(GameObject.Find("Menu_View"));
        }
    }

    [ClientRpc]
    public void ReSpawnClientRpc()
    {
        if(IsOwner)
            transform.Find("XR Rig").transform.position = spawnPos;
    }

    [ClientRpc]
    public void EndGameClientRpc()
    {
        RenderSettings.ambientIntensity = 1.5f;
        Destroy(gameObject);
    }

    [ClientRpc]
    public void EndTowerClientRpc()
    {
        foreach (GameObject door in GameObject.FindGameObjectsWithTag("Door"))
        {
            Destroy(door);
        }
    }

    [ClientRpc]
    public void SpawnClientRpc(ulong id)
    {
        if(NetworkManager.Singleton.LocalClientId == id)
        {
            spawnPos = new Vector3(0, 0.2f, 0);

            Debug.Log("Le joueur: " + dataConnection.Value.idJoueur + " c'est connecté en tant que " + dataConnection.Value.role);
        }
        
    }

    [ServerRpc]
    public void SpawnServerRpc(ulong id)
    {
        if (id == 1)
            SpawnClientRpc(id);
        else
        {
            spawnPos = new Vector3(50, 20.7f, 0);

            Debug.Log("Le joueur: " + dataConnection.Value.idJoueur + " c'est connecté en tant que " + dataConnection.Value.role);
        }
    }

    [ServerRpc]
    private void OnSpawnServerRpc()
    {
        NbJoueur.Value++;
    }

    [ServerRpc]
    public void OwlObjServerRpc()
    {
        GameObject obj = GameObject.Find("Owl").GetComponent<Owl>().GetObjTenu();

        obj.transform.SetParent(GameObject.Find("Owl").transform);
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<Rigidbody>().isKinematic = true;
    }

    [ServerRpc]
    public void OwlPosServerRpc(Vector3 target)
    {
        GameObject.Find("Owl").GetComponent<NavMeshAgent>().SetDestination(target);
        targetOwl = target;
    }

    [ClientRpc]
    public void OwlReleaseObjClientRpc()
    {
        GameObject.Find("Owl").GetComponent<Owl>().ResetTarget();
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        if(targetOwl != Vector3.zero)
        {
            float distance = Vector3.Distance(GameObject.Find("Owl").transform.position, targetOwl);
            if (distance <= 0.1f)
            {
                OwlReleaseObjClientRpc();
                targetOwl = Vector3.zero;
            }
        }
    }
}