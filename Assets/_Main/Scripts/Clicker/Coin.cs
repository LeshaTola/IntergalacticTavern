using System;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
	public event Action OnCoinClicked;

	[SerializeField] private Wallet wallet;
	[field: SerializeField] public int CoinsForClick { get; private set; } = 1;

	[SerializeField] private FlyingCoinText coinTemplate;
	private ObjectPool<FlyingCoinText> textPool;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(() =>
		{
			wallet.AddMoney(CoinsForClick);
			textPool.Get();
			OnCoinClicked?.Invoke();
		});

		textPool = new(
			preloadFunc: () =>
			{
				return Instantiate(coinTemplate, transform);
			},

			getAction: (FlyingCoinText coinText) =>
			{
				coinText.transform.position = transform.position;

				coinText.UpdateText(CoinsForClick);
				coinText.Show();
				coinText.Shot();

				Timer timer = new Timer(2f, endAction: () =>
				{
					textPool.Release(coinText);
				});
				StartCoroutine(timer.Start());
			},

			releaseAction: (FlyingCoinText coinText) =>
			{
				coinText.Hide();
			},
			10
			);
	}

	public void MultiplyCoinForClick(int multiplyValue)
	{
		CoinsForClick = CoinsForClick * multiplyValue;
	}
}
