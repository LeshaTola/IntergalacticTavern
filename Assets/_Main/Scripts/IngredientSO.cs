using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients/Item")]
public class IngredientSO : ScriptableObject
{
	[field: SerializeField] private Sprite Sprite { get; set; }
	[field: SerializeField] private string Name { get; set; }
}
