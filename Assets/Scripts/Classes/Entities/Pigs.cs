﻿using UnityEngine;

namespace Classes.Entities
{
    internal class Pigs : MonoBehaviour
    {
        public float BreakThreshold = 5f;



        // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
        private void OnCollisionEnter(Collision collision)
        {
            var impact = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody>().mass;
            if (!(impact > BreakThreshold)) return;
            Break();
        }

        public virtual void Break()
        {
            Destroy(gameObject);
        }

    }
}