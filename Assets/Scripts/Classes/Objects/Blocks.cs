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
        Debug.Log(impact);
        if (!(impact > BreakThreshold)) return;
        Break();
    }


    public void Break()
    {
        Destroy(gameObject);
    }
}
