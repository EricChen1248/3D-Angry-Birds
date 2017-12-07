using System;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    public class Slingshot : MonoBehaviour {

        public const float RotationSpeed = 2f;
        public GameObject[] Birds;
        public GameObject Pouch;
        private readonly Queue<GameObject> birdsList = new Queue<GameObject>();
        private float zoom = 10f;


        // Use this for initialization
        internal void Start () {
            foreach (var bird in Birds)
            {
                birdsList.Enqueue(bird);
            }
            SlingshotPouch.Self.GetComponent<SlingshotPouch>().Reset();
        }
	
        // Update is called once per frame
        internal void FixedUpdate ()
        {
            zoom += Input.GetAxisRaw("Zoom") / 5;
            Mathf.Clamp(zoom, 1, 15);
            var fpCamera = transform.Find("Camera").GetComponent<Camera>();
            fpCamera.fieldOfView = 75 * zoom / 10;

            var rotation = Input.GetAxisRaw("Spin") * RotationSpeed * zoom / 10;
            transform.Rotate(new Vector3(0, rotation, 0));



            // Handles Drawing the slingshot line
            var lineRenderer = GetComponent<LineRenderer>();

            var pouchLoc = Pouch.transform.localPosition;
            lineRenderer.SetPosition(1, new Vector3(pouchLoc.x + 0.72f, pouchLoc.y, pouchLoc.z));
            lineRenderer.SetPosition(2, new Vector3(pouchLoc.x - 0.72f, pouchLoc.y, pouchLoc.z));
        }

        internal GameObject Refill()
        {
            try
            {
                return birdsList.Dequeue();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }


    }
}
