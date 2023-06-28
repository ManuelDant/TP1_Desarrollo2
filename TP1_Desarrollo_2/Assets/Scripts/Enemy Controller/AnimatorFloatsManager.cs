using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager floats of animator enemy
/// </summary>
public class AnimatorFloatsManager : MonoBehaviour
{
    [SerializeField] private string AnimatorNamePosX = "velX";
    [SerializeField] private string AnimatorNamePosY = "velY";

    /// <summary>
    /// Set the float for the enemy animator
    /// </summary>
    /// <param name="x"> Position x float</param>
    /// <param name="y"> Position y float</param>
    /// <param name="animator"> Animator of enemy</param>
    public void SetFloats(float x, float y, Animator animator)
    {
        animator.SetFloat(AnimatorNamePosX, x);
        animator.SetFloat(AnimatorNamePosY, y);
    }
}
