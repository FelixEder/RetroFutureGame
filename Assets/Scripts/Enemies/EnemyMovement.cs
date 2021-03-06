﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowRange {
    public float vertical, up, down;
}

[RequireComponent(typeof(Animator))]
public class EnemyMovement : MonoBehaviour {
    public float followSpeed = 3;
    public FollowRange followRange;
    public bool canSeeBehind;
    [Space(10)]
    public float wanderSpeed;
    public float wanderDist = 5, timeBeforeWander;
    [Space(10)]
    public Vector2 groundcheckPos = Vector2.one;
    public Vector2 wallcheckPos = Vector2.one;
    [Space(10)]
    public LayerMask groundMask = 262656;
    public LayerMask wallMask = 268763136, raycastMask = 268697856;

    bool grounded, wallcheck;
    public int wanderDir = -1;
    float startPos;
    public RaycastHit2D raycastHit;
    Rigidbody2D rb2D;
    GameObject player;

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 1, 0, 0.5f); //yellow
        Gizmos.DrawCube(transform.position - new Vector3(0, groundcheckPos.y / 2, 0), new Vector3(groundcheckPos.x, groundcheckPos.y, 0));
        Gizmos.DrawCube(transform.position - new Vector3(wallcheckPos.x * -wanderDir, 0, 0), new Vector3(0.1f, wallcheckPos.y, 0));
        Gizmos.color = new Color(1, 1, 1, 0.3f); //white
        Gizmos.DrawCube(transform.position + new Vector3(0, (followRange.up - followRange.down) / 2, 0), new Vector3(followRange.vertical * 2, followRange.up + followRange.down, 0));
    }

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        startPos = transform.position.x;
        wanderDir = 0;
        if(timeBeforeWander > 0)
            Invoke("StartWander", timeBeforeWander);
    }

    void Update() {
        raycastHit = Physics2D.Raycast(transform.position, RaycastDirection(), followRange.vertical, raycastMask);
        grounded = Physics2D.OverlapBox(transform.position - new Vector3(0, groundcheckPos.y / 2, 0), new Vector3(groundcheckPos.x, groundcheckPos.y, 0), 0, groundMask);
        wallcheck = Physics2D.OverlapBox(transform.position - new Vector3(wallcheckPos.x * -wanderDir, 0, 0), new Vector3(0.1f, wallcheckPos.y, 0), 0, wallMask);

        if(wallcheck) {
            wanderDir *= -1;
        }

        SpriteFacing();
    }

    void FixedUpdate() {
        if(grounded) {
            if(raycastHit) {
                if(raycastHit.collider.name == "Player") {
                    rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < followSpeed ? new Vector2(followSpeed * 0.1f * Mathf.Sign(player.transform.position.x - transform.position.x), 0) : Vector2.zero;
                    wanderDir = (int)Mathf.Sign(player.transform.position.x - transform.position.x);
                }
                else if(!(wanderDist == 0 && Mathf.Abs(transform.position.x - startPos) < 0.5f))
                    rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < wanderSpeed ? new Vector2(wanderSpeed * 0.1f * wanderDir, 0) : Vector2.zero;
            }
            else if(!(wanderDist == 0 && Mathf.Abs(transform.position.x - startPos) < 0.5f))
                rb2D.velocity += Mathf.Abs(rb2D.velocity.x) < wanderSpeed ? new Vector2(wanderSpeed * 0.1f * wanderDir, 0) : Vector2.zero;
        }
        if(rb2D.velocity.y < -15)
            rb2D.velocity = new Vector2(rb2D.velocity.x, -15);
    }

    Vector2 RaycastDirection() {
        Vector2 raycastDirection = player.transform.position - transform.position;
        if((raycastDirection.y < -followRange.down || raycastDirection.y > followRange.up || Mathf.Abs(raycastDirection.x) > followRange.vertical) || (raycastDirection.x * wanderDir < -2f && !canSeeBehind)) {
            raycastDirection = new Vector2(followRange.vertical * wanderDir, 0);
            if(transform.position.x - startPos > wanderDist && wanderDir == 1 || startPos - transform.position.x > wanderDist && wanderDir == -1)
                wanderDir *= -1;
        }
        Debug.DrawRay(transform.position, raycastDirection);
        return raycastDirection;
    }

    void SpriteFacing() {
        var anim = GetComponent<Animator>();

        if(rb2D.velocity.x > 0.2f) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetFloat("velocity", Mathf.Clamp01(Mathf.Abs(rb2D.velocity.x)));
        }
        else if(rb2D.velocity.x < -0.2f) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            anim.SetFloat("velocity", Mathf.Clamp01(Mathf.Abs(rb2D.velocity.x)));
        }
        else {
            anim.SetFloat("velocity", 0.1f);
        }

        if(raycastHit) {
            if(raycastHit.collider.name.Equals("Player")) {
                if(player.transform.position.x - transform.position.x < 0)
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                else
                    transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

    }

    void OnBecameVisible() {
        if(timeBeforeWander == 0)
            StartWander();
    }

    void StartWander() {
        if(Random.Range(0, 2) == 0)
            wanderDir = -1;
        else
            wanderDir = 1;
    }
}
