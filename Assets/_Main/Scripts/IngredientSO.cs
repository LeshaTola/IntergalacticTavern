using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Ingredients/Item")]
public class IngredientSO : ScriptableObject
{
	[field: SerializeField] public Sprite Sprite { get; private set; }
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public int Cost { get; private set; }
}
