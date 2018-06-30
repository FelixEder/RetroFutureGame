using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {
    Rigidbody2D rb2D;
    PlayerStatus status;
    InputManager input;

    public float knockForce;
    bool holdStomp, isStomping, acquired;
    public LayerMask whatIsHurtable;
    public Transform stompCenter;
    Collider2D[] victims;

    // Use this for initialization
    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        input = GameObject.Find("InputManager").GetComponent<InputManager>();
        //Change sprite, display correct tutorial and play theme.
    }

    void Update() {
        if(GetComponent<PlayerInventory>().HasAcquired("stomp") && !acquired) {
            acquired = true;
        }
    }

    void FixedUpdate() {
        if(acquired) {
            if(!input.GetKey("attack"))
                holdStomp = false;
            if(status.InAir() && !status.isSmall && input.GetAxis("Y") < -0.3f && input.GetAxis("ysign") < 0f && input.GetKey("attack") && !holdStomp && !isStomping) {
                holdStomp = true;
                isStomping = true;
                status.isStomping = true;

                StartCoroutine(StartStomp());
            }
            else if(status.grounded && isStomping) {

                LandStomp();
            }
        }
    }


    IEnumerator StartStomp() {
        Debug.Log("Started Stomp");
        rb2D.velocity = new Vector2(0, 0);
        rb2D.gravityScale = 0.0f;

        yield return new WaitForSeconds(0.2f);
        status.invulnerable = true;
        rb2D.gravityScale = 2.0f;
        rb2D.velocity = new Vector2(0, -9f);
    }

    void LandStomp() {
        Debug.Log("Finished Stomp");
        GetComponent<AudioPlayer>().PlayClip(5, 2f);

        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        isStomping = false;
        status.isStomping = false;
        status.Invulnerable(0.2f);

        victims = Physics2D.OverlapBoxAll(stompCenter.position, new Vector2(4f, 2f), 0, whatIsHurtable);

        foreach(Collider2D victim in victims) {
            var enemyHealth = victim.gameObject.GetComponent<EnemyHealth>();
            switch(victim.gameObject.tag) {

                case "SmallCritter":
                case "JumpingCritter":
                    enemyHealth.TakeDamage(3, gameObject, knockForce);
                    break;

                case "CrawlerCritter":
                    Debug.Log("Hit crawler!");
                    var crawler = victim.gameObject.GetComponent<CrawlerCritter>();
                    if(!crawler.noShell) {
                        enemyHealth.TakeDamage(1, gameObject, knockForce);
                        crawler.BreakShell();
                    }
                    else
                        enemyHealth.TakeDamage(2, gameObject, knockForce);
                    break;

                case "ShellMan":
                    enemyHealth.Knockback(gameObject, 4f);
                    break;

                case "FinalBossLastForm":
                    victim.gameObject.GetComponent<Phase3Head>().TakeDamage();
                    break;

                case "Barrier":
                    if(victim.gameObject.GetComponent<Barrier>().GetBarrierType().Equals("Stomp")) {
                        victim.gameObject.GetComponent<Barrier>().TakeDamage(1);
                    }
                    break;

                default:
                    if(enemyHealth)
                        enemyHealth.Knockback(gameObject, knockForce);
                    break;
            }
            Debug.Log("STOMPED: " + victim.gameObject.name + " with tag: " + victim.gameObject.tag);
        }
    }
}