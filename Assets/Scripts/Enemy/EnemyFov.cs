using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFov : MonoBehaviour
{
    public Transform player;
    public float viewRadius = 10f;
    public float viewAngle = 90f;
    public bool playerInFov;
    RaycastHit hitInfo;
    void Update()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        
        if (angleToPlayer < viewAngle / 2f)
        {
           
            // Ensure the player is within the view radius
            if (directionToPlayer.magnitude <= viewRadius)
            {
               
                // Perform raycast to check for obstacles
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, viewRadius))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // Player is in the FOV, you can trigger actions here (e.g., attack).
                        Debug.Log("Player in FOV!");
                        playerInFov = true;


                    }
                    

                }
                
            }
            
        }
        

    }
}
