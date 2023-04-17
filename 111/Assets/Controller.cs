using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    public class Controller : MonoBehaviour
    {
        public Camera Camera;

        private RaycastHit hit;
        private NavMeshAgent Nav;
        // Start is called before the first frame update
        void Start()
        {
            Nav = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        Nav.SetDestination(hit.point);
                    }
                }
            }
        }
    }
}
