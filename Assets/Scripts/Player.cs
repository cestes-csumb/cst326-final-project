using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     private bool lockToYAxis;
     private float verticalInput;
     private float speedModifier;
     public Player player;
     private GameObject currentSurface;
     private Vector3 newLocation;
     private float verticalBoundary;
     private Rigidbody rb;
     // Start is called before the first frame update
     void Start()
     {
          lockToYAxis = true;
          speedModifier = 5.0f;
          newLocation = player.transform.position;
          rb = this.GetComponent<Rigidbody>();
     }

     // Update is called once per frame
     void Update()
     {
          //when we're on an object
          if (currentSurface != null)
          {
               if (lockToYAxis == true)
               {
                    verticalInput = Input.GetAxis("Vertical");
                    //while on the tree, can't go above the tree
                    if ((verticalInput * speedModifier * Time.deltaTime) + player.transform.position.y > verticalBoundary)
                    {
                         newLocation = new Vector3(player.transform.position.x, verticalBoundary, player.transform.position.z);
                         player.transform.position = newLocation;
                    }
                    //can't go below 0 because you should never be able to go that low
                    if ((verticalInput * speedModifier * Time.deltaTime) + player.transform.position.y <= 0.0)
                    {
                         newLocation = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
                         player.transform.position = newLocation;
                    }
                    //otherwise just update position
                    else
                    {
                         newLocation = new Vector3(0, (verticalInput * speedModifier * Time.deltaTime), 0);
                         player.transform.Translate(newLocation);
                    }
               }
               else
               {
                    notOnSurface();
               }
               /*TODO:
                * Figure out a smart way to push the player out of the tree object
                * 
                */
               if (Input.GetKeyDown(KeyCode.Space))
               {
                    lockToYAxis = false;
                    Vector3 force = transform.up;
                    force.x += 3;
                    rb.AddForce(force * speedModifier, ForceMode.VelocityChange);
               }
          }
     }

     private void OnTriggerEnter(Collider other)
     {
          if (other.name.Contains("Tree"))
          {
               Debug.Log("I'm on a tree");
               currentSurface = other.gameObject;
               //https://answers.unity.com/questions/120387/measuring-the-length-of-objects.html
               verticalBoundary = other.gameObject.GetComponent<Transform>().localScale.y;
          }
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.name.Contains("Tree"))
          {
               this.GetComponent<Rigidbody>().useGravity = true;
               lockToYAxis = false;
          }

          if (other.name.Contains("Floor"))
          {
               Destroy(this.gameObject);
          }
     }

     private void notOnSurface()
     {
          lockToYAxis = false;
          this.GetComponent<Rigidbody>().useGravity = true;
          this.GetComponent<Rigidbody>().isKinematic = false;
     }
}
