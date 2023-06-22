using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorWeaponManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetTriggerAnimator(string nameAnimator)
    {
        animator.SetTrigger(nameAnimator);
    }
}
