using UnityEngine;

namespace Classes.Objects
{
    internal class Glass : Blocks
    {

        public override void Break()
        {
            var scale = transform.localScale;
            if (scale.y >= 3)
            {
                var glass = Instantiate(gameObject);
                glass.transform.parent = transform;
                glass.transform.localScale = new Vector3(1, 0.3f, 1);
                glass.transform.localPosition = new Vector3(0, glass.transform.localScale.y + 0.05f,0);
                glass.transform.localRotation = Quaternion.identity;
                glass.transform.parent = null;

                glass = Instantiate(gameObject);
                glass.transform.parent = transform;
                glass.transform.localScale = new Vector3(1, 0.3f, 1);
                glass.transform.localPosition = new Vector3(0,0,0);
                glass.transform.localRotation = Quaternion.identity;
                glass.transform.parent = null;

                glass = Instantiate(gameObject);
                glass.transform.parent = transform;
                glass.transform.localScale = new Vector3(1, 0.3f, 1);
                glass.transform.localPosition = new Vector3(0, -glass.transform.localScale.y - 0.05f, 0);
                glass.transform.localRotation = Quaternion.identity;
                glass.transform.parent = null;


                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(CoUpdate());
            }
        }
    }
}
