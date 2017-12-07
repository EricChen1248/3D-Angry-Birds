﻿using UnityEngine;

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
            var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            var curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            // Making sure ammo can't go infront of slingshot
            if ((curPosition - GameObject.FindGameObjectWithTag("Slingshot").transform.position +
                 SlingshotPouch.StartingPosition).z <= 0)
            {
                curPosition.z = GameObject.FindGameObjectWithTag("Slingshot").transform.position.z +
                                SlingshotPouch.StartingPosition.z;
            }
            SlingshotPouch.Self.transform.position = curPosition;
            SlingshotPouch.Self.transform.localPosition.Scale(new Vector3(0, 1, 1));
            transform.position = SlingshotPouch.Self.transform.position;

            var fpCamera = SlingshotPouch.Self.transform.parent.Find("Camera");
            var direction = SlingshotPouch.Self.transform.parent.transform.position + SlingshotPouch.StartingPosition -
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
            if (SlingshotPouch.Self.GetComponent<SlingshotPouch>().CurrentAmmo != null)
                return;

            SlingshotPouch.Self.GetComponent<SlingshotPouch>().Reset();



        }


        // Starts shooting process of bird;
        private void Shoot()
        {
            var slingshot = SlingshotPouch.Self.transform.parent.transform;
            var force = slingshot.position + SlingshotPouch.StartingPosition - transform.position;
            //var force = SlingshotPouch.StartingPosition - SlingshotPouch.Self.transform.localPosition;
            Rigidbody.AddForce(force * 30);
            SlingshotPouch.Self.transform.position = transform.position;
            if (SlingshotPouch.Self.transform.localPosition.z <= 0)
            {                
                transform.parent = null;
                SlingshotPouch.Self.GetComponent<SlingshotPouch>().CurrentAmmo = null;
                ReloadAmmo();
                isShooting = true;
            }
        }
    }
}