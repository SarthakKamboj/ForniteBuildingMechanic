using UnityEngine;

namespace Build
{
    public interface IBuildResponse
    {
        bool ShouldBuild(Vector3 baseWallPosition, Vector3 buildDir);
        GameObject Build(Transform baseWall, Vector3 buildDir);
        int GetBuildPriority();
    }
}