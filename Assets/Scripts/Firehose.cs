using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firehose : MonoBehaviour
{
    public GameObject waterFromHose;
     private Vector3 spawnerPosition;
     float timer;
    // Start is called before the first frame update
    void Start()
    {
          spawnerPosition = this.transform.position;
          timer = 1.0f;
    }

     // Update is called once per frame
     void Update()
     {
          timer -= Time.deltaTime;
          if (timer <= 0.0f) {
               shootWater(spawnerPosition, waterFromHose);
          }
    }

     void shootWater(Vector3 spawnerPosition, GameObject prefab) {
          GameObject projectile;
          Vector3 startingPosition = spawnerPosition;
          startingPosition.x -= 1.5f;
          projectile = Instantiate(prefab);
          projectile.transform.position = startingPosition;
          timer = 2.5f;
          return;
     }
}
