using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public bool enableYMovement;
    public float yMin;
    public float yMax;

    public float speed = 0.25f;
    public float pauseDuration = 2f;
    public bool paused = false;
    public bool ascending = false;

    private float pauseStartTime;
    private float timer;
    // Update is called once per frame
    float movement;
    void Start() {
        if (ascending)
        {
            movement = yMax - transform.localPosition.y;
        }
        else {
            movement = transform.localPosition.y - yMin;
        }
    }

    void Update()
    {
        if (enableYMovement)
        {
            float currY = transform.localPosition.y;

            if (paused)
            {
                timer += Time.deltaTime;
                if (timer > pauseDuration) {
                    paused = false;
                    timer = 0;
                }
            }
            else
            {
                if (ascending)
                {
                    currY += movement * speed * Time.deltaTime;
                    //Debug.Log("currY: " + currY + ", max: " + yMax);
                    if (yMax - currY < 1)
                    {
                        pauseStartTime = System.DateTime.Now.Ticks * 10000;
                        //currY = yMax;
                        paused = true;
                        ascending = false;
                        movement = currY - yMin;
                    }
                }
                else
                {
                    currY -= movement * speed * Time.deltaTime;
                    //Debug.Log("currY: " + currY + ", min: " + yMin);

                    if (currY - yMin < 1)
                    {
                        pauseStartTime = System.DateTime.Now.Ticks * 10000;
                        //currY = yMin;
                        paused = true;
                        ascending = true;
                        movement = yMax - currY;
                    }
                }
                transform.localPosition = new Vector3(transform.localPosition.x, currY, transform.localPosition.z);
            }
        }
    }
}
