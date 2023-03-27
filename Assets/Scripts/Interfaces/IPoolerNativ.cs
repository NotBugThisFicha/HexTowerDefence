
using UnityEngine;

public interface IPoolerNativ
{
    public void Enable();
    public void Disable();
    public void SetTransform(Transform transform);
    public void SetStartPosition(Vector3 position);
    public void SetStartRotation(Quaternion quaternion);
}
