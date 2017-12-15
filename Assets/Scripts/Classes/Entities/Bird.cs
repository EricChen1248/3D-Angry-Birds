using UnityEngine;

namespace Classes.Entities
{
    public class Bird: MonoBehaviour
    {
        public Vector3 Speed = new Vector3(10,10,10);
        public float Mass = 1f;        

        protected Rigidbody Rigidbody { get; set; }

        protected int ExplodeTime = 200;
        protected Vector3 Velocity
        {
            get { return Rigidbody.velocity; }
        }

        // Use this for initialization
        protected void Start ()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Rigidbody.mass = Mass;
        }
	
        // Update is called once per frame
        protected virtual void FixedUpdate ()
        {
            FixedUpdateFunctions();
        }

        protected void FixedUpdateFunctions()
        {
            if (IsShooting && IsShot)
            {
                Shoot();
            }

            if (Rigidbody.useGravity)
            {
                Rigidbody.AddForce(-Physics.gravity * 0.6f);
            }

            if (IsShot) return;
            if (!(Velocity.magnitude <= 0.3f)) return;
            if (ExplodeTime <= 0)
            {
                StartShrink();
            }
            --ExplodeTime;
        }

        protected void StartShrink()
        {
            gameObject.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            if (gameObject.transform.localScale.x < 0.1f)
            {
                Destroy(gameObject);
            }
        }
        

        protected void ReloadAmmo()
        {
            if (SlingshotPouch.Instance.GetComponent<SlingshotPouch>().CurrentAmmo != null)
                return;

            SlingshotPouch.Instance.GetComponent<SlingshotPouch>().Reset();
        }

#region Shooting and Aiming
    
        protected Vector3 ScreenPoint;
        protected Vector3 Offset;
        protected bool IsShooting = false;
        protected bool IsShot = true;

        protected void OnMouseDown()
        {
            if (IsShooting)
            {
                return;
            }
            ScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));

        }

        protected void OnMouseDrag()
        {
            if (IsShooting)
            {
                return;
            }

            var curPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z)) 
                                + Offset;

            SlingshotPouch.Instance.transform.position = curPosition;

            // Clamping slingshot boundaries
            var locPos = SlingshotPouch.Instance.transform.localPosition;
            locPos.z = Mathf.Min(Mathf.Max(locPos.z, 0), 10);
            locPos.y = Mathf.Max(locPos.y, -0.5f);
            locPos.x = 0;
            SlingshotPouch.Instance.transform.localPosition = locPos;

            transform.localPosition = Vector3.zero;

            var fpCamera = SlingshotPouch.Instance.transform.parent.Find("Camera");
            var direction = SlingshotPouch.Instance.transform.parent.transform.position + SlingshotPouch.StartingPosition -
                            transform.position;

            if (direction != Vector3.zero)
            {
                fpCamera.rotation = Quaternion.LookRotation(direction);
            }
        }

        protected void OnMouseUp()
        {
            if ((SlingshotPouch.StartingPosition - SlingshotPouch.Instance.transform.localPosition).magnitude < 0.6f)
            {
                SlingshotPouch.Instance.transform.localPosition = SlingshotPouch.StartingPosition;
                return;
            }
            SlingshotPouch.Instance.transform.parent.Find("Camera").rotation = 
                Quaternion.LookRotation(SlingshotPouch.Instance.transform.parent.transform.position + SlingshotPouch.StartingPosition - transform.position);
            IsShooting = true;
            Rigidbody.useGravity = true;
        }

        // Starts shooting process of bird;
        protected void Shoot()
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
                IsShot = false;
            }
        }
        
#endregion

    }
}
