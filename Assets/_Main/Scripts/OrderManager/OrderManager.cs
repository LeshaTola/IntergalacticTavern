using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class OrderManager : MonoBehaviour, IUseSaves
{
	public event Action<float> OnTimerChanged;
	public event Action<RecipeSO> OnOrderChanged;
	public event Action<int> OnScoreChanged;

	[SerializeField] private Inventory inventory;
	[SerializeField] private SaveSystem saveSystem;

	private List<RecipeSO> ordersList;
	private float orderTimer;

	private int currentOrderIndex;
	public RecipeSO CurrentOrder { get; private set; }
	public int Score { get; private set; }

	[field: SerializeField] public RecipeListSO RecipeList { get; private set; }


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

		Score += CurrentOrder.Score;
		OnScoreChanged?.Invoke(Score);

		foreach (var ingredientWithCount in CurrentOrder.Ingredients)
		{
			inventory.RemoveIngredient(ingredientWithCount.Ingredient, ingredientWithCount.Count);
		}

		NextOrder();
		saveSystem.Save();
	}

	public void RejectOrder()
	{
		Score -= CurrentOrder.Score / 2;
		if (Score < 0)
		{
			Score = 0;
		}
		OnScoreChanged?.Invoke(Score);

		NextOrder();
		saveSystem.Save();
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
		ordersList = Shuffle(RecipeList.List);
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

	private List<int> GetListOfOrdersIndexes(List<RecipeSO> orders)
	{
		var ordersIndexes = new List<int>();

		foreach (var order in orders)
		{
			ordersIndexes.Add(RecipeList.List.IndexOf(order));
		}

		return ordersIndexes;
	}

	private List<RecipeSO> GetListOfOrdersFromIndexes(List<int> ordersIndexes)
	{
		var orders = new List<RecipeSO>();

		foreach (var index in ordersIndexes)
		{
			orders.Add(RecipeList.List[index]);
		}

		return orders;
	}

	public void SaveData()
	{
		YandexGame.savesData.Score = Score;
		YandexGame.savesData.OrdersIndexesList = GetListOfOrdersIndexes(ordersList);
		YandexGame.savesData.OrderTimer = orderTimer;
		YandexGame.savesData.OrderIndex = currentOrderIndex;
	}

	public void LoadData()
	{
		Score = YandexGame.savesData.Score;
		OnScoreChanged?.Invoke(Score);

		var ordersIndexesList = YandexGame.savesData.OrdersIndexesList;
		if (ordersIndexesList == null || ordersIndexesList.Count <= 0)
		{
			ResetOrders();
			return;
		}

		ordersList = GetListOfOrdersFromIndexes(ordersIndexesList);
		SetOrder(YandexGame.savesData.OrderIndex);

		orderTimer = YandexGame.savesData.OrderTimer;
		OnTimerChanged?.Invoke(orderTimer);

	}
}
