using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
	public AudioMixer mixer;
    public string audioMixerParameterName;

	Slider slider;

	void Start()
	{
		slider = GetComponent<Slider>();
		float mixerValue;
		mixer.GetFloat(audioMixerParameterName, out mixerValue);
		slider.value = StretchBackToSlider(mixerValue);
	}

	public void OnVolumeSliderMoved(float value)
	{
		mixer.SetFloat(audioMixerParameterName, StretchToMixer(value));
	}


	float StretchToMixer(float sliderVal)
	{
		
		float sliderLength = slider.maxValue - slider.minValue;
		float mixerLength = 80;
		float stretchFactor = mixerLength / sliderLength;
		float zeroedSliderVal = sliderVal - slider.maxValue;
		return zeroedSliderVal * stretchFactor;
	}

	float StretchBackToSlider(float mixerVal)
	{
		float sliderLength = slider.maxValue - slider.minValue;
		float mixerLength = 80;
		float stretchFactor = sliderLength / mixerLength;
		return mixerVal * stretchFactor + slider.maxValue;
	}
}
