using UnityEngine;

namespace Build
{
    interface IValidBuildPosProvider
    {
        bool IsValidPos(Vector3 position);
    }
}