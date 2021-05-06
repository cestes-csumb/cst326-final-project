using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
     private bool lockToYAxis;
     private float verticalInput;
     private float speedModifier;
     public Player player;
     public Sounds sm;
     private GameObject currentSurface;
     private Vector3 newLocation;
     private float verticalBoundary;
     private Rigidbody rb;
     private bool launched;
     private bool reachedGoal = false;
     private int treeCounter;
     private int boxCounter;
     private int jumpCounter;
     private int barrelCounter;
     // Start is called before the first frame update
     void Start()
     {
          notOnSurface();
          speedModifier = 10.0f;
          newLocation = player.transform.position;
          rb = this.GetComponent<Rigidbody>();
          launched = false;
          //https://answers.unity.com/questions/738991/ontriggerenter-being-called-multiple-times-in-succ.html
          treeCounter = 0;
          boxCounter = 0;
          jumpCounter = 0;
          barrelCounter = 0;
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
                    if ((verticalInput * speedModifier * Time.deltaTime) + player.transform.position.y <= 1.25)
                    {
                         newLocation = new Vector3(player.transform.position.x, 1.25f, player.transform.position.z);
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
                    //Debug.Log("am I on tree?");
               }
               /*TODO:
                * Figure out a smart way to push the player out of the tree object
                * 
                */
               if (Input.GetKeyDown(KeyCode.Space) && launched == false)
               {
                    barrelCounter = 0;
                    if (jumpCounter == 0) {
                         sm.playSound("jump");
                         jumpCounter++;
                    }
                    notOnSurface();
                    Vector3 slightAdjust = player.transform.position;
                    slightAdjust.x += 2.5f;
                    player.transform.position = slightAdjust;
                    Vector3 force = transform.up;
                    force.x += 1;
                    rb.AddForce(force * speedModifier, ForceMode.VelocityChange);
                    launched = true;
               }
          }
     }

     private void OnTriggerEnter(Collider other)
     {
          if (other.name.Contains("Floor"))
          {
               sm.playSound("death2");
               Debug.Log("You died");
               player.freezePlayer();
               //SceneManager.LoadScene("SampleScene");
          }
          if (other.name.Contains("Water")) {
               sm.playSound("death1");
               Debug.Log("You died");
               player.freezePlayer();
               this.GetComponent<MeshRenderer>().enabled = false;
               this.GetComponent<SphereCollider>().enabled = false;
               //SceneManager.LoadScene("SampleScene");
          }
          if(other.name.Contains("Tree"))
          {
               jumpCounter = 0;
               if (treeCounter == 0) { 
                    sm.playSound("land");
                    treeCounter++;
               }
               //Debug.Log("I'm on an object.");
               currentSurface = other.gameObject;
               lockToYAxis = true;
               //https://answers.unity.com/questions/120387/measuring-the-length-of-objects.html
               verticalBoundary = other.gameObject.GetComponent<Transform>().localScale.y;
               this.GetComponent<Rigidbody>().useGravity = false;
               this.GetComponent<Rigidbody>().isKinematic = true;
               Vector3 alignedPosition = transform.position;
               alignedPosition.x = other.transform.position.x;
               transform.position = alignedPosition;
               launched = false;
          }
          if (other.name.Contains("Crate")) {
               jumpCounter = 0;
               if (boxCounter == 0)
               {
                    sm.playSound("land");
                    treeCounter++;
               }
               currentSurface = other.gameObject;
               lockToYAxis = true;
               //https://answers.unity.com/questions/120387/measuring-the-length-of-objects.html
               verticalBoundary = other.gameObject.GetComponent<Transform>().localScale.y;
               float horizontalBoundary = other.gameObject.GetComponent<Transform>().localScale.x;
               //Debug.Log(horizontalBoundary);
               this.GetComponent<Rigidbody>().useGravity = false;
               this.GetComponent<Rigidbody>().isKinematic = true;
               Vector3 alignedPosition = transform.position;
               //yeah I dunno, this works I guess
               alignedPosition.x = other.transform.position.x + (horizontalBoundary / 2);
               transform.position = alignedPosition;
               launched = false;

          }
          if (other.name.Contains("Goal")){
               if (reachedGoal == false) {
                    sm.playSound("goal");
                    freezePlayer();
                    reachedGoal = true;
               }
          }
          //~53.5 put goal
          if (other.name.Contains("Barrel")) {
               if (barrelCounter == 0) {
                    sm.playSound("explosion");
                    barrelCounter++;
               }
          }
     }

     private void OnTriggerExit(Collider other)
     {
          if (other.name.Contains("Tree"))
          {
               this.GetComponent<Rigidbody>().useGravity = true;
               lockToYAxis = false;
          }
     }

     private void notOnSurface()
     {
          lockToYAxis = false;
          this.GetComponent<Rigidbody>().useGravity = true;
          this.GetComponent<Rigidbody>().isKinematic = false;
          boxCounter = 0;
          treeCounter = 0;
     }

     private void freezePlayer() {
          /*
          lockToYAxis = false;
          this.GetComponent<Rigidbody>().useGravity = false;
          this.GetComponent<Rigidbody>().isKinematic = true;
          launched = true;
          rb.velocity = Vector3.zero;
          */
          this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
     }
}
