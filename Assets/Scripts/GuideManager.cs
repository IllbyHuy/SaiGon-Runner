using UnityEngine;

public class GuideManager : MonoBehaviour
{
    public Animator characterRightWalkAnimator;
    public Animator characterRunAnimator;
    public Animator characterJumpAnimator;
    public Animator characterLeftWalkAnimator; 

    void Start()
    {
        PlayRightWalkAnimation();
        PlayLeftWalkAnimation();
        PlayRunAnimation();
        PlayJumpAnimation();
    }
    void PlayRightWalkAnimation()
    {
        if (characterRightWalkAnimator != null)
        {
            characterRightWalkAnimator.SetFloat("Walk", 1f);
        }
    }
    void PlayLeftWalkAnimation()
    {
        if (characterLeftWalkAnimator != null)
        {
            characterLeftWalkAnimator.SetFloat("Walk", 1f);
        }
    }
    void PlayRunAnimation()
    {
        if (characterRunAnimator != null)
        {
            characterRunAnimator.SetBool("Run", true);
        }
    }
    void PlayJumpAnimation()
    {
        if (characterJumpAnimator != null)
        {
            characterJumpAnimator.SetBool("Jump", true);
        }
    }


}
