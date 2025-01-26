// Knockbackable.cs
using UnityEngine;

public interface IKnockbackable
{
    void ApplyKnockback(Vector3 direction, float force, float duration);
}
