using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
     public TimeHandler th;
     private bool goalHit;
    // Start is called before the first frame update
    void Start()
    {
          goalHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
     {
          if (other.name.Contains("Player")) {
               goalHit = true;
               //Debug.Log("you win!");
               //Debug.Log("Finished level in " + th.getCompletionTime() + " seconds.");
          }
     }

     public bool getGoalHit() {
          return this.goalHit;
     }
}
