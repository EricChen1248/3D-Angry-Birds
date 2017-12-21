using UnityEngine;

namespace Classes.Objects
{
    public class Balloon : Blocks
    {
        public GameObject ObjectToLift;
        public Balloon()
        {
            BreakThreshold = 0;
        }

        public void Start()
        {
            ObjectToLift.GetComponent<Rigidbody>().isKinematic = true;
        }
        // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
        private void OnTriggerEnter(Collider other)
        {
            Break();
        }
        protected override void Break()
        {
            ObjectToLift.GetComponent<Rigidbody>().isKinematic = false;
            ObjectToLift.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
