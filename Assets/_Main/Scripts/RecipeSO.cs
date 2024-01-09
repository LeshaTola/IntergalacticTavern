using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe/Item")]
public class RecipeSO : ScriptableObject
{
	[field: SerializeField] private Sprite Sprite { get; set; }
	[field: SerializeField] private string Name { get; set; }
	[field: SerializeField] private int Score { get; set; }
	[field: SerializeField] private List<IngredientsWithCount> Ingredients { get; set; }
}

[Serializable]
internal class IngredientsWithCount
{
	public IngredientSO Ingredient;
	public int Count;
}
