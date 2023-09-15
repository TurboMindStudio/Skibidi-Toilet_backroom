using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoint;
    private NavMeshAgent enemyAi;
    private int randomPos;
    private Vector3 EnemyPos;
    //private Transform player;


    private void Start()
    {
        enemyAi = GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        EnemyPos = this.transform.position;

        randomPos = Random.Range(0, patrolPoint.Length);
        enemyAi.destination = patrolPoint[randomPos].position;
    }

    private void Update()
    {
     
        float distanceBeetweenPoints = Vector3.Distance(transform.position, patrolPoint[randomPos].position);
        //Debug.Log(distanceBeetweenPoints);
        if (distanceBeetweenPoints <= 5)
        {
            randomPos = Random.Range(0, patrolPoint.Length);
            enemyAi.destination = patrolPoint[randomPos].position;
        }
        
    }
}
