using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewArea : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;

    public LayerMask layerPlayer;
    public LayerMask layerObstacles;

    public GameObject target { get; set; }

    private void Start()
    {
        StartCoroutine(MakingPatrule());
    }

    IEnumerator MakingPatrule()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            FindingTarget();
            
        }
    }
    void FindingTarget()
    {
        Collider[] targetsInRadius = Physics.OverlapSphere(transform.position, viewRadius, layerPlayer);
        for(int i =0; i<targetsInRadius.Length; i++)
        {
            Vector3 targetPosition = targetsInRadius[i].transform.position;
            Vector3 direction = (targetPosition - transform.position).normalized;
            if(Vector3.Angle(transform.forward, direction) < viewAngle / 2)
            {
                float distanseBetwen = (targetPosition - transform.position).magnitude;
                if (!Physics.Raycast(transform.position, direction, distanseBetwen, layerObstacles))
                {
                    target = targetsInRadius[i].gameObject;
                    return;
                }
                else
                {
                    target = null;
                }
                
            }
        }
        
    }

}
