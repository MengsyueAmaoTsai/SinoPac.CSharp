namespace RichillCapital.SinoPac.Sor;

public class OrdsTable
{
    /// <summary>
    /// 用 SorRID 索引 SorOrd.
    /// </summary>
    SortedList<string, int> SorOrds_ = new SortedList<string, int>();
    /// <summary>
    /// 依加入順序儲存的委託列表.
    /// </summary>
    List<SorOrder> SorOrdsList_ = new List<SorOrder>();

    /// <summary>
    /// 清除全部委託資料.
    /// </summary>
    public void Clear()
    {
        SorOrds_.Clear();
        SorOrdsList_.Clear();
    }

    /// <summary>
    /// 使用委託Key = OrdSorRID, 取得委託書.
    /// </summary>
    public SorOrder SorOrdAtKey(string orgSorRID)
    {
        int lisuint;
        if (!SorOrds_.TryGetValue(orgSorRID, out lisuint))
            return null;
        SorOrder ord = SorOrdsList_[lisuint];
        return ord;
    }
    /// <summary>
    /// 增加一筆新委託書 or 若已存在則更新.
    /// </summary>
    public SorOrder AddSorOrd(OrdTable ordTable, string orgSorRID, string[] flds, AccountManager accs)
    {
        SorOrder ord;
        int lisuint;
        if (SorOrds_.TryGetValue(orgSorRID, out lisuint))
        {
            ord = SorOrdsList_[lisuint];
            ord.SetSorOrdFields(flds);
        }
        else
        {
            ord = new SorOrder(ordTable, flds, accs);
            SorOrds_.Add(orgSorRID, SorOrdsList_.Count);
            SorOrdsList_.Add(ord);
        }
        return ord;
    }
    /// <summary>
    /// 增加or更新一筆新委託書, 傳回: true=新增, false=更新.
    /// </summary>
    public bool AddOrUpdateOrder(string orgSorRID, SorOrder ord)
    {
        int lisuint;

        if (SorOrds_.TryGetValue(orgSorRID, out lisuint))
        {
            SorOrdsList_[lisuint] = ord;
            return false;
        }

        SorOrds_.Add(orgSorRID, lisuint = SorOrdsList_.Count);

        SorOrdsList_.Add(ord);

        return true;
    }
    /// <summary>
    /// 增加or更新一筆新委託書, 傳回: true=新增, false=更新.
    /// </summary>
    public bool AddSorOrd(SorOrder ord)
    {
        return AddOrUpdateOrder(ord.OrgSorRID, ord);
    }

    /// <summary>
    /// 取得委託筆數.
    /// </summary>
    public int Count { get { return SorOrdsList_.Count; } }
    /// <summary>
    /// 取得委託列表.
    /// </summary>
    public List<SorOrder> OrdsList { get { return SorOrdsList_; } }
}
