using UnityEngine;

namespace Utils
{
    public class VectorUtils
    {
        public static Vector3 ProjectVectorOntoPlane(Vector3 vec, Vector3 normal)
        {
            Vector3 vecComponentAlignWithNormal = Vector3.Dot(vec, normal) / Mathf.Pow(normal.magnitude, 2) * normal;
            return (vec - vecComponentAlignWithNormal).normalized;
        }

        public static float GetVerticalAngle(Vector3 from, Vector3 to)
        {
            return Vector3.Angle(from, to) * Mathf.Sign(Vector3.Dot(to, Vector3.up));
        }

        public static Vector3 GetRelativePosition(Vector3 origin, Vector3 extents, Vector3 pos)
        {
            float x = (pos.x - origin.x) / extents.x;
            float y = (pos.y - origin.y) / extents.y;
            float z = (pos.z - origin.z) / extents.z;

            return new Vector3(x, y, z);
        }
    }
}