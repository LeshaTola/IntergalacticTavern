using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
	public event Action OnBuy;

	[SerializeField] private Inventory inventory;
	[SerializeField] private Wallet wallet;

	[SerializeField] private IngredientListSO ingredients;
	[SerializeField] private List<SingleBuyButtonUI> buttons;


	[SerializeField] private Button exitButton;
	[SerializeField] private TextMeshProUGUI moneyText;

	private void Awake()
	{
		exitButton.onClick.AddListener(() =>
		{
			Hide();
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
