using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
    [System.Serializable]
	public struct ItemAttributes{
		public string name;
		public string attrib;
	}

	[System.Serializable]
	public struct InventoryCell {
        public ItemsInfo.Id id;
        public int count;
		public List<ItemAttributes> attribs;
    }
	

    public enum Results{
		Ok, There_is_no_place, The_cell_is_empty, Lacks_the_items, There_is_no_item, Other_inventory_does_not_have_free_space

    }

	[SerializeField] private InventoryCell[] inventory;

    public bool isResetTheInventory = true;

	// Use this for initialization
	void Start (){
        if (isResetTheInventory){
            for (int i = 0; i < inventory.Length; i++){
                inventory[i].id = ItemsInfo.Id.empty;
                inventory[i].count = 0;
            }
        }
	}

    public Results giveTheItem(ItemsInfo.Id id, int count, List<ItemAttributes> attribs = null){
		int maxCountInCell = (int) ItemsInfo.getInfo (id).maxCountInCell;
		long cellsCount = count / maxCountInCell;
		if (cellsCount <= getCountEmptyCells () && getCountEmptyCells() != 0) { //Если будет баг - всместо '<=' поставить '<'
			int countT = count;
			for (int i = 0; i < inventory.Length; i++) {
				if (inventory [i].id == ItemsInfo.Id.empty) {
					inventory [i].id = id;
					if (countT > maxCountInCell) {
						inventory [i].count += maxCountInCell;
						countT -= maxCountInCell;
					} else {
						inventory [i].count += countT;
						countT -= countT;
					}
					if(attribs != null){
						setItemAttributesList(i, attribs);
					}
				} else if (inventory [i].id == id) {
					if (inventory [i].count < maxCountInCell) {
						if (countT > maxCountInCell - inventory [i].count) {
							countT -= maxCountInCell - inventory [i].count;
							inventory [i].count += maxCountInCell - inventory [i].count;
						} else {
							inventory [i].count += countT;
							countT -= countT;
						}
						if(attribs != null){
							setItemAttributesList(i, attribs);
						}
					}
				}

				if (countT <= 0) {
					break;
				}
			}
		} else {
			for (int i = 0; i < inventory.Length; i++) {
				if (inventory [i].count + count <= maxCountInCell) {
					if (inventory [i].id == id) {
						inventory [i].count += count;
						return Results.Ok;
					} else if (inventory [i].id == ItemsInfo.Id.empty) {
						inventory [i].id = id;
						inventory [i].count += count;
						return Results.Ok;
					}
					if(attribs != null){
						setItemAttributesList(i, attribs);
					}
				}
			}

			return Results.There_is_no_place;
		}

		return Results.Ok;
    }

    public Results deleteTheItem(int index)
    {
        if (inventory[index].id != ItemsInfo.Id.empty) {
            inventory[index].id = ItemsInfo.Id.empty;
            inventory[index].count = 0;
			inventory[index].attribs.Clear();
        }
        else{
            return Results.The_cell_is_empty;
        }
        return Results.Ok;
    }

    public Results pickUpTheSomeItems(int index, int count){
        if (inventory[index].id != ItemsInfo.Id.empty){
            if ((inventory[index].count - count) >= 0){
                inventory[index].count -= count;
            }
            else{
                return Results.Lacks_the_items;
            }
        }
        else{
			return Results.There_is_no_item;
        }

        if (inventory[index].count <= 0) {
            deleteTheItem(index);
        }

        return Results.Ok;
    }

	public Results pickUpTheSomeItems(ItemsInfo.Id id, int count){
		if (getCountById (id) >= count) {
			int countT = count;
			int maxCountInCell = (int) ItemsInfo.getInfo (id).maxCountInCell;
			for (int i = 0; i < inventory.Length; i++) {
				if (inventory [i].id == id) {
					if (inventory [i].count <= countT) {
						countT -= inventory [i].count;
						inventory [i].count -= inventory [i].count;
					} else if (inventory [i].count > countT) {
						inventory [i].count -= countT;
						countT -= countT;
					}
				}

				if (inventory [i].count <= 0) {
					deleteTheItem (i);
				}
			}
		} else {
			return Results.There_is_no_item;
		}

		return Results.Ok;
	}

    public ItemsInfo.Id getIdOfTheItem(int index) {
        if(inventory.Length < index)
        {
            return ItemsInfo.Id.empty;
        }

		return inventory[index].id;
    }

	public Results isHaveFreePlace(){
		bool isContinue = false;

		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i].id == ItemsInfo.Id.empty) {
				isContinue = true;
			}
		}

		if (!isContinue) {
			return Results.There_is_no_place;
		}

		return Results.Ok;
	}

	public Results isHaveTheItem(ItemsInfo.Id id){
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i].id == id) {
				return Results.Ok;
			}
		}

		return Results.There_is_no_item;
	}

	public Results translateItemsToOtherInventory(InventoryController otherInventory, int index, int count){
		if (otherInventory.isHaveFreePlace () == Results.Ok) {
			ItemsInfo.Id idOfTheItem = getIdOfTheItem (index);
			if (idOfTheItem != ItemsInfo.Id.empty) {
				List<ItemAttributes> attribs = new List<ItemAttributes>();
				if(inventory[index].attribs.Count > 0){
					attribs = inventory[index].attribs.GetRange(0, inventory[index].attribs.Count);
				}
				Results resultOfPickUp = pickUpTheSomeItems (index, count);
				if (resultOfPickUp == Results.Ok) {
					Results resultOfGiveTheItem = otherInventory.giveTheItem (idOfTheItem, count, attribs);
					if (resultOfGiveTheItem != Results.Ok) {
						return resultOfGiveTheItem;
					}
				} else {
					return resultOfPickUp;
				}
			} else {
				return Results.There_is_no_item;
			}
		} else {
			return Results.Other_inventory_does_not_have_free_space;
		}

		return Results.Ok;
	}

	public Results translateItemsToOtherInventory(InventoryController otherInventory, ItemsInfo.Id id, int count){
		if (otherInventory.isHaveFreePlace () == Results.Ok) {
			Results resultOfHaveTheItem = isHaveTheItem (id);
			if (resultOfHaveTheItem == Results.Ok) {
				Results resultOfPickUp = pickUpTheSomeItems (id, count);
				if (resultOfPickUp == Results.Ok) {
					Results resultOfGiveTheItem = otherInventory.giveTheItem (id, count);
					if (resultOfGiveTheItem != Results.Ok) {
						return resultOfGiveTheItem;
					}
				} else {
					return resultOfPickUp;
				}
			} else {
				return Results.There_is_no_item;
			}
		} else {
			return Results.Other_inventory_does_not_have_free_space;
		}

		return Results.Ok;
	}

	public InventoryCell[] items{
		get { return inventory; }
	}

	public int getCountById(ItemsInfo.Id id){
		int count = 0;
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i].id == id) {
				count += inventory [i].count;
			}
		}
		return count;
	}

	public int getCountEmptyCells(){
		int count = 0;
		for (int i = 0; i < inventory.Length; i++) {
			if (inventory [i].id == ItemsInfo.Id.empty) {
				count++;
			}
		}
		return count;
	}

	////////////////////////////////////////////////////
	/// Атрибуты предметов
	////////////////////////////////////////////////////

	public static ItemAttributes newItemAttribute(string name, string attrib){
		ItemAttributes _attrib;
		_attrib.name = name;
		_attrib.attrib = attrib;
		return _attrib;
	}

	public string getItemAttribute(int itemIndex, string name){
		for(int i = 0; i < inventory[itemIndex].attribs.Count; i++){
			if(inventory[itemIndex].attribs[i].name == name){
				return inventory[itemIndex].attribs[i].attrib;
			}
		}
		return null;
	}

	public bool setItemAttribute(int itemIndex, string name, string attrib){
		int index = -1;
		for(int i = 0; i < inventory[itemIndex].attribs.Count; i++){
			if(inventory[itemIndex].attribs[i].name == name){
				index = i;
			}
		}
		if(index >= 0){
			inventory[itemIndex].attribs.Remove(inventory[itemIndex].attribs[index]);
			inventory[itemIndex].attribs.Add(newItemAttribute(name, attrib));
		} else {
			inventory[itemIndex].attribs.Add(newItemAttribute(name, attrib));
		}
		return true;
	}

	public void setItemAttributesList(int itemIndex, List<ItemAttributes> attribs){
		inventory[itemIndex].attribs = attribs;
	}

	public void clearItemAttributesList(int itemIndex){
		inventory[itemIndex].attribs.Clear();
	}
}
