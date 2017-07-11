using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEnergy : MonoBehaviour {
	public int currentEnergy, maxEnergy, rechargeTime;
	bool reCharging;
	Slider slider, rechargeSlider;

	void Start() {
		slider = GameObject.Find("energySlider").GetComponent<Slider>();
		rechargeSlider = GameObject.Find("energyRechargeSlider").GetComponent<Slider>();
		SetEnergySliderSize();
		SetEnergySlider();
	}

	void Update() {
		if(currentEnergy < maxEnergy && !reCharging) {
			StartCoroutine(EnergyRecharge());
		}
	}

	public bool UseEnergy(int amount) {
		if(currentEnergy - amount < 0) {
			OutOfEnergy();
			return false;
		}
		else {
			currentEnergy -= amount;
			StartCoroutine(TransitionEnergySlider());
			return true;
		}
	}

	public void IncreaseCurrentEnergy(int amount) {
		currentEnergy += amount;
		if(currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
		StartCoroutine(TransitionEnergySlider());
	}

	public void IncreaseMaxEnergy() {
		maxEnergy++;
		currentEnergy = maxEnergy;
		SetEnergySliderSize();
		StartCoroutine(TransitionEnergySlider());
	}

	public void MaximizeEnergy() {
		SetEnergySlider();
		currentEnergy = maxEnergy;
		StartCoroutine(TransitionEnergySlider());
	}


	public void SetEnergySlider() {
		slider.value = (float) currentEnergy / maxEnergy;
	}

	void SetEnergySliderSize() {
		GameObject.Find("energySlider").GetComponent<RectTransform>().sizeDelta = new Vector2(8 + 32 * (float) maxEnergy, 32);
		GameObject.Find("energyRechargeSlider").GetComponent<RectTransform>().sizeDelta = new Vector2(8 + 32 * (float) maxEnergy, 32);
	}

	void OutOfEnergy() {
		//PLay some sound or animation since you are out of energy
	}

	IEnumerator EnergyRecharge() {
		reCharging = true;
		int startEnergy = currentEnergy;
		while(currentEnergy < maxEnergy && startEnergy == currentEnergy) {
			rechargeSlider.value = (float) startEnergy / maxEnergy;
			while(rechargeSlider.value * (float) maxEnergy < startEnergy + 0.9f && currentEnergy < maxEnergy && startEnergy == currentEnergy) {
				rechargeSlider.value += Time.deltaTime / (rechargeTime * 5);
				yield return new WaitForSeconds(0.01f);
			}
			if(startEnergy == currentEnergy)
				IncreaseCurrentEnergy(1);
		}
		yield return new WaitForSeconds(1.0f);
		rechargeSlider.value = 0.0f;
		reCharging = false;
	}

	IEnumerator TransitionEnergySlider() {
		float transitionSpeed = (maxEnergy - Mathf.Abs(slider.value * maxEnergy - currentEnergy));
		while(slider.value * (float) maxEnergy < currentEnergy - 0.1f || slider.value * (float) maxEnergy > currentEnergy + 0.1f) {
			slider.value = Mathf.Lerp(slider.value, (float) currentEnergy / maxEnergy, Time.deltaTime * transitionSpeed);
			yield return new WaitForSeconds(0.01f);
		}
		SetEnergySlider();
	}
}
