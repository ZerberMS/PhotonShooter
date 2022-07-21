using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider col;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Crossbow") || collision.gameObject.CompareTag("Bolt"))
        {
            return;
        }
        else
        {
            rb.isKinematic = true;
            Destroy(col);
            transform.SetParent(collision.gameObject.transform);
        }
    }
}
