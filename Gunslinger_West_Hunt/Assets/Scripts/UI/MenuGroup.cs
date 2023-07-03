using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGroup : MonoBehaviour
{
    [SerializeField] GameObject groupHolder;
    [SerializeField] GameObject nextGroup;
    [SerializeField] GameObject previousGroup;
    [HideInInspector] public bool hasNext;
    [HideInInspector] public bool hasPrevious;
    
    #region Animations
    public string enterAnimation;
    public string exitAnimation;
    private Animator animator;
    #endregion Animations

    private void Start()
    {
        animator = GetComponent<Animator>();
        if(nextGroup != null) { hasNext = true; }
        if(previousGroup != null) { hasPrevious = true; }
    }

    public GameObject GetCurrentGroup()
    {
        return groupHolder;
    }
    public GameObject GetNextGroup()
    {
        if(hasNext)
            return nextGroup;
        return groupHolder;

    }

    public GameObject GetPreviousGroup()
    {
        if(hasPrevious)
            return previousGroup;
        return groupHolder;

    }

    public void EnterAnimation()
    {
        if(animator == null) {  
            animator = GetComponent<Animator>();
        }
        Debug.Log($"Playing {enterAnimation}");
        animator.Play(enterAnimation);
    }

    public void ExitAnimation()
    {
        animator.Play(exitAnimation);
    }

}
