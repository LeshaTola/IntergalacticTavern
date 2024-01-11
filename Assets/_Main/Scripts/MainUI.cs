using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
	[SerializeField] private Button shopButton;
	[SerializeField] private ShopUI shopUI;
	[SerializeField] private TextMeshProUGUI orderScoreText;
	[SerializeField] private OrderManager orderManager;

	private void Awake()
	{
		shopButton.onClick.AddListener(() =>
		{
			shopUI.Show();
		});
	}

	private void OnEnable()
	{
		orderManager.OnScoreChanged += OnScoreChanged;
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
		orderScoreText.text = "—чет: " + score;
	}
}
