using System.Collections;
using Interfaces;
using UnityEngine;

namespace Classes.Objects
{
    public class Blocks : MonoBehaviour, IBreakables
    {

        public float BreakThreshold = 3f;

        private bool pendingBreak;

        private bool pendingDestroy;

        private float impactRate;

	
        // Update is called once per frame
        private void Update () {
            if (pendingBreak && pendingDestroy == false)
            {
                pendingDestroy = true;
                StartCoroutine(CoUpdate());
            }
        }

    

        // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
        private void OnCollisionEnter(Collision collision)
        {
            var impact = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody>().mass;
            if (!(impact > BreakThreshold)) return;
            impactRate = impact / BreakThreshold;
            Break();
        }


        public virtual void Break()
        {
            if (pendingBreak)
            {
                return;
            }
            
            InstatiateChildBlock(0.49f);
            InstatiateChildBlock(-0.49f);

            Destroy(gameObject);

        }

        private void InstatiateChildBlock(float position)
        {
            var block = Instantiate(this).transform;
            var blockScript = block.GetComponent<Blocks>();
            blockScript.impactRate = impactRate;
            blockScript.pendingBreak = true;
            block.parent = transform;
            block.localScale = new Vector3(1, 0.48f, 1);
            block.localPosition = new Vector3(0, position, 0);
            block.localRotation = Quaternion.identity;
            block.parent = null;
        }

        internal IEnumerator CoUpdate ()
        {
            pendingDestroy = true;
            pendingBreak = true;
            yield return new WaitForSeconds(2f / impactRate);
            Destroy(gameObject);
        }
        
    }
}
