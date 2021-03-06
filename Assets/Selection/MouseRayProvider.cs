using UnityEngine;

namespace Selection
{
    class MouseRayProvider : MonoBehaviour, IRayProvider
    {
        public Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}