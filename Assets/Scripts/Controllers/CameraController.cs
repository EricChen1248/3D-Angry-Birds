using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {

        // Use this for initialization
        public void Start()
        {

        }

        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled
        private void FixedUpdate()
        {

            var distance = Input.GetAxisRaw("Mouse ScrollWheel") * 3;
            var vertical = Input.GetAxisRaw("Vertical") / 10;
            var horizontal = Input.GetAxisRaw("Horizontal") / 10;
            transform.localPosition += new Vector3(distance, vertical, horizontal);
        }



    }
}
