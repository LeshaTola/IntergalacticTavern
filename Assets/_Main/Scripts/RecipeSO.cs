using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe/Item")]
public class RecipeSO : ScriptableObject
{
	[field: SerializeField] public Sprite Sprite { get; set; }
	[field: SerializeField] public string Name { get; set; }
	[field: SerializeField] public int Score { get; set; }
	[field: SerializeField] public List<IngredientsWithCount> Ingredients { get; set; }
}

[Serializable]
public class IngredientsWithCount
{
	public IngredientSO Ingredient;
	public int Count;
}
