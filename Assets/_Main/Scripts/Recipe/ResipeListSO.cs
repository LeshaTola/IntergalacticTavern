using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipesList", menuName = "Recipe/List")]
public class RecipeListSO : ScriptableObject
{
	[field: SerializeField] public List<RecipeSO> List { get; private set; }
}
