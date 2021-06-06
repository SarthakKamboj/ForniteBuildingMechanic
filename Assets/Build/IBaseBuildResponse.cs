using UnityEngine;

namespace Build
{
    interface IBaseBuildResponse
    {
        GameObject Build(Vector3 aimPosition);
        bool ShouldBuild(Vector3 aimPosition);
    }
}