using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientList", menuName = "Ingredients/List")]
public class IngredientListSO : ScriptableObject
{
	[field: SerializeField] public List<IngredientSO> List { get; set; }
}
