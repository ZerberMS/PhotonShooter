using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviourPunCallbacks
{
    [SerializeField] float      healthRefreshTime = 5;
    [SerializeField] int        healAmount        = 25;
    [SerializeField] PhotonView fv;

    [SerializeField] AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        Heal(other);
        fv.RPC("HealSpawnManager", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    private void HealSpawnManager()
    {
        gameObject.SetActive(false);
        Invoke("Refresh", healthRefreshTime);
    }

    private void Heal(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Heal(healAmount);
            audioSource.Play();
        }
    }

    void Refresh()
    {
        gameObject.SetActive(true);
    }
}
