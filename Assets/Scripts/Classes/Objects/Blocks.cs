using System.Collections;
using Interfaces;
using UnityEngine;

namespace Classes.Objects
{
    public class Blocks : MonoBehaviour, IBreakables
    {

        public float BreakThreshold = 3f;

        protected float impactRate;
        protected bool reactsToCollision = true;
	
        // Update is called once per frame
        private void Update () 
        {
        }

    

        // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
        private void OnCollisionEnter(Collision collision)
        {
            if (reactsToCollision == false)
                return;
                
            var impact = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody>().mass;
            if (!(impact > BreakThreshold)) return;
            impactRate = impact / BreakThreshold;
            Break();
        }


        public virtual void Break()
        {            
            InstatiateChildBlock(0.49f);
            InstatiateChildBlock(-0.49f);

            Destroy(gameObject);
        }

        private void InstatiateChildBlock(float position)
        {
            var block = Instantiate(this).transform;
            var blockScript = block.GetComponent<Blocks>();
            blockScript.reactsToCollision = false;
            block.parent = transform;
            block.localScale = new Vector3(1, 0.48f, 1);
            block.localPosition = new Vector3(0, position, 0);
            block.localRotation = Quaternion.identity;
            block.parent = null;
            Destroy(block.gameObject, 2f / impactRate);
        }
    }
}
