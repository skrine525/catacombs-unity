using UnityEngine.UI;
using UnityEngine;

public static class ItemsInfo {

	public struct Items {
		public Id id;
		public string name;
		public uint maxCountInCell;
		public ItemType type;
		public Sprite image;
	}

	public enum ItemType {
		Null, Resource, Tool, Equipment, Food, WorldItem
	}

	public static Items getInfo(Id id) {
		int indexOfEmptyId = 0;
		for (int i = 0; i < items.Length; i++) {
			if (items [i].id == id) {
				return items [i];
			}
			if (items [i].id == Id.empty) {
				indexOfEmptyId = i;
			}
		}
		return items [indexOfEmptyId];
	}

	public static Id getIdByStrId(string strId){
		Id returnId = Id.empty;
		for (int i = 0; i < items.Length; i++) {
			if (items [i].id.ToString () == strId) {
				returnId = items [i].id;
			}
		}
		return returnId;
	}

	private static Items newItem(Id id, string name, uint maxCountInCell, ItemType type, string nameOfImage){
		Items item;
		item.id = id;
		item.name = name;
		item.maxCountInCell = maxCountInCell;
		item.type = type;
		item.image = Resources.Load<Sprite>("Textures/UI/Inventory/Items Images/" + nameOfImage);
		return item;
	}

	/// <summary>
	/// ///////////////////////////////////////////////////////////////////////////////////
	/// </summary>

	public enum Id {
		empty, bed, chest, craft_table, log, board, strawberry, cherry, tomato_soup, water_bottle, axe, hammer, pickaxe,
		iron_ingot, stick, iron_ore, coal, iron_rod, pea_stew, ministrone, wooden_cup, apple, apple_juice_wooden_cup, coin,
		backpack
	}

	/// <summary>
	/// ///////////////////////////////////////////////////////////////////////////////////
	/// </summary>

	public static readonly Items[] items = {
		newItem(Id.empty, "Пусто", 1, ItemType.Null, "empty"), newItem(Id.chest, "Сундук", 2, ItemType.WorldItem, "chest"), newItem(Id.craft_table, "Верстак", 1, ItemType.WorldItem, "craft_table"), newItem(Id.bed, "Кровать", 1, ItemType.WorldItem, "bed"),
		newItem(Id.log, "Бревно", 1, ItemType.Resource, "log"), newItem(Id.board, "Доска", 5, ItemType.Resource, "board"), newItem(Id.strawberry, "Клубника", 15, ItemType.Food, "strawberry"), newItem(Id.cherry, "Вишня", 15, ItemType.Food, "cherry"),
		newItem(Id.tomato_soup, "Томатный суп", 8, ItemType.Food, "tomato_soup"), newItem(Id.water_bottle, "Бутылка воды", 3, ItemType.Food, "water_bottle"), newItem(Id.axe, "Топор", 1, ItemType.Tool, "axe"), newItem(Id.hammer, "Молот", 1, ItemType.Tool, "hammer"),
		newItem(Id.pickaxe, "Кирка", 1, ItemType.Tool, "pickaxe"), newItem(Id.iron_ingot, "Железный слиток", 5, ItemType.Resource, "iron_ingot"), newItem(Id.stick, "Палка", 10, ItemType.Resource, "stick"), newItem(Id.iron_ore, "Железная руда", 10, ItemType.Resource, "iron_ore"),
		newItem(Id.coal, "Уголь", 10, ItemType.Resource, "coal"), newItem(Id.iron_rod, "Железная балка", 2, ItemType.Resource, "iron_rod"), newItem(ItemsInfo.Id.pea_stew, "Тушеное мясо", 8, ItemType.Food, "pea_stew"), newItem(ItemsInfo.Id.ministrone, "Овощной суп", 8, ItemType.Food, "ministrone"),
		newItem(Id.wooden_cup, "Деревянная чаша", 3, ItemType.Resource, "wooden_cup"), newItem(Id.apple, "Яблоко", 8, ItemType.Food, "apple"), newItem(Id.apple_juice_wooden_cup, "Яблочный сок", 1, ItemType.Food, "apple_juice_wooden_cup"), newItem(Id.coin, "Монета", 50, ItemType.Resource, "coin"),
		newItem(Id.backpack, "Рюкзак", 1, ItemType.Tool, "backpack")
	};
}
