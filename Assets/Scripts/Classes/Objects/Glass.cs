using UnityEngine;

namespace Classes.Objects
{
    internal class Glass : Blocks
    {
        public GameObject GlassPrefab;

        public override void Break(float impactRate)
        {
            var scale = transform.localScale;
            if (scale.y >= 3)
            {
                var glass = Instantiate(GlassPrefab);
                glass.transform.position = transform.position;
                glass.transform.localPosition += new Vector3(0, scale.y / 3 + 0.05f, 0);
                glass.transform.localScale = new Vector3(scale.x, scale.y / 3, scale.z);

                glass = Instantiate(GlassPrefab);
                glass.transform.position = transform.position;
                glass.transform.localScale = new Vector3(scale.x, scale.y / 3, scale.z);

                glass = Instantiate(GlassPrefab);
                glass.transform.position = transform.position;
                glass.transform.localPosition -= new Vector3(0, scale.y / 3 + 0.05f, 0);
                glass.transform.localScale = new Vector3(scale.x, scale.y / 3, scale.z);


                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(CoUpdate(impactRate));
            }
        }
    }
}
