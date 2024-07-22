namespace WebApplication1.EfCore.Models;

public class PriceListModel
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    public List<PriceColumnModel> Columns { get; set; } = [];
    
    public List<ProductModel> Products { get; set; } = [];
}

public class PriceColumnModel
{
    public long Id { get; set; }
    public PriceColumnPropTypeModel? PropType { get; set; }
    public long PropTypeId { get; set;  }
    public string PropName { get; set; } = null!;
    public List<PriceListModel> PriceLists { get; set; } = [];
}

public class PriceColumnPropTypeModel
{
    public long Id { get; set; }
    
    public string Name { get; set; } = null!;
}


public class ProductModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    //По хорошему это должно называться VendorCode - но Articul более понятно
    public string Articul { get; set; } = null!;

    public PriceListModel? PriceList { get; set; }
    public long PriceListId { get; set; }

    public List<ProductColumnValueModel> ColumnValues { get; set; } = [];

    public bool IsDeleted { get; set; }
}


public class ProductColumnValueModel
{
    public long Id { get; set; }
    
    public PriceColumnModel Column { get; set; } = null!;

    public string ColumnValue { get; set; } = null!;
}

[Flags]
public enum ColumnTypeEnum
{
    Number = 1,
    String = 2,
    Text = 3
};

    

