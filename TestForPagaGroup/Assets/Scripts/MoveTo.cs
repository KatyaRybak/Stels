using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Vector3[] points;
    public Vector3 startPlayerPoint;
    public Transform goal;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }
}