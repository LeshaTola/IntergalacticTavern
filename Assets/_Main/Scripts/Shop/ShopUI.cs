using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private Wallet wallet;

	[SerializeField] private IngredientListSO ingredients;
	[SerializeField] private Transform buttonsContainer;
	[SerializeField] private SingleBuyButtonUI buttonUITemplate;

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
		foreach (var ingredient in ingredients.List)
		{
			var newButton = Instantiate(buttonUITemplate, buttonsContainer);

			newButton.UpdateUI(ingredient);

			newButton.SetBuyButtonAction(() =>
			{
				if (wallet.Money >= ingredient.Cost)
				{
					wallet.DenyMoney(ingredient.Cost);
					inventory.AddIngredient(ingredient);
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
