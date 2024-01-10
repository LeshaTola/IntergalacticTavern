using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{

	public event Action OnMoneyChanged;

	public int Money { get; private set; }

	public void AddMoney(int count)
	{
		if (count <= 0)
		{
			return;
		}

		Money += count;
		OnMoneyChanged?.Invoke();
	}

	public void DenyMoney(int count)
	{
		if (count <= 0)
		{
			return;
		}

		Money -= count;
		OnMoneyChanged?.Invoke();
	}
}
