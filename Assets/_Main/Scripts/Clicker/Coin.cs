using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
	[SerializeField] private Wallet wallet;
	[SerializeField] private int coinsForClick;

	[SerializeField] private FlyingCoinText coinTemplate;
	private ObjectPool<FlyingCoinText> textPool;

	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(() =>
		{
			wallet.AddMoney(coinsForClick);
			textPool.Get();
		});

		textPool = new(
			preloadFunc: () =>
			{
				return Instantiate(coinTemplate, transform);
			},

			getAction: (FlyingCoinText coinText) =>
			{
				coinText.transform.position = transform.position;

				coinText.UpdateText(coinsForClick);
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
}
