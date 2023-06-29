using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private GenerateTime generateTime;
    private float checkTimer= 0f;
    private float timeAlloted = 0f;
    private float timeWhenSpacePressed = 0f;
    private bool keypressed = false;
      

    private void Awake()
    {
        generateTime = GetComponent<GenerateTime>();
    }

    private void Start()
    {
        Debug.Log("Press the spacebar when you think the alloted time has passed");
        timeAlloted = generateTime.GenerateRandomTime();
        Debug.Log(timeAlloted + "seconds");
       

    }

    void Update()
    {
        checkTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeWhenSpacePressed = checkTimer;
            keypressed = true;
            if (keypressed == true)
            {
                Invoke("Result", 0f);
            }
        }
       
    }   
    void Result()
    {
        float timeDiff = timeAlloted - timeWhenSpacePressed;
        string result = "";
        if(Mathf.Abs(timeDiff) >= 10f)
        {
            result = " Dreadful.";
        }
        else if(Mathf.Abs(timeDiff) < 10f && Mathf.Abs(timeDiff) >= 3f)
        {
            result = " Ok, but could be better.";
        }
        else if(Mathf.Abs(timeDiff) < 3f)
        {
            result = " Amazing.";
        }
        Debug.Log("You waited for " + timeWhenSpacePressed + " seconds. That's " + Mathf.Abs(timeDiff) + " seconds off." + result);
        keypressed = false;

    }


}
