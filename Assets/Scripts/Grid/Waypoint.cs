using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower ballista;
    
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    void OnMouseDown() {
        if (isPlaceable){
            SpawnBallista();
        }
        
    }

    void SpawnBallista()
    {
        if (ballista)
        {
            if (ballista.CreateTower(ballista, transform.position))
            {
                isPlaceable = false;
            }
        }
    }
}
