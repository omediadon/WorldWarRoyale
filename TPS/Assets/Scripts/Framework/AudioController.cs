using System;

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {
	[SerializeField]
	AudioClip[] audioClips = Array.Empty<AudioClip>();
	[SerializeField]
	float delayBetweenClips = 0.2f;

	bool canPlay;
	AudioSource source;

	Timer timer;

	void Start() {
		timer = GameManager.Instance.Timer;
		source = GetComponent<AudioSource>();
		canPlay = true;
	}

	public void Play() {
		if(!canPlay) {
			return;
		}

		timer.Add(() => { canPlay = true; }, delayBetweenClips);

		AudioClip selectedClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
		source.PlayOneShot(selectedClip);
		canPlay = false;
	}
}
