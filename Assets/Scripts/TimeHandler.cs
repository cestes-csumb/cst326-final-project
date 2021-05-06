using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
     private float timer;
     private float timerValueForDisplay;
     private float completionTime;
     private bool timerPause;
     private bool notPaused;
     public TMPro.TextMeshProUGUI timeText;
     public TMPro.TextMeshProUGUI winMessage;
     public Goal goal;
     // Start is called before the first frame update
     void Start()
    {
          timer = 100f;
          completionTime = timer;
          timerPause = false;
          notPaused = true;
          winMessage.SetText("");
    }

    // Update is called once per frame
    void Update()
    {
          if(timerPause == false) {
               timer -= Time.deltaTime;
          }
          if (timer <= 0)
          {
               timeText.text = "Game Over!";
          }
          else {
               if (timer < 100)
               {
                    timeText.text = timer.ToString().Remove(2);
               }
               else {
                    timeText.text = timer.ToString().Remove(3);
               }
          }
          if (goal.getGoalHit() && notPaused) {
               float finalTime = getCompletionTime();
               winMessage.SetText("Congratulations!\nCompletion Time: " + finalTime.ToString() + " s");
               notPaused = false;
          }
    }

     public float getCompletionTime() {
          completionTime -= timer;
          timerPause = true;
          return completionTime;
     }
}
