public partial class Player
{
    public int GetItemAmount(string itemName)
    {
        int itemAmount = 0;
        
        foreach (var inventorySlot in inventorySlots)
        {
            if (inventorySlot.inventoryItem == null) continue;
            
            if (inventorySlot.inventoryItem.itemName == itemName)
            {
                itemAmount += inventorySlot.Amount;
            }
        }

        return itemAmount;
    }

    /// <summary>
    /// Removes a specific amount from an item in the inventory
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="amountToRemove"> 0 - Remove All | N - Remove N quantity </param>
    public void RemoveItem(string itemName, int amountToRemove = 0)
    {
        if (amountToRemove == 0)
        {
            foreach (var inventorySlot in inventorySlots)
            {
                if (inventorySlot.isEmpty) continue;

                if (inventorySlot.inventoryItem.itemName == itemName)
                {
                    inventorySlot.Clear();
                }
            }

            return;
        }

        var amountRemoved = 0;

        foreach (var inventorySlot in inventorySlots)
        {
            if (inventorySlot.isEmpty) continue;

            if (inventorySlot.inventoryItem.itemName == itemName)
            {
                do
                {
                    inventorySlot.Amount--;
                    amountRemoved++;
                } while (amountRemoved < amountToRemove && inventorySlot.Amount > 0);

                if (inventorySlot.Amount <= 0) inventorySlot.Clear();

                if (amountRemoved >= amountToRemove) break;
            }
        }
    }
}