using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;

    PhotonView PV;

    [SerializeField] AudioSource audioSource;

    [SerializeField] ParticleSystem ps;

    float counter;
    [SerializeField] float timeToShoot = 1f;

    private void Update()
    {
        counter += Time.deltaTime;
    }

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void Use()
    {
        if (counter > timeToShoot)
        {
            Shoot();
            counter = 0;
        }
    }

    public void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamagable>()?.TakeDamage(((GunInfo)ItemInfo).damage);
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        if (PV.IsMine)
        {
            audioSource.Play();
            ps.Play();

            Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
            if (colliders.Length != 0)
            {
                GameObject bulletImpactObj = Instantiate(bulletImpactPrefab, hitPosition + hitNormal * 0.001f,
                    Quaternion.LookRotation(hitNormal, Vector3.up) * bulletImpactPrefab.transform.rotation);
                Destroy(bulletImpactObj, 10f);
                bulletImpactObj.transform.SetParent(colliders[0].transform);
            }
        }
    }

}
