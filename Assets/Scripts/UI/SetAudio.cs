using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Slider))]
public class SetAudio : MonoBehaviour {
	private float audiovolume;

	void Start()
	{
		audiovolume = PlayerPrefs.GetFloat(Prefs.ton, 1f);
		GetComponent<Slider>().value = audiovolume;
		setVolume(audiovolume);
	}
	void OnDisable()
	{
		save();
	}
	void OnApplicationQuit()
	{
		save();
	}

	public void setVolume(float volume)
	{
		audiovolume = volume;
		AudioListener.volume = volume;
	}
	public void save()
	{
		PlayerPrefs.SetFloat(Prefs.ton, audiovolume);
	}
}
