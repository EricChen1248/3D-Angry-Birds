using UnityEngine;

namespace Classes.Objects
{
    public class Barrel : Blocks
    {
        public GameObject ExplosionFragment;

        private readonly System.Random random = new System.Random();
        private Vector3 Position
        {
            get { return transform.position; }
        }

        // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
        private void OnCollisionEnter(Collision collision)
        {
            if (ReactsToCollision == false)
                return;
            
            var impact = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody>().mass;
            if (!(impact > BreakThreshold)) return;
            ReactsToCollision = false;
            ImpactRate = impact / BreakThreshold;
            Break();
        }

        protected override void Break()
        {
            Destroy(gameObject);
            Helper.GenerateExplosion(Position);
            Score.UpdateScore(BreakScore);
        }
        
    }
}
