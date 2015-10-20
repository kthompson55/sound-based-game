using UnityEngine;
using System.Collections;

public class EnemyFieldOfView : MonoBehaviour {

    private Enemy attachedEnemy;
    private bool timerOn;
    private float timer;
    private float lastUpdateTime;
    public Light leftEye;
    public Light rightEye;
    public Color alertedColor;
    public GameObject StatusLightObject;
    //private Light[] statusLights;

    private bool canUpdatePosition;

	void Start () {
        canUpdatePosition = true;
        attachedEnemy = GetComponentInParent<Enemy>();
        if (!attachedEnemy) {
            Debug.LogError("This field of view doesn't have a parent with an enemy script; fov name: " + name);
        }
        if (StatusLightObject) {
            //statusLights = StatusLightObject.GetComponentsInChildren<Light>();
        }
	}

    void Update() {
        if (timerOn) {
            if ((System.DateTime.Now.Ticks * 10000) - lastUpdateTime > (attachedEnemy.reactionInterval / 1000)) {
                //Debug.Log("can update position again");
                timerOn = false;
                canUpdatePosition = true;
            }
        }
        leftEye.transform.LookAt(attachedEnemy.GetTargetPosition());
        rightEye.transform.LookAt(attachedEnemy.GetTargetPosition());
        //if (attachedEnemy.IsAlerted() && !StatusLightObject.activeSelf)
        //{
        //    StatusLightObject.SetActive(true);
        //}
        //else if(StatusLightObject.activeSelf){
        //    StatusLightObject.SetActive(false);
        //}
    }

    void OnTriggerEnter(Collider col)
    {
         if ( canUpdatePosition && col.gameObject.CompareTag("Player") ){
            UpdatePlayerLastKnownPosition(col);
        }
    }

    void OnTriggerStay(Collider col) {
        if ( canUpdatePosition && col.gameObject.CompareTag("Player") ){
            UpdatePlayerLastKnownPosition(col);
        }
    }

    void UpdatePlayerLastKnownPosition(Collider col)
    {
        canUpdatePosition = false;
        timerOn = true;
        
        Debug.Log("Enemy sees player!!");
        attachedEnemy.ChasePlayer(col);
        lastUpdateTime = System.DateTime.Now.Ticks * 10000;
    }
}
