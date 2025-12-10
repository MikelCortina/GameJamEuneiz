using UnityEngine;

public class SetIdle : MonoBehaviour
{
    public Animator animator;
    public AnimationClip clip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIdle1()
    {

        animator.Play(clip.name);
    }
}
