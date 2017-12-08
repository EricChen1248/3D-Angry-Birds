using UnityEngine;

namespace Classes.Entities
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
            if (isShooting == false)
            {
                Shoot();
            }

            if (Rigidbody.useGravity)
            {
               Rigidbody.AddForce(-Physics.gravity * 0.6f);
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
        private bool isShooting = true;

        private void OnMouseDown()
        {
            // No shooting in camera mode
            if (isShooting == false)
            {
                return;
            }
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }

        private void OnMouseDrag()
        {
            // No shooting in camera mode
            if (isShooting == false)
            {
                return;
            }

            var curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)) 
                                + offset - GameObject.FindGameObjectWithTag("Slingshot").transform.position;

            // Making sure ammo can't go infront or too far away from slingshot
            curPosition.z = Mathf.Max(curPosition.z, 0);
            curPosition.z = Mathf.Min(curPosition.z, 10);

            curPosition.y = Mathf.Max(curPosition.y, -0.5f);

            SlingshotPouch.Instance.transform.localPosition = curPosition;
            SlingshotPouch.Instance.transform.localPosition.Scale(new Vector3(0, 1, 1));

            transform.localPosition = Vector3.zero;

            var fpCamera = SlingshotPouch.Instance.transform.parent.Find("Camera");
            var direction = SlingshotPouch.Instance.transform.parent.transform.position + SlingshotPouch.StartingPosition -
                            transform.position;
            fpCamera.rotation = Quaternion.LookRotation(direction);
        }

        private void OnMouseUp()
        {
            isShooting = false;
            Rigidbody.useGravity = true;
        }

        private void ReloadAmmo()
        {
            if (SlingshotPouch.Instance.GetComponent<SlingshotPouch>().CurrentAmmo != null)
                return;

            SlingshotPouch.Instance.GetComponent<SlingshotPouch>().Reset();



        }


        // Starts shooting process of bird;
        private void Shoot()
        {
            var slingshot = SlingshotPouch.Instance.transform.parent.transform;
            var force = slingshot.position + SlingshotPouch.StartingPosition - transform.position;
            //var force = SlingshotPouch.StartingPosition - SlingshotPouch.Instance.transform.localPosition;
            Rigidbody.AddForce(force * 30);
            Rigidbody.velocity.Scale(new Vector3(1,3,1));
            SlingshotPouch.Instance.transform.position = transform.position;
            if (SlingshotPouch.Instance.transform.localPosition.z <= 0)
            {                
                transform.parent = null;
                SlingshotPouch.Instance.GetComponent<SlingshotPouch>().CurrentAmmo = null;
                ReloadAmmo();
                isShooting = true;
            }
        }
    }
}
