using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEnergy : MonoBehaviour {
	public int currentEnergy, maxEnergy, rechargeTime;
	bool reCharging;
	public Slider slider, rechargeSlider;

	void Start() {
		SetEnergySliderSize();
		SetEnergySlider(slider);
		SetEnergySlider(rechargeSlider);
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
			TransitionEnergySlider();
			return true;
		}
	}

	public void IncreaseCurrentEnergy(int amount) {
		currentEnergy += amount;
		if(currentEnergy > maxEnergy)
			currentEnergy = maxEnergy;
		TransitionEnergySlider();
	}

	public void IncreaseMaxEnergy() {
		maxEnergy++;
		currentEnergy = maxEnergy;
		SetEnergySliderSize();
		TransitionEnergySlider();
	}

	public void MaximizeEnergy() {
		currentEnergy = maxEnergy;
		TransitionEnergySlider();
	}


	public void SetEnergySlider(Slider _slider) {
		_slider.value = (float) currentEnergy / maxEnergy;
	}

	void SetEnergySliderSize() {
		slider.GetComponent<RectTransform>().sizeDelta = new Vector2(8 + 32 * (float) maxEnergy, 32);
		rechargeSlider.GetComponent<RectTransform>().sizeDelta = new Vector2(8 + 32 * (float) maxEnergy, 32);
		rechargeSlider.transform.GetChild(0).GetComponent<Image>().enabled = maxEnergy > 0;
	}

	void OutOfEnergy() {
		//PLay some sound or animation since you are out of energy
	}

	IEnumerator EnergyRecharge() {
		reCharging = true;
		yield return new WaitForSeconds(2.0f);
		int startEnergy = currentEnergy;
		while(currentEnergy < maxEnergy && startEnergy == currentEnergy) {
			rechargeSlider.value = (float) startEnergy / maxEnergy;
			while(rechargeSlider.value * (float) maxEnergy < startEnergy + 1f && currentEnergy < maxEnergy && startEnergy == currentEnergy) {
				rechargeSlider.value += Time.deltaTime / (rechargeTime * 5);
				yield return new WaitForSeconds(0.01f);
			}
			if(startEnergy == currentEnergy) {
				IncreaseCurrentEnergy(1);
				SetEnergySlider(rechargeSlider);
			}
		}
		reCharging = false;
	}
	
	void TransitionEnergySlider() {
		StopCoroutine(TransitionSlider());
		StartCoroutine(TransitionSlider());
	}

	IEnumerator TransitionSlider() {
		while(slider.value * (float) maxEnergy < currentEnergy - 0.1f || slider.value * (float) maxEnergy > currentEnergy + 0.1f) {
			slider.value = Mathf.Lerp(slider.value, (float) currentEnergy / maxEnergy, Time.deltaTime * 5);
			yield return new WaitForSeconds(0.01f);
		}
		SetEnergySlider(slider);
		
		while(rechargeSlider.value * (float) maxEnergy < currentEnergy - 0.1f || rechargeSlider.value * (float) maxEnergy > currentEnergy + 0.1f) {
			rechargeSlider.value = Mathf.Lerp(rechargeSlider.value, (float) currentEnergy / maxEnergy, Time.deltaTime * 5);
			yield return new WaitForSeconds(0.01f);
		}
		SetEnergySlider(rechargeSlider);
	}
}
