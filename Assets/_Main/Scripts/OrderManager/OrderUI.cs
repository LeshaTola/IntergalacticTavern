using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
	[SerializeField] private Image foodImage;
	[SerializeField] private Image clientImage;

	[SerializeField] private TextMeshProUGUI foodNameText;
	[SerializeField] private TextMeshProUGUI foodRecipeText;

	[SerializeField] private Slider timeSlider;

	[SerializeField] private Button ApplyButton;
	[SerializeField] private Button RejectButton;

	[SerializeField] private OrderManager orderManager;

	private void Awake()
	{
		ApplyButton.onClick.AddListener(() =>
		{
			orderManager.ApplyOrder();
		});

		RejectButton.onClick.AddListener(() =>
		{
			orderManager.RejectOrder();
		});
	}

	private void Start()
	{
		orderManager.OnOrderChanged += OnOrderChanged;
		orderManager.OnTimerChanged += OnTimerChanged;

		OnOrderChanged(orderManager.CurrentOrder);
	}

	private void UpdateTimer(float value)
	{
		timeSlider.value = value;
	}

	private void OnTimerChanged(float value)
	{
		UpdateTimer(value);
	}

	private void OnOrderChanged(RecipeSO order)
	{
		foodImage.sprite = order.Sprite;
		clientImage.sprite = order.Client;


		foodNameText.text = order.Name;
		foodRecipeText.text = GetRecipeText(order.Ingredients);

		UpdateTimer(0);
	}

	private string GetRecipeText(List<IngredientsWithCount> list)
	{
		string recipeText = string.Empty;
		foreach (IngredientsWithCount ingredientsWithCount in list)
		{
			recipeText += $"*{ingredientsWithCount.Count} {ingredientsWithCount.Ingredient.Name}\n";
		}

		return recipeText;
	}
}
