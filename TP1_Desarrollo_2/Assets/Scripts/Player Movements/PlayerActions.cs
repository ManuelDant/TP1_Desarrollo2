using UnityEngine;
/// <summary>
/// Class of All Actions from the Player.
/// </summary>
public interface IPlayerActions
{
    /// <summary>
    /// The method performs the movement of the player with a parameter of Vector2
    /// </summary>
    void Move(Vector2 direction);
    /// <summary>
    /// The method sets the sprint velocity of the player through a parameter boolean where false is not sprint and true is sprint.
    /// </summary>
    void SetSprint(bool isSprinting);
}
