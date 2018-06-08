using UnityEngine;
using System.Collections;

public class BigEyeGuy : MonoBehaviour {
    public LayerMask hitLayers;
    public int damage = 1;

	AudioPlayer audioplay;
    public LineRenderer lineRenderer;
    Vector2 laserHit;
    RaycastHit2D hit;
    bool shooting, laserActive;

    EnemyMovement enemyMove;

    void Start() {
		audioplay = GetComponent<AudioPlayer>();
        enemyMove = transform.GetComponent<EnemyMovement>();
        lineRenderer.useWorldSpace = true;
    }

    void Update() {

        //Calculate laser trajectory with raycast.
        hit = Physics2D.Raycast(lineRenderer.transform.position, new Vector2(enemyMove.wanderDir, 0), Mathf.Infinity, hitLayers);
        laserHit = hit.point;

        //Set linerenderer positions.
        lineRenderer.SetPosition(0, lineRenderer.transform.position + Vector3.back);
        lineRenderer.SetPosition(1, laserHit);


        if(enemyMove.raycastHit && !laserActive) {
            if(enemyMove.raycastHit.collider.name == "Player") {
                laserActive = true;
                StartCoroutine(ShootLaser());
            }
        }
        
        if(shooting)
            HitByLaser(hit);
    }

    IEnumerator ShootLaser() {
        yield return new WaitForSeconds(1f);

        audioplay.PlayClip(0, 0.7f);

        yield return new WaitForSeconds(1.5f);

        //enable line and set shooting.
        audioplay.PlayClip(1, 0.7f);
        lineRenderer.enabled = true;
        shooting = true;

        yield return new WaitForSeconds(0.1f);

        //disable line and set not shooting.
        audioplay.StopPlaying();
        lineRenderer.enabled = false;
        shooting = false;

        yield return new WaitForSeconds(1f);

        laserActive = false;
        
		
    }

    public void CancelLaser() {
        CancelInvoke();

        lineRenderer.enabled = false;
        shooting = false;
    }

    void HitByLaser(RaycastHit2D victim) {
        switch(victim.collider.gameObject.tag) {

            case "Player":
                Debug.Log("Hit by laser!!");
                victim.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage, gameObject, 10f);
                break;

            case "SmallCritter":
                victim.collider.GetComponent<EnemyHealth>().TakeDamage(99);
                break;

            case "PickupableItem":
                victim.collider.gameObject.GetComponent<PickUpableItem>().Break();
                break;

            default:
                break;
        }
    }


    /**
	 * The barrier of the door has been broken, it is now destroyed.
	 */
    void Broken() {
        //Play animation and 
        Destroy(gameObject);
    }
}