using UnityEngine;

namespace Build
{
    public class WallCreation : MonoBehaviour
    {
        [SerializeField] GameObject _wallPrefab;
        int num = 0;

        public GameObject CreateWall(Vector3 pos, Quaternion quat)
        {
            return Instantiate(_wallPrefab, pos, quat);
        }

    }
}