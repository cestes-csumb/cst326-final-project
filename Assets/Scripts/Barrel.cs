using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
     {
          if (other.name.Contains("Player")){
               //not doing anything...
               //TODO: fix me
               Vector3 force = transform.up;
               Rigidbody rb = other.GetComponent<Rigidbody>();
               rb.AddForce(force * 40.0f, ForceMode.Impulse);
               //Debug.Log("Player hit barrel");
          }
     }
}
