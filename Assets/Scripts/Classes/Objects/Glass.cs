using System.Collections.Generic;
using UnityEngine;

namespace Classes.Objects
{
    internal class Glass : Blocks
    {
        private static readonly Vector3 Size = new Vector3(1, 0.3f, 1);
        private static readonly float[] Position = { Size.y + 0.05f, 0, -Size.y - 0.05f };

        public override void Break()
        {
            if (transform.localScale.y >= 3)
            {
                for (var i = 0; i < 3; i++)
                {
                    GenerateSmallGlass(Size, i);
                }

                Destroy(gameObject);
                return;
            }
            
            Destroy(gameObject, 2f / impactRate);
        }

        private void GenerateSmallGlass(Vector3 size, int i)
        {
            var glass = Instantiate(gameObject).transform;
            glass.parent = transform;
            glass.localScale = size;
            glass.localPosition = new Vector3(0, Position[i], 0);
            glass.localRotation = Quaternion.identity;
            glass.parent = null;
        }
    }
}
