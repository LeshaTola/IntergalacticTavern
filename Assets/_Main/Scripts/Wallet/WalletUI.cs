using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
	[SerializeField] private Wallet wallet;
	[SerializeField] private TextMeshProUGUI moneyText;

	private void Start()
	{
		wallet.OnMoneyChanged += OnMoneyChanged;
		UpdateUI(0);
	}

	private void OnDestroy()
	{
		wallet.OnMoneyChanged -= OnMoneyChanged;
	}

	private void OnMoneyChanged()
	{
		UpdateUI(wallet.Money);
	}

	private void UpdateUI(int coins)
	{
		moneyText.text = coins.ToString();

	}
}
