namespace InventoryService.ApiService.Batches;

public class Txn
{
    public Guid Id { get; private set; }

    public string Note { get; private set; } = string.Empty;

    public int AdjustedQuantity { get; private set; }
}