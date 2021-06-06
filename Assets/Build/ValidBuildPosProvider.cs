using UnityEngine;

namespace Build
{
    class ValidBuildPosProvider : MonoBehaviour, IValidBuildPosProvider
    {

        [SerializeField] LayerMask wallLayerMask;

        public bool IsValidPos(Vector3 position)
        {
            // foreach (Collider col in Physics.OverlapSphere(position, 0.1f, wallLayerMask))
            // {
            //     if (col.tag == "Snow")
            //     {
            //         return false;
            //     }
            // }
            // return true;
            return Physics.OverlapSphere(position, 0.1f, wallLayerMask).Length == 0;
        }
    }
}