using UnityEngine;
using System.Collections;
using Cinemachine;

public class BirdBossMovement : MonoBehaviour {

    public GameObject bottomLeftObj, topRightObj;
    GameObject player;

    Vector2 bottomLeft, topRight, currentTarget, lastTarget;

    public bool traveling;
    bool towardsPlayer;

    public float minSpeed, maxSpeed;
    float speed;

    public int wallDetectCooldown = 0;

    void Start() {
        player = GameObject.Find("Player");
        bottomLeft = bottomLeftObj.transform.position;
        topRight = topRightObj.transform.position;
    }

    void FixedUpdate() {
        /*set en vector position randomly från inom en area.
        rör sig mot positionen
        när kommer till positionen set en ny position.
        upprepa
         */
        if(!traveling && !towardsPlayer){
            SetTarget(new Vector2(Random.Range(bottomLeft.x, topRight.x),
                Random.Range(bottomLeft.y, topRight.y)));
        }
        MoveTowardsTarget();
    }

    void MoveTowardsTarget() {
        if(towardsPlayer) {
            currentTarget = player.transform.position;
        }

        speed = Time.fixedDeltaTime * Mathf.Clamp(Mathf.Min(
            Vector2.Distance(transform.position, currentTarget),
            Vector2.Distance(transform.position, lastTarget)), minSpeed, maxSpeed);
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed);

        if(Vector2.Distance(transform.position, currentTarget) < 1f) {
            traveling = false;
        }
    }

    void SetTarget(Vector2 target) {
        lastTarget = currentTarget;  //maybe use = transform.position instead?
        currentTarget = target;
        traveling = true;
    }

    void ResetTarget() {
        currentTarget = transform.position;
        traveling = false;
    }

    void OnTriggerStay2D(Collider2D col) {
		switch(col.gameObject.layer) {
			case 18:
                wallDetectCooldown --;
                Debug.Log(wallDetectCooldown);
                if(!towardsPlayer && wallDetectCooldown <= 0) {
                    Debug.Log(wallDetectCooldown);
                    ResetTarget();
                    wallDetectCooldown = 100;
                    Debug.Log("walldetect run");
                }
				break;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        switch(col.gameObject.layer) {
			case 18:
                wallDetectCooldown = 0;
                Debug.Log("walldetect exit");
				break;
        }
    }
    
    public void MoveTowardsPlayer(bool value) {
        SetTarget(player.transform.position);
        towardsPlayer = value;
    }
}