using System.Collections.Generic;
using UnityEngine;

public class BuyButtonSoundManager : MonoBehaviour
{
	[SerializeField] private List<AudioClip> buySounds;
	[SerializeField] private ShopUI shopUI;

	private void Start()
	{
		shopUI.OnBuy += OnBuy;
	}
	private void OnDestroy()
	{
		shopUI.OnBuy -= OnBuy;
	}

	private void OnBuy()
	{
		SoundManager.PlaySound(buySounds.ToArray(), Vector3.zero);
	}
}
