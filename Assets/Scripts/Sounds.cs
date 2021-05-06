using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
     public AudioClip jump;
     public AudioClip land;
     public AudioClip death1;
     public AudioClip death2;
     public AudioClip goal;
     public AudioClip explosion;
     public AudioSource speaker;
    // Start is called before the first frame update
    void Start()
    {
          speaker = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void playSound(string n) {
          if (n.Equals("jump")) {
               speaker.clip = jump;
               speaker.PlayOneShot(speaker.clip);
          }
          if (n.Equals("land")) {
               speaker.clip = land;
               speaker.PlayOneShot(speaker.clip);
          }
          if (n.Equals("explosion"))
          {
               speaker.clip = explosion;
               speaker.PlayOneShot(speaker.clip);
          }
          if (n.Equals("goal"))
          {
               speaker.clip = goal;
               speaker.PlayOneShot(speaker.clip);
          }
          if (n.Equals("death1"))
          {
               speaker.clip = death1;
               speaker.PlayOneShot(speaker.clip);
          }
          if (n.Equals("death2"))
          {
               speaker.clip = death2;
               speaker.PlayOneShot(speaker.clip);
          }
     }
}
