using UnityEngine;

namespace Classes.Entities
{
    public class Bird: MonoBehaviour
    {
        public Vector3 Speed = new Vector3(10,10,10);
        public float Mass = 1f;        

        private Rigidbody Rigidbody { get; set; }

        private int explodeTime = 200;
        private Vector3 Velocity
        {
            get { return Rigidbody.velocity; }
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
            if (isShooting && isShot)
            {
                Shoot();
            }

            if (Rigidbody.useGravity)
            {
               Rigidbody.AddForce(-Physics.gravity * 0.6f);
            }

            if (isShot) return;
            if (!(Velocity.magnitude <= 0.3f)) return;
            if (explodeTime <= 0)
            {
                StartShrink();
            }
            --explodeTime;
        }

        private void StartShrink()
        {
            gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            if (gameObject.transform.localScale.x < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        

        private void ReloadAmmo()
        {
            if (SlingshotPouch.Instance.GetComponent<SlingshotPouch>().CurrentAmmo != null)
                return;

            SlingshotPouch.Instance.GetComponent<SlingshotPouch>().Reset();
        }

#region Shooting and Aiming
    
        private Vector3 screenPoint;
        private Vector3 offset;
        private bool isShooting = false;
        private bool isShot = true;

        private void OnMouseDown()
        {
            if (isShooting)
            {
                return;
            }
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        }

        private void OnMouseDrag()
        {
            if (isShooting)
            {
                return;
            }

            var curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)) 
                                + offset - GameObject.FindGameObjectWithTag("Slingshot").transform.position;

            // Making sure ammo can't go infront or too far away from slingshot
            curPosition.z = Mathf.Max(curPosition.z, 0);
            curPosition.z = Mathf.Min(curPosition.z, 10);

            curPosition.y = Mathf.Max(curPosition.y, -0.5f);
            curPosition.Scale(new Vector3(0, 1, 1));

            SlingshotPouch.Instance.transform.localPosition = curPosition;
            transform.localPosition = Vector3.zero;

            var fpCamera = SlingshotPouch.Instance.transform.parent.Find("Camera");
            var direction = SlingshotPouch.Instance.transform.parent.transform.position + SlingshotPouch.StartingPosition -
                            transform.position;

            if (direction != Vector3.zero)
            {
                fpCamera.rotation = Quaternion.LookRotation(direction);
            }
        }

        private void OnMouseUp()
        {
            if ((SlingshotPouch.StartingPosition - SlingshotPouch.Instance.transform.localPosition).magnitude < 0.6f)
            {
                SlingshotPouch.Instance.transform.localPosition = SlingshotPouch.StartingPosition;
                return;
            }
            SlingshotPouch.Instance.transform.parent.Find("Camera").rotation = Quaternion.LookRotation(new Vector3(0,0,-90));
            isShooting = true;
            Rigidbody.useGravity = true;
        }

        // Starts shooting process of bird;
        private void Shoot()
        {
            var slingshot = SlingshotPouch.Instance.transform.parent.transform;
            var force = slingshot.position + SlingshotPouch.StartingPosition - transform.position;
           
            Rigidbody.AddForce(force * 30);
            Velocity.Scale(new Vector3(1, 3, 1));
            SlingshotPouch.Instance.transform.position = transform.position;

            // If slingshot pouch is at orignal position, bird should start flying on it's own
            if (SlingshotPouch.Instance.transform.localPosition.z <= 0)
            {                
                transform.parent = null;
                SlingshotPouch.Instance.GetComponent<SlingshotPouch>().CurrentAmmo = null;
                ReloadAmmo();
                isShot = false;
            }
        }
        
#endregion

    }
}
