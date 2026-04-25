namespace Bifrost
{
    public enum JobMode
    {
        New,
        Modify,
        Read,
        NewAfterRead,
    }

    public enum PayMode
    {
        None,
        Paid
    }

    public enum ContentsMode
    {
        Customer,
        Item,
        Favorite,
        Custom,
        StoredProcedure
    }

    public enum DBType
    {
        New,
        Old
    }


    public enum TempOrderType
    {
        Order,
        Customer,
        Etc
    }

    public enum ReceitMode
    {
        Single,
        Double,
        Etc
    }
}