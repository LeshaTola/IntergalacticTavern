using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public event Action OnInventoryChanged;

	[SerializeField] private IngredientListSO ingredients;

	public List<IngredientsWithCount> Slots { get; private set; }

	private void Awake()
	{
		CreateInventory();
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
}
