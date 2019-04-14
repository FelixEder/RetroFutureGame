using UnityEngine;
using System.Collections;
using Cinemachine;

public class BirdBoss : MonoBehaviour {
    BirdBossMovement move;
    void Start() {
        move = GetComponent<BirdBossMovement>();
        InvokeRepeating("Beak", 1f, 10f);
    }

    void Update() {

    }

    void Beak() {
        StartCoroutine("BeakAttack");
    }

    IEnumerator BeakAttack() {
        Debug.Log("attack start");
        move.MoveTowardsPlayer(true); //call move towards player
        while(move.traveling) { //wait for arrival
            yield return 0;
        }
        //execute attack
       move.MoveTowardsPlayer(false); //retur to regular movement
       Debug.Log("exit attack");
    }
}