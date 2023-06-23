using UnityEngine;

/// <summary>
/// Weapon animator controller
/// </summary>
public class AnimatorWeaponManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    /// <summary>
    /// Activate weapon animation
    /// </summary>
    /// <param name="nameAnimator">Set name of animator</param>
    public void SetTriggerAnimator(string nameAnimator)
    {
        animator.SetTrigger(nameAnimator);
    }
}
