
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
		public int Money { get; set; }
		public int Score { get; set; }

		public List<int> IngredientCount { get; set; }

		public List<int> OrdersIndexesList { get; set; }
		public int OrderIndex { get; set; }
		public float OrderTimer { get; set; }
	}
}
