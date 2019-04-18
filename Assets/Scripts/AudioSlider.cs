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

    float sliderLength;
    float mixerLength = 80;
    float stretchFactor;

    void Start()
	{
		slider = GetComponent<Slider>();

        sliderLength = slider.maxValue - slider.minValue;
        stretchFactor = mixerLength / sliderLength;

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
        float posSliderVal = sliderVal - slider.minValue;
        if (posSliderVal == 0)
            return -80.0f;
        return Mathf.Log10(posSliderVal / sliderLength) * mixerLength;
    }

    float StretchBackToSlider(float mixerVal)
	{
        return Mathf.Pow(10, (mixerVal + mixerLength) / mixerLength - 1) * sliderLength + slider.minValue;
    }
}
