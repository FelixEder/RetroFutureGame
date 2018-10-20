using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour {
	public PlayerInventory inventory;
	public string tooltip, upgradeType;
	Image image;
	
	void Start () {
		image = transform.GetChild(0).GetComponent<Image>();
	}
	
	void Update () {
		if(inventory.HasAcquired(upgradeType) && !image.enabled)
			image.enabled = true;
	}
}
