using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerClickController : MonoBehaviour
{
    public LayerMask clickOn;
    private RaycastHit hitInfo;
    private Ray ray;
    private NavMeshAgent Nav;
    private Animator animator;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Nav = GetComponent<NavMeshAgent>();
        currentPosition = transform.position;
    }
    // Update is called once per frame

    void Update()
    {
        // 0 for letf click
        bool click1 = Input.GetMouseButtonDown(1);
        if (click1)
        {
            animator.SetBool("isRunning", true);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 100, clickOn))
            {
                Nav.SetDestination(hitInfo.point);
            }
        }

        else if (Nav.remainingDistance <= 0) 
        {
            animator.SetBool("isRunning", false);
        }
    }
}