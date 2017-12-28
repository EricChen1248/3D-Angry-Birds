using UnityEngine;

namespace Classes.Objects
{
    public class Blocks : MonoBehaviour
    {

        public float BreakThreshold = 3f;
        public int BreakScore = 1000;

        protected float ImpactRate;
        protected bool ReactsToCollision = true;
	
        // Update is called once per frame
        private void Update () 
        {
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

        protected virtual void Break()
        {            
            InstatiateChildBlock(0.49f);
            InstatiateChildBlock(-0.49f);

            Destroy(gameObject);
            Score.UpdateScore(BreakScore);
        }

        private void InstatiateChildBlock(float position)
        {
            var block = Instantiate(this).transform;
            var blockScript = block.GetComponent<Blocks>();
            blockScript.ReactsToCollision = false;
            block.parent = transform;
            block.localScale = new Vector3(1, 0.48f, 1);
            block.localPosition = new Vector3(0, position, 0);
            block.localRotation = Quaternion.identity;
            block.parent = null;
            Destroy(block.gameObject, 2f / ImpactRate);
        }
       
    }
}
