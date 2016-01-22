using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CompleteProject
{
public class UpdateSlider : MonoBehaviour {

	public Slider slider;
	public Color MaxHealthColor = Color.green;
	public Color MinHealthColor = Color.red;

	RectTransform fill;

		void Awake()
		{
			fill = slider.fillRect;
			fill.GetComponent<Image> ().color = MaxHealthColor;
		}


	public void SetSlider(float newValue)
		{
			float maxHealth = slider.maxValue;

			slider.value = newValue;

			fill.GetComponent<Image>().color = Color.Lerp(MinHealthColor, MaxHealthColor, (float) newValue / maxHealth);
		}

	public void SetInactive()
		{
			gameObject.SetActive (false);
		}

	public void SetActive()
		{
			gameObject.SetActive (true);
		}

}
}