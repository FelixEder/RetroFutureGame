using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidLaser : MonoBehaviour {
    public int damage = 2;
    public GameObject player;
    public LayerMask hitLayers;
    public bool canShoot = true;

    bool holdShoot, acquired;

    Vector2 analogDir;
    Vector3 origin, aimDir;
    float speed = 0.7f;

    Animator anim;
    LineRenderer line;
    PlayerEnergy energy;
    InputManager input;


    void Start() {
        energy = player.GetComponent<PlayerEnergy>();
        input = GameObject.Find("InputManager").GetComponent<InputManager>();

        anim = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;
    }

    void Update() {
        if(player.GetComponent<PlayerInventory>().HasAcquired("laser") && !acquired) {
            acquired = true;
        }

        if(acquired) {
            if(input.GetKey("shoot") && !holdShoot)
                holdShoot = true;

            if(!input.GetKey("shoot") && holdShoot && canShoot) {
                if(energy.UseEnergy(2)) {
                    canShoot = false;
                    StartCoroutine(ActivateLaser());
                }
            }

            if(!input.GetKey("shoot"))
                holdShoot = false;
        }
    }

    IEnumerator ActivateLaser() {
        origin = transform.position + new Vector3(0, 0.3f, -1f);

        aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - origin;
        analogDir = new Vector2(input.GetAxis("RightX"), input.GetAxis("RightY"));
        if(analogDir.magnitude != 0)
            aimDir = analogDir;

        aimDir = ((Vector2)aimDir * 10).normalized;

        line.SetPosition(0, origin);
        line.SetPosition(1, origin);
        line.enabled = true;

        StartCoroutine(DeactivateLaser());

        RaycastHit2D raycastHit = new RaycastHit2D();

        for(int i = 0; !raycastHit && i < 50; i++) {
            raycastHit = Physics2D.Raycast(origin, aimDir, 3f, hitLayers);
            Debug.Log("TEST");
            if(raycastHit)
                line.SetPosition(1, raycastHit.point);
            else if(i < 7)
                line.SetPosition(1, origin + (aimDir * i * 0.5f));
            else
                line.SetPosition(1, origin + (aimDir * 3f));

            yield return new WaitForSeconds(0.0001f);
        }
        if(raycastHit)
            HitByLaser(raycastHit);
    }

    IEnumerator DeactivateLaser() {
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(ChargeLaser());
        for(float i = 0; Mathf.Sign(line.GetPosition(1).x - line.GetPosition(0).x) == Mathf.Sign(aimDir.x) && Mathf.Sign(line.GetPosition(1).y - line.GetPosition(0).y) == Mathf.Sign(aimDir.y) && i < 50; i++) {
            origin += (aimDir * speed);
            line.SetPosition(0, origin);
            Debug.Log("Origin = " + origin);

            yield return new WaitForSeconds(0.0001f);
        }

        line.enabled = false;
    }

    IEnumerator ChargeLaser() {
        anim.SetTrigger("charge");

        yield return new WaitForSeconds(1f);

        canShoot = true;
    }

    void HitByLaser(RaycastHit2D victim) {
        Debug.Log("Player shot: " + victim.collider.gameObject.name + " with tag: " + victim.collider.gameObject.tag);

        var enemyHealth = victim.collider.gameObject.GetComponent<EnemyHealth>();

        //NOTE: RaycastHit2D.transform returns parent transform, RaycastHit2D.collider returns the hit collider.
        switch(victim.collider.gameObject.tag) {
            //Add more cases as more types of enemies are added to the game

            case "SmallCritter":
            case "JumpingCritter":
                enemyHealth.TakeDamage(damage);
                break;

            case "HardCritter":
                //victim.transform.gameObject.GetComponent<HardCritter>().Rush();
                break;

            case "BigEyeGuy":
                //Can't be hurt by laser, play relevant things
                break;

            case "CrawlerCritter":
                if(victim.collider.gameObject.GetComponent<CrawlerCritter>().noShell) {
                    enemyHealth.TakeDamage(damage);
                }
                else {
                    //Can't be hurt by laser, play relevant things
                }
                break;

            case "ShellMan":
                ShellMan shellMan = victim.collider.gameObject.GetComponent<ShellMan>();
                if(shellMan.getDeShelled()) {
                    enemyHealth.TakeDamage(damage);
                }
                else {
                    //Can't be hurt by laser, play relevant things
                }
                break;

            case "BirdBossWeakSpot":
                Debug.Log("Hit Bird in the Mouth!");
                victim.collider.transform.parent.GetComponent<BigBadBird>().TakeDamage();
                break;

            case "BigEyeGuyWeakSpot":
                Debug.Log("Hit EyeGuy in the Eye!");
                victim.collider.transform.parent.GetComponent<EnemyHealth>().TakeDamage(damage);
                break;

            case "FinalBossArmor":
                Debug.Log("Hit the boss in the armor!");
                victim.collider.gameObject.GetComponent<Phase1>().Stunned(7f);
                break;

            case "FinalBossArmor2":
                victim.collider.gameObject.GetComponent<Phase2>().Stunned(7f);
                break;

            case "FinalBossWeakSpot":
                Debug.Log("Hit the boss in the weak spot!");
                victim.collider.gameObject.GetComponent<Phase1>().Fall();
                break;

            case "FinalBossHead":
                Debug.Log("Hit the boss in the head!");
                victim.collider.gameObject.GetComponent<Phase2>().LaserShot();
                break;

            case "FinalBossLastForm":
                Debug.Log("Shot last boss form");
                //Check so that it goes after the correct child
                //victim.collider.gameObject.transform.GetChild(0).GetComponent<BigEyeGuy>().Shoot();
                break;

            case "Barrier":
                Debug.Log("Laser hit barrier");
                if(victim.collider.GetComponent<Barrier>().GetBarrierType().Equals("Laser"))
                    victim.collider.GetComponent<Barrier>().TakeDamage(1);
                break;
        }
    }
}
