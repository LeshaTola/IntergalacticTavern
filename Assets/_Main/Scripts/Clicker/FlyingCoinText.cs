using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyingCoinText : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private TextMeshProUGUI coinText;

	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void Shot()
	{
		Vector2 direction = Random.insideUnitCircle;
		rb.velocity = direction * speed;
	}

	public void UpdateText(int coin)
	{
		coinText.text = $"{coin}$";
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
