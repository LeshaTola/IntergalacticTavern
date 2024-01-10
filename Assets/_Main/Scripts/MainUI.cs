using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
	[SerializeField] private Button shopButton;
	[SerializeField] private TextMeshProUGUI orderScoreText;
	[SerializeField] private OrderManager orderManager;

	private void Awake()
	{
		shopButton.onClick.AddListener(() =>
		{
			Debug.Log("Магазин");
		});
	}

	private void Start()
	{
		orderManager.OnScoreChanged += OnScoreChanged;

		UpdateUI(0);
	}

	private void OnDestroy()
	{
		orderManager.OnScoreChanged -= OnScoreChanged;
	}

	private void OnScoreChanged(int score)
	{
		UpdateUI(score);
	}

	private void UpdateUI(int score)
	{
		orderScoreText.text = "Счет: " + score;
	}
}
