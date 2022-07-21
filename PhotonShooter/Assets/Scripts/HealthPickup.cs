using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] float healthRefreshTime = 5;
    [SerializeField] int healAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        Heal(other);
    }

    private void Heal(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Heal(healAmount);
            gameObject.SetActive(false);
            Invoke("Refresh", healthRefreshTime);
        }
    }

    void Refresh()
    {
        gameObject.SetActive(true);
    }
}
