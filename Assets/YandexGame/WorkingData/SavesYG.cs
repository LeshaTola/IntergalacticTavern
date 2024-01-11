
using System.Collections.Generic;

namespace YG
{
	[System.Serializable]
	public class SavesYG
	{
		// "Технические сохранения" для работы плагина (Не удалять)
		public int idSave;
		public bool isFirstSession = true;
		public string language = "ru";
		public bool promptDone;

		// Ваши сохранения
		public int Money;
		public int Score;

		public List<int> IngredientCount;

		public List<int> OrdersIndexesList;
		public int OrderIndex;
		public float OrderTimer;
	}
}
