using UnityEngine;

public static class FoodParameters{
	public struct FoodInfo
	{
		public ItemsInfo.Id id;
		public float restoredFood;
		public float restoredWater;
		public GameObject dropPrefab;
		public ItemsInfo.Id inventoryEmployItemDropId;

	}

	private static FoodInfo setFoodInfo(ItemsInfo.Id id, float restoredFood, float restoredWater, string dropPrefabName, ItemsInfo.Id inventoryEmployItemDropId = ItemsInfo.Id.empty){
		FoodInfo food;
		food.id = id;
		food.restoredFood = restoredFood;
		food.restoredWater = restoredWater;
		food.dropPrefab = Resources.Load<GameObject> ("Prefabs/Foods/" + dropPrefabName);
		food.inventoryEmployItemDropId = inventoryEmployItemDropId;
		return food;
	}

	public static FoodInfo getFoodInfo(ItemsInfo.Id id){
		for (int i = 0; i < food.Length; i++) {
			if (food [i].id == id) {
				return food [i];
			}
		}
		return setFoodInfo (ItemsInfo.Id.empty, 0f, 0f, "");
	}

	public static readonly FoodInfo[] food = {
		setFoodInfo (ItemsInfo.Id.strawberry, 4f, 0f, "Strawberry"), setFoodInfo (ItemsInfo.Id.cherry, 4f, 0f, "null"), setFoodInfo (ItemsInfo.Id.tomato_soup, 30f, 0f, "Tomato_Soup"),
		setFoodInfo (ItemsInfo.Id.water_bottle, 0f, 50f, "null"), setFoodInfo (ItemsInfo.Id.pea_stew, 50f, 0f, "Pea_Stew"), setFoodInfo (ItemsInfo.Id.ministrone, 30f, 0f, "Ministrone"),
		setFoodInfo (ItemsInfo.Id.apple, 10f, 0, "Apple"), setFoodInfo(ItemsInfo.Id.apple_juice_wooden_cup, 0f, 30f, "Apple_Juice_Wooden_Cup", ItemsInfo.Id.wooden_cup)
	};
}
