using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private SlotUI slotTemplate;
	[SerializeField] private Transform slotsContainer;

	private void Start()
	{
		inventory.OnInventoryChanged += OnInventoryChanged;
		UpdateUI();
	}

	private void OnDestroy()
	{
		inventory.OnInventoryChanged -= OnInventoryChanged;
	}

	private void OnInventoryChanged()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		foreach (Transform child in slotsContainer)
		{
			Destroy(child.gameObject);
		}

		foreach (var slot in inventory.Slots)
		{
			var newSlotUI = Instantiate(slotTemplate, slotsContainer);
			newSlotUI.UpdateUI(slot);
		}
	}
}
