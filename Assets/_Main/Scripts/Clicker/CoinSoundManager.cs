using System.Collections.Generic;
using UnityEngine;

public class CoinSoundManager : MonoBehaviour
{
	[SerializeField] private List<AudioClip> sounds;
	[SerializeField] private Coin coin;

	private void Start()
	{
		coin.OnCoinClicked += OnCoinClicked;
	}

	private void OnCoinClicked()
	{
		SoundManager.PlaySound(sounds.ToArray(), Vector3.zero);
	}
}
