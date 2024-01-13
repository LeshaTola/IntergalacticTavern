using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopUI : MonoBehaviour
{
	public event Action OnBuy;

	[SerializeField] private Inventory inventory;
	[SerializeField] private Wallet wallet;

	[SerializeField] private IngredientListSO ingredients;
	[SerializeField] private List<SingleBuyButtonUI> buttons;


	[SerializeField] private Button exitButton;
	[SerializeField] private Button x2PerClickButton;
	[SerializeField] private Button x2CurrentMoneyButton;
	[SerializeField] private TextMeshProUGUI moneyText;

	private void Awake()
	{
		exitButton.onClick.AddListener(() =>
		{
			Hide();
		});

		x2PerClickButton.onClick.AddListener(() =>
		{
			YandexGame.RewVideoShow((int)RewardId.X2PerClick);
		});

		x2CurrentMoneyButton.onClick.AddListener(() =>
		{
			YandexGame.RewVideoShow((int)RewardId.X2CurrentMoney);
		});

		SetupUI();
	}

	private void Start()
	{
		wallet.OnMoneyChanged += OnMoneyChanged;
		updateUI(wallet.Money);
		Hide();
	}

	private void OnDestroy()
	{
		wallet.OnMoneyChanged -= OnMoneyChanged;
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void SetupUI()
	{
		for (int i = 0; i < ingredients.List.Count; i++)
		{
			var ingredient = ingredients.List[i];

			buttons[i].SetBuyButtonAction(() =>
			{
				if (wallet.Money >= ingredient.Cost)
				{
					wallet.DenyMoney(ingredient.Cost);
					inventory.AddIngredient(ingredient);
					OnBuy?.Invoke();
				}
			});
		}
	}

	private void updateUI(int money)
	{
		moneyText.text = $"{money}$";
	}

	private void OnMoneyChanged()
	{
		updateUI(wallet.Money);
	}
}
