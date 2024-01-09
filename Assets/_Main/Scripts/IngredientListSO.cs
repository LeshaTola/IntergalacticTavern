using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientList", menuName = "Ingredients/List")]
public class IngredientListSO : ScriptableObject
{
	[field: SerializeField] private List<IngredientSO> Ingredients { get; set; }
}
