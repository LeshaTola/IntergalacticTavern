using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI count;
	[SerializeField] private Image image;

	public void UpdateUI(IngredientsWithCount ingredientsWithCount)
	{
		count.text = ingredientsWithCount.Count.ToString();
		image.sprite = ingredientsWithCount.Ingredient.Sprite;
	}
}
