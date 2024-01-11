using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class Inventory : MonoBehaviour, IUseSaves
{
	public event Action OnInventoryChanged;

	[SerializeField] private IngredientListSO ingredients;

	public List<IngredientsWithCount> Slots { get; private set; }

	private void Awake()
	{
		//CreateInventory();
	}

	private void CreateInventory()
	{
		Slots = new List<IngredientsWithCount>();
		foreach (var ingredient in ingredients.List)
		{
			Slots.Add(new IngredientsWithCount()
			{
				Ingredient = ingredient,
				Count = 0
			});
		}
	}

	public void AddIngredient(IngredientSO ingredient, int count = 1)
	{
		if (count <= 0)
			return;

		Slots[FindIngredientIndex(ingredient)].Count += count;
		OnInventoryChanged?.Invoke();
	}

	public void RemoveIngredient(IngredientSO ingredient, int count = 1)
	{
		if (count <= 0)
			return;

		Slots[FindIngredientIndex(ingredient)].Count -= count;
		OnInventoryChanged?.Invoke();
	}

	public bool IsEnough(IngredientSO ingredient, int count = 1)
	{
		return Slots[FindIngredientIndex(ingredient)].Count >= count;
	}

	private int FindIngredientIndex(IngredientSO ingredient)
	{
		for (int i = 0; i < Slots.Count; i++)
		{
			if (Slots[i].Ingredient == ingredient)
			{
				return i;
			}
		}

		return -1;
	}

	public void SaveData()
	{
		YandexGame.savesData.IngredientCount = Slots.Select(s => s.Count).ToList();
	}

	public void LoadData()
	{
		var listOfCounts = YandexGame.savesData.IngredientCount;

		if (listOfCounts == null || listOfCounts.Count <= 0)
		{
			CreateInventory();
			return;
		}

		for (int i = 0; i < listOfCounts.Count; i++)
		{
			Slots[i].Count = listOfCounts[i];
		}

		OnInventoryChanged?.Invoke();
	}
}
