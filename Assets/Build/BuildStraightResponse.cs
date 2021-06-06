using UnityEngine;
using Utils;

namespace Build
{
    class BuildStraightResponse : MonoBehaviour, IBuildResponse
    {
        [SerializeField] GameObject _wallPrefab;
        [Min(0)]
        [SerializeField] int _priority = 1;
        [SerializeField] Transform _player;

        IValidBuildPosProvider _validBuildPosProvider;
        Vector3 _wallExtents;
        Vector3 _newBuildPos;
        WallCreation _wallCreation;

        void Awake()
        {
            _validBuildPosProvider = GetComponent<IValidBuildPosProvider>();
            _wallCreation = GetComponent<WallCreation>();
            _wallExtents = GetWallExtents();
        }

        Vector3 GetWallExtents()
        {
            GameObject tempWall = Instantiate(_wallPrefab);
            Vector3 wallExtents = tempWall.GetComponent<Renderer>().bounds.extents;
            Destroy(tempWall);
            return wallExtents;
        }

        public bool ShouldBuild(Vector3 baseWallPosition, Vector3 buildDir)
        {
            _newBuildPos = GetBuildPos(baseWallPosition, buildDir);

            Vector3 playerForwardRounded = GetPlayerRoundedForward();

            if (playerForwardRounded == Vector3.zero)
            {
                return false;
            }

            Vector3 buildDirForward = VectorUtils.ProjectVectorOntoPlane(buildDir, Vector3.up);

            float angle = Vector3.Angle(playerForwardRounded, buildDirForward);
            bool buildToRightOrLeft = angle == 90f;

            bool newPosAvailable = _validBuildPosProvider.IsValidPos(_newBuildPos);
            bool shouldBuild = Input.GetMouseButton(0) && newPosAvailable && buildToRightOrLeft;

            return shouldBuild;
        }

        public GameObject Build(Transform baseWall, Vector3 buildDir)
        {
            return _wallCreation.CreateWall(_newBuildPos, baseWall.rotation);
        }

        Vector3 GetPlayerRoundedForward()
        {
            float yRot = _player.eulerAngles.y;
            float roundedYRot = AngleUtils.RoundToNearestRightAngle(yRot, 30f);

            if (roundedYRot == -1f)
            {
                return Vector3.zero;
            }

            return Quaternion.Euler(new Vector3(0f, roundedYRot, 0f)) * Vector3.forward;
        }

        Vector3 GetBuildPos(Vector3 baseWallPosition, Vector3 buildDir)
        {
            Vector3 buildDirForward = VectorUtils.ProjectVectorOntoPlane(buildDir, Vector3.up);

            float angle = VectorUtils.GetVerticalAngle(buildDirForward, buildDir);
            Vector3 yChange = new Vector3(0f, Mathf.Sin(angle * Mathf.Deg2Rad) * _wallExtents.z * 2, 0f);
            Vector3 xChange = Mathf.Cos(angle * Mathf.Deg2Rad) * _wallExtents.z * buildDirForward * 2;

            return baseWallPosition + yChange + xChange;
        }

        public int GetBuildPriority()
        {
            return _priority;
        }
    }
}