using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
     float timer;
     bool startBurning;
    // Start is called before the first frame update
    void Start()
    {
          timer = 5.0f;
          startBurning = false;
    }

    // Update is called once per frame
    void Update()
    {
          if (startBurning == true) {
               //Debug.Log("burning...");
               timer -= Time.deltaTime;
               //Debug.Log(timer);
          }
          if (timer <= 0.0f) {
               Destroy(this.gameObject);
          }
    }

     private void OnTriggerEnter(Collider other)
     {
          if (other.name.Equals("Player")) {
               Debug.Log("player is burning me to a crisp");
               startBurning = true;
          }
     }
}
