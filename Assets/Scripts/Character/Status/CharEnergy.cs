using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharEnergy : MonoBehaviour {
	public int currentEnergy, maxEnergy;

	void Start() {
		SetEnergySlider ();
		StartCoroutine (EnergyRecharge ());
	}

	public void UseEnergy(int amount) {
		if (currentEnergy - amount < 0) {
			OutOfEnergy ();
		} else {
			currentEnergy -= amount;
			SetEnergySlider ();
		}
	}

	public bool HasJuice(int glasses) {
		return (currentEnergy - glasses >= 0);
	}

	public void IncreaseCurrentEnergy(int amount) {
		currentEnergy += amount;
		if (currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
		SetEnergySlider ();
	}

	public void IncreaseMaxEnergy() {
		maxEnergy ++;
		currentEnergy = maxEnergy;
		SetEnergySlider ();
	}

	public void MaximizeEnergy() {
		currentEnergy += (maxEnergy - currentEnergy);
	}


	void SetEnergySlider() {
		GameObject.Find ("energySlider").GetComponent<Slider>().value = (float) currentEnergy / maxEnergy;
		GameObject.Find ("energySlider").GetComponent<RectTransform> ().sizeDelta = new Vector2 (8 + 32 *  (float) maxEnergy, 32);
	}

	void OutOfEnergy() {
		//PLay some sound or animation since you are out of energy
	}

	IEnumerator EnergyRecharge() {
		while (currentEnergy < maxEnergy) {
			yield return new WaitForSeconds (5);
			IncreaseCurrentEnergy (1);
		}
	}
}
