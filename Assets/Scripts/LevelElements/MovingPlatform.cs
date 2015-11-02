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
    public bool startAtMin;

    private float timer;
    private bool ascending;

    void Start() {
        transform.localPosition = new Vector3(transform.localPosition.x, ((startAtMin) ? yMin : yMax), transform.localPosition.z);
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
                    currY += speed * Time.deltaTime;
                    //Debug.Log("currY: " + currY + ", max: " + yMax);
                    if (yMax - currY < 1)
                    {
                        paused = true;
                        ascending = false;
                    }
                }
                else
                {
                    currY -= speed * Time.deltaTime;
                    //Debug.Log("currY: " + currY + ", min: " + yMin);

                    if (currY - yMin < 1)
                    {
                        paused = true;
                        ascending = true;
                    }
                }
                transform.localPosition = new Vector3(transform.localPosition.x, currY, transform.localPosition.z);
            }
        }
    }
}
