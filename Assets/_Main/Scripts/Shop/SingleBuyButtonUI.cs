using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleBuyButtonUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI buttonText;
	[SerializeField] private Button buyButton;

	public void UpdateUI(IngredientSO ingredient)
	{
		buttonText.text = ingredient.Name + "\n" + ingredient.Cost;
	}

	public void SetBuyButtonAction(Action action)
	{
		buyButton.onClick.AddListener(() =>
		{
			action();
		});
	}
}
