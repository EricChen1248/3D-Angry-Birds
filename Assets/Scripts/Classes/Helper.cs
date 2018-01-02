using Classes.Entities;
using Classes.Objects;
using UnityEngine;

namespace Classes
{
    internal static class Helper
    {
        internal static System.Random Random = new System.Random();

        internal static void GenerateExplosion(Vector3 position)
        {
            const float explosionForce = 150f;
            const float radius = 20f;
            const float upwards = 3f;

            var smoke = Object.Instantiate((GameObject) Resources.Load("SmokeSystem"));
            smoke.transform.position = position;
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
