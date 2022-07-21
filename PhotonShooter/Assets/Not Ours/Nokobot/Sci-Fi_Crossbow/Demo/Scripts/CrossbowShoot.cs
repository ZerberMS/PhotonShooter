using UnityEngine;

namespace Nokobot.Assets.Crossbow
{
    public class CrossbowShoot : MonoBehaviour
    {
        [SerializeField] ParticleSystem ps;
        public Transform arrowLocation;

        public float shotPower = 100f;

        void Start()
        {
            if (arrowLocation == null)
                arrowLocation = transform;
        }

        void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                ps.Play();
            }
        }
    }
}
