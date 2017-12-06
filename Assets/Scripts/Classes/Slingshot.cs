using Controllers;
using UnityEngine;

namespace Classes
{
    public class Slingshot : MonoBehaviour {

        public const float RotationSpeed = 2f;
        public GameObject[] Birds;
        public GameObject Pouch;

        // Use this for initialization
        internal void Start () {
		
        }
	
        // Update is called once per frame
        internal void FixedUpdate ()
        {
            if (CoreController.PlayerMode == PlayerMode.ShootMode)
            {
                var rotation = Input.GetAxisRaw("Spin") * RotationSpeed;
                transform.Rotate(new Vector3(0, rotation, 0));
            }


            // Handles Drawing the slingshot line
            var lineRenderer = GetComponent<LineRenderer>();

            var pouchLoc = Pouch.transform.localPosition;
            lineRenderer.SetPosition(1, new Vector3(pouchLoc.x + 0.72f, pouchLoc.y, pouchLoc.z));
            lineRenderer.SetPosition(2, new Vector3(pouchLoc.x - 0.72f, pouchLoc.y, pouchLoc.z));
        }


    }
}
