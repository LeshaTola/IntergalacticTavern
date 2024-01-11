using UnityEngine;
using YG;

public class SaveSystem : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private Wallet wallet;
	[SerializeField] private OrderManager orderManager;


	private void OnEnable() => YandexGame.GetDataEvent += Load;

	private void OnDisable() => YandexGame.GetDataEvent -= Load;

	private void Start()
	{
		if (YandexGame.SDKEnabled == true)
		{
			Load();
		}
	}

	public void Load()
	{
		wallet.LoadData();
		inventory.LoadData();
		orderManager.LoadData();
	}

	public void Save()
	{
		wallet.SaveData();
		inventory.SaveData();
		orderManager.SaveData();

		YandexGame.SaveProgress();
	}
}
