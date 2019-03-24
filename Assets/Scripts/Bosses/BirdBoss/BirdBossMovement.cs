using UnityEngine;
using System.Collections;
using Cinemachine;

public class BirdBossMovement : MonoBehaviour {

    public GameObject bottomLeftObj, topRightObj;

    Vector2 bottomLeft, topRight, currentTarget, lastTarget;
    bool traveling;

    public float minSpeed, maxSpeed;
    float speed;

    void Start() {
        bottomLeft = bottomLeftObj.transform.position;
        topRight = topRightObj.transform.position;
    }

    void FixedUpdate() {
        /*set en vector position randomly från inom en area.
        rör sig mot positionen
        när kommer till positionen set en ny position.
        upprepa
         */
        if(!traveling){
            lastTarget = currentTarget;
            currentTarget = new Vector2(Random.Range(bottomLeft.x, topRight.x),
                Random.Range(bottomLeft.y, topRight.y));
                traveling = true;
        }

        speed = Time.fixedDeltaTime * Mathf.Clamp(Mathf.Min(
            Vector2.Distance(transform.position, currentTarget),
            Vector2.Distance(transform.position, lastTarget)), minSpeed, maxSpeed);
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed);

        if(Vector2.Distance(transform.position, currentTarget) < 1f) {
            traveling = false;
        }
    }
}