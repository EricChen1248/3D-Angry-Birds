using Classes;
using Controllers;
using UnityEditor;
using UnityEngine;

namespace Birds
{
    public class Bird: MonoBehaviour
    {
        public Vector3 Speed = new Vector3(10,10,10);
        public static GameObject Pouch;
        public float Mass = 1f;
        

        private Rigidbody Rigidbody { get; set; }

        private Vector3 Velocity
        {
            get { return Rigidbody.velocity; }
            set { Rigidbody.velocity = value; }
        }

        // Use this for initialization
        private void Start ()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.mass = Mass;
        }
	
        // Update is called once per frame
        private void FixedUpdate ()
        {
            if (isShooting)
            {
                Shoot();
            }

            if (Rigidbody.useGravity)
            {
               Rigidbody.AddForce(-Physics.gravity * 0.8f);
            }
        }

        public void SetVelocity(Vector3 velocity)
        {
            Velocity = velocity;
        }

        public float GetForce(Collision collision)
        {
            return collision.relativeVelocity.magnitude * Mass;
        }

        private Vector3 screenPoint;
        private Vector3 offset;
        private bool isShooting;

        private void OnMouseDown()
        {
            // No shooting in camera mode
            if (CoreController.PlayerMode == PlayerMode.CameraMode)
            {
                return;
            }
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }

        private void OnMouseDrag()
        {
            // No shooting in camera mode
            if (CoreController.PlayerMode == PlayerMode.CameraMode)
            {
                return;   
            }
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            
            // Making sure ammo can't go infront of slingshot
            if ((curPosition - GameObject.FindGameObjectWithTag("Slingshot").transform.position + SlingshotPouch.StartingPosition).z <= 0)
            {
                curPosition.z = GameObject.FindGameObjectWithTag("Slingshot").transform.position.z + SlingshotPouch.StartingPosition.z;
            }
            SlingshotPouch.Self.transform.position = curPosition;
            
            SlingshotPouch.Self.transform.localPosition.Scale(new Vector3(0, 1, 1));
            transform.position = SlingshotPouch.Self.transform.position;
        }

        private void OnMouseUp()
        {
            // Disable Shot
            CoreController.PlayerMode = PlayerMode.CameraMode;
            isShooting = true;
            Rigidbody.useGravity = true;
            //ReloadAmmo();
        }

        private void ReloadAmmo()
        {
            // TODO : Reload Ammo
            SlingshotPouch.Self.GetComponent<SlingshotPouch>().Reset();
            
        }

        private void Shoot()
        {
            var force = SlingshotPouch.StartingPosition - SlingshotPouch.Self.transform.localPosition;
            Rigidbody.AddRelativeForce(force * 10);
            SlingshotPouch.Self.transform.position = transform.position;
            if (SlingshotPouch.Self.transform.localPosition.z <= 0)
            {
                isShooting = false;
                ReloadAmmo();
                transform.parent = null;
            }
        }
    }
}
