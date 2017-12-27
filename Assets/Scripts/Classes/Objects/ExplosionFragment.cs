using UnityEngine;

namespace Classes.Objects
{
    public class ExplosionFragment : MonoBehaviour
    {
        public Vector3 ParentBarrelPosition;
        private Vector3 Position
        {
            get { return transform.position; }
        }

        private int timeElapsed;

        // Update is called once per frame
        private void Update ()
        {
            ++timeElapsed;

            if ((ParentBarrelPosition - Position).magnitude > 20 || timeElapsed > 60)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
