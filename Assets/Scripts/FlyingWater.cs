using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingWater : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
          rb = this.GetComponent<Rigidbody>();
          Vector3 force = Vector3.right;
          force.y -= 1;
          rb.AddForce(force * -10.0f, ForceMode.VelocityChange);
     }

    // Update is called once per frame
    void Update()
    {
          if (transform.position.y <= -1.5f) {
               Destroy(this.gameObject);
          }
    }

     private void OnTriggerEnter(Collider other)
     {
          if (other.name.Equals("Player")) {
               //Debug.Log("collided with player");
          }
     }
}
