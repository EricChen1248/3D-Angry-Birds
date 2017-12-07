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
        void Update () {
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
            var block = Instantiate(this);
            var blockScript = block.GetComponent<Blocks>();
            blockScript.pendingBreak = true;
            blockScript.impactRate = impactRate;
            block.transform.parent = transform;
            block.transform.localScale = new Vector3(1, 0.48f, 1);
            block.transform.localPosition = new Vector3(0, block.transform.localScale.y, 0);
            block.transform.localRotation = Quaternion.identity;
            block.transform.parent = null;

            block = Instantiate(this);
            blockScript = block.GetComponent<Blocks>();
            blockScript.pendingBreak = true;
            blockScript.impactRate = impactRate;
            block.transform.parent = transform;
            block.transform.localScale = new Vector3(1, 0.48f, 1);
            block.transform.localPosition = new Vector3(0, -block.transform.localScale.y, 0);
            block.transform.localRotation = Quaternion.identity;
            block.transform.parent = null;

            Destroy(gameObject);

        }

        internal IEnumerator CoUpdate ()
        {
            pendingDestroy = true;
            pendingBreak = true;
            yield return new WaitForSeconds(0.5f / impactRate);
            Destroy(gameObject);
        }
    }
}
