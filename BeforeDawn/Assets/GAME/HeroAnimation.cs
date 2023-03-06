using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform)
            {
                animator.SetBool("isRunning", true);
            }
            else if (!Input.GetMouseButtonDown(1))
            {
                animator.SetBool("isRunning", false);
            }
        }
}
