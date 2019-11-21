using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Patrol : MonoBehaviour
{

    public Vector3[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    ViewArea view;
    Coroutine waiting;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;
        agent.destination = points[destPoint];
        agent.speed = 0.2f;
        gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.yellow;
        destPoint = (destPoint + 1) % points.Length;

    }


    void Update()
    {
        view = GetComponent<ViewArea>();
        if (view.target != null)
        {
            agent.destination = view.target.transform.position;
            agent.speed = 0.7f;
            gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.red;
            if (waiting != null)
            {
                StopCoroutine(waiting);
                waiting = null;
            }

        }
        else if(agent.remainingDistance < 0.1f)
        {
            GotoNextPoint();
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MoveTo playerMoving = collision.gameObject.GetComponent<MoveTo>();
            Vector3 playerNextPosition;
            int counterTrying=0;
            do
            {
                playerNextPosition = playerMoving.points[Random.Range(0, playerMoving.points.Length)];
                counterTrying++;
            }
            while (playerNextPosition == collision.gameObject.transform.position||counterTrying<=10);

            collision.gameObject.transform.position = playerNextPosition;
            playerMoving.agent.destination = playerNextPosition;
            view.target= null;
            GotoNextPoint();
        }
    }

}