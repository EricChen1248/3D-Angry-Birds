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
            for (var i = 0; i < 1000; i++)
            {
                var fragment = Instantiate(ExplosionFragment);
                fragment.transform.position = Position;
                fragment.transform.position += new Vector3(random.Next(-10,10) / 10f, random.Next(-10,10) / 10f, random.Next(0, 10) / 10f);
                fragment.GetComponent<Rigidbody>().AddForce((fragment.transform.position - Position), ForceMode.Impulse);
                fragment.GetComponent<ExplosionFragment>().ParentBarrelPosition = Position;
            }

            Score.UpdateScore(BreakScore);
        }
    }
}
