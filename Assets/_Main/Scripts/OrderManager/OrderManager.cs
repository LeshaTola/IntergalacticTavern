using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
	public event Action<float> OnTimerChanged;
	public event Action<RecipeSO> OnOrderChanged;
	public event Action<int> OnScoreChanged;

	[SerializeField] private List<RecipeSO> recipes;
	[SerializeField] private Inventory inventory;

	private List<RecipeSO> ordersList;
	private float orderTimer;
	private int score;

	private int currentOrderIndex;
	public RecipeSO CurrentOrder { get; private set; }

	private void Start()
	{
		ResetOrders();
	}

	private void Update()
	{
		HandleOrderTimer();
	}

	public void ApplyOrder()
	{
		if (!CheckAbilityToApply())
		{
			return;
		}

		score += CurrentOrder.Score;
		OnScoreChanged?.Invoke(score);

		foreach (var ingredientWithCount in CurrentOrder.Ingredients)
		{
			inventory.RemoveIngredient(ingredientWithCount.Ingredient, ingredientWithCount.Count);
		}

		NextOrder();
	}

	public void RejectOrder()
	{
		NextOrder();
	}

	private bool CheckAbilityToApply()
	{
		foreach (var ingredientWithCount in CurrentOrder.Ingredients)
		{
			if (!inventory.IsEnough(ingredientWithCount.Ingredient, ingredientWithCount.Count))
			{
				return false;
			}
		}

		return true;
	}

	private void HandleOrderTimer()
	{
		orderTimer -= Time.deltaTime;
		if (orderTimer <= 0)
		{
			RejectOrder();
			orderTimer = CurrentOrder.Time;
		}
		OnTimerChanged(orderTimer / CurrentOrder.Time);
	}

	private void SetOrder(int orderIndex)
	{
		if (ordersList != null && ordersList.Count <= 0 || orderIndex >= ordersList.Count)
		{
			ResetOrders();
			return;
		}

		currentOrderIndex = orderIndex;
		CurrentOrder = ordersList[orderIndex];
		OnOrderChanged?.Invoke(CurrentOrder);

		orderTimer = CurrentOrder.Time;
		OnTimerChanged?.Invoke(0);
	}

	private void NextOrder()
	{
		SetOrder(currentOrderIndex + 1);
	}

	private void ResetOrders()
	{
		ordersList = Shuffle(recipes);
		SetOrder(0);
	}

	private List<RecipeSO> Shuffle(List<RecipeSO> originalList)
	{
		var newList = originalList.ToList();

		System.Random rng = new();
		int n = newList.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			RecipeSO value = newList[k];
			newList[k] = newList[n];
			newList[n] = value;
		}
		return newList;
	}

}
