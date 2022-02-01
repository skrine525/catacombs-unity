using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ItemsCraftInfo {
	public struct RecipePart{
		public ItemsInfo.Id id;
		public int count;
	}

	public struct Recipe{
		public ItemsInfo.Id itemId;
		public List<RecipePart> recipe;
		public int count;
	}

	private static Recipe newRecipe(ItemsInfo.Id itemId, int count, List<RecipePart> recipePart){
		Recipe recipe;
		recipe.itemId = itemId;
		recipe.count = count;
		recipe.recipe = recipePart;
		return recipe;
	}

	private static List<RecipePart> recipeList(params RecipePart[] part){
		List<RecipePart> partList = new List<RecipePart>();
		for (int i = 0; i < part.Length; i++) {
			partList.Add (part[i]);
		}
		return partList;
	}

	private static RecipePart newRecipePart(ItemsInfo.Id itemId, int count){
		RecipePart part;
		part.id = itemId;
		part.count = count;
		return part;
	}

	public static Recipe getRecipe(ItemsInfo.Id itemId){
		for (int i = 0; i < recipes.Length; i++) {
			if (recipes [i].itemId == itemId) {
				return recipes [i];
			}
		}
		return newRecipe(ItemsInfo.Id.empty, 0, recipeList(newRecipePart(ItemsInfo.Id.empty, 0)));;
	}

	/// <summary>
	/// ///////////////////////////////////////////////////////////////////////////////////
	/// </summary>

	public static readonly Recipe[] recipes = {
		/////////////////////////////////////////////////////////////
		/// Tool
		newRecipe(ItemsInfo.Id.pickaxe, 1, recipeList(newRecipePart(ItemsInfo.Id.iron_ingot, 2), newRecipePart(ItemsInfo.Id.board, 1))), // 2 Iron Ingots & 1 Boards - 1 Pickaxe
		newRecipe(ItemsInfo.Id.axe, 1, recipeList(newRecipePart(ItemsInfo.Id.iron_ingot, 4), newRecipePart(ItemsInfo.Id.board, 1))), // 4 Iron Ingots & 1 Board - 1 Pickaxe


		/////////////////////////////////////////////////////////////
		/// Equipment


		/////////////////////////////////////////////////////////////
		/// Resource
		newRecipe(ItemsInfo.Id.board, 2, recipeList(newRecipePart(ItemsInfo.Id.log, 1))), // 1 Log - 2 Board
		newRecipe(ItemsInfo.Id.iron_ingot, 1, recipeList(newRecipePart(ItemsInfo.Id.iron_ore, 3), newRecipePart(ItemsInfo.Id.coal, 1))), // 3 Iron Ores - 1 Iron Ingot
		newRecipe(ItemsInfo.Id.iron_rod, 1, recipeList(newRecipePart(ItemsInfo.Id.iron_ingot, 6))), // 6 Iron Ingot - 1 Iron Rod


		/////////////////////////////////////////////////////////////
		/// Food
		newRecipe(ItemsInfo.Id.apple_juice_wooden_cup, 1, recipeList(newRecipePart(ItemsInfo.Id.wooden_cup, 1), newRecipePart(ItemsInfo.Id.apple, 6))), // 1 Wooden Cup & 6 Apples - Apple Juice Wooden Cup

		/////////////////////////////////////////////////////////////
		/// WorldItem
		newRecipe(ItemsInfo.Id.bed, 1, recipeList(newRecipePart(ItemsInfo.Id.board, 5))) // 10 Boards - 1 Bed
	};
}