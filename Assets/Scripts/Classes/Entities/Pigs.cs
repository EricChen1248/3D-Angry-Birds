using UnityEngine;

namespace Classes.Entities
{
    internal class Pigs : MonoBehaviour
    {
        public float BreakThreshold = 5f;
        private const int BreakScore = 2000;
        
        // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
        public void OnCollisionEnter(Collision collision)
        {
            var impact = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody>().mass;
            if (!(impact > BreakThreshold)) return;
            Break();
        }

        public virtual void Break()
        {
            Destroy(gameObject);
            Score.UpdateScore(BreakScore);
        }

    }
}
