using Classes.Entities;
using Classes.Objects;
using UnityEngine;

namespace Classes
{
    internal static class Helper
    {
        internal static System.Random Random = new System.Random();
        internal static GameObject ExplosionFragment = (GameObject) Resources.Load("Explosion Fragment");

        internal static void GenerateExplosion(Vector3 position)
        {
            const float explosionForce = 150f;
            const float radius = 20f;
            const float upwards = 3f;

            var colliders = Physics.OverlapSphere(position, 20);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Bird>() != null)
                {
                    continue;
                }

                var rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(explosionForce, position, radius, upwards);
            }
        }
    }
}
