using System;
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

class HealthComponent
{
    public event Action<float, float> OnHealthChanged;

    private float _health;
    private float _maxHp;

    public float Health => _health;

    public void Init(float maxHp)
    {
        _maxHp = maxHp;
    }

    public void AddHealh(float value)
    {
        if (value <= 0) return;

        if (_health + value >= _maxHp)
            value = _maxHp - _health;

        OnHealthChanged?.Invoke(_health, _health + value);

        _health += value;
    }
}
