using UnityEngine;
using System.Collections;

public class PlayerPickUp : MonoBehaviour {
    PlayerStatus status;
    PlayerInventory inventory;
    InputManager input;
    bool holdPickup;

    public LayerMask whatIsItem;
	public Transform itemInventory;

    void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector2(1.2f, 2f));
    }

    void Start() {
        inventory = GetComponent<PlayerInventory>();
        status = GetComponent<PlayerStatus>();
        input = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update() {
        if(!input.GetKey("grab") && holdPickup) {
            holdPickup = false;
        }
        if(input.GetKey("grab") && !holdPickup && inventory.IsHoldingItem() && !status.isSmall) { //calls on drop method.
            holdPickup = true;
            Drop(true);
        }
        else if(input.GetKey("grab") && !holdPickup && !inventory.IsHoldingItem() && !status.isSmall) {
            holdPickup = true;
            PickupArea();
        }
    }

    void PickupArea() {
        Collider2D item = Physics2D.OverlapBox(transform.position, new Vector2(1.2f, 2f), 0, whatIsItem);
        if(item != null) {
            Debug.Log("Tried to pick up " + item);
            switch(item.gameObject.GetComponent<PickUpableItem>().GetItemType()) {
                case "Rock":
                case "Branch":
                    PickUp(item.gameObject);
                    break;
            }
        }
    }

	/**Sets the item gameobject as child of "player" and freezes all it's movement.*/
    void PickUp(GameObject item) {
        inventory.SetHoldingItem(item.gameObject);
        item.transform.SetParent(itemInventory);
		item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
		item.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
		Debug.Log("Picked up " + item.gameObject);
    }
	
    /**
 	* Sets the gamobject as child of Items gameobject and allows all movement.
 	* If canThrow; adds a force to gamobject relative to input if player has horizontal input.
 	*/
    public void Drop(bool canThrow) {
        var item = inventory.GetHoldingItem().GetComponent<PickUpableItem>();
        item.transform.localPosition = new Vector2(0.5f, 0);
        item.gameObject.layer = LayerMask.NameToLayer("CPickupableItem");
        item.transform.SetParent(item.originalParent);
        item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        if(canThrow && (Input.GetAxis("Horizontal") > 0.2 || Input.GetAxis("Horizontal") < -0.2)) {
            item.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 500 * Mathf.Sign(Input.GetAxis("Horizontal")));
        }
		inventory.SetHoldingItem(null);		
		Debug.Log("Dropped " + item.gameObject);
    }
}
