using UnityEngine;
using YG;

public enum RewardId
{
	X2PerClick,
	X2CurrentMoney
}

public class RewardManager : MonoBehaviour
{

	[SerializeField] private Wallet wallet;
	[SerializeField] private Coin coin;

	private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

	private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

	void Rewarded(int id)
	{
		RewardId rewardId = (RewardId)id;

		switch (rewardId)
		{
			case RewardId.X2PerClick:
				coin.MultiplyCoinForClick(2);
				break;
			case RewardId.X2CurrentMoney:
				wallet.AddMoney(wallet.Money);

				break;
		}
	}

}
