//  Hold data for rare items in the database
public struct RareItemStruct
{
    public RareItemStruct(int itemID, string name)
    {
        this.itemID = itemID;
        this.name = name;
    }

    public string name { get; private set; }
    public int itemID { get; private set; }

    public override string ToString()
    {
        return $"[ ItemID: {itemID}, Name: {name} ]";
    }
}
