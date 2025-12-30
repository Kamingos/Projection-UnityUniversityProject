using UnityEngine;

namespace Scripts
{
    public class FloorTypeModule : MonoBehaviour
    {
        [SerializeField] private FloorType type;
        public FloorType Type => type;
    }

    public enum FloorType
    {
        Wood,
        Concrete,
        Grass
    }
}
