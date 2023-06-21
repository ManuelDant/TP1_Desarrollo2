using UnityEngine;

public interface ICameraRotation
{
    /// <summary>
    /// Rotate camera of player
    /// </summary>
    /// <param name="delta"> The position of input controller</param>
    void RotateCamera(Vector2 delta);
}