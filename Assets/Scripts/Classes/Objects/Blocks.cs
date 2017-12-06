using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Blocks : MonoBehaviour, IBreakables
{

    public float BreakThreshold = 3f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    // OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider
    private void OnCollisionEnter(Collision collision)
    {
        var impact = collision.relativeVelocity.magnitude * collision.gameObject.GetComponent<Rigidbody>().mass;
        if (!(impact > BreakThreshold)) return;
        Break(impact * impact / BreakThreshold / BreakThreshold);
    }


    public virtual void Break(float impactRate)
    {
        StartCoroutine(CoUpdate(impactRate));
    }

    internal IEnumerator CoUpdate (float impactRate){
        yield return new WaitForSeconds(0.2f / impactRate);
        Destroy(gameObject);
    }
}
