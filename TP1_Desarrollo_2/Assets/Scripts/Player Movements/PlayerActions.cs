using UnityEngine;

public interface IPlayerActions
{
    void Move(Vector2 direction);
    void SetSprint(bool isSprinting);
}
