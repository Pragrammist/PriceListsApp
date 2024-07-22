using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore;

namespace WebApplication1.Features.PriceListDetailsFeature;

public class PriceListDetailsRequest : IRequest<PriceListDetails>
{
    public long Id { get; set; }

    public int Page { get; set; }
}

public class PriceListDetails
{
    public string Name { get; set; } = null!;
    public List<ColumnHeaderData> HeadersData { get; set; } = [];
    public List<ProductData> Products { get; set; } = [];
}
public class ColumnHeaderData
{
    public string ColumnName { get; set; } = null!;
    public long ColumnTypeId { get; set; }
}

public class ProductData
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Articul { get; set; } = null!;
    public List<ProductColumnData> ColumnValues { get; set; } = [];
}

public class ProductColumnData
{
    public ColumnHeaderData Column { get; set; } = null!;

    public string Value { get; set; } = null!;
}



public class PriceListDetailsHandler(PriceListDbContext context) : IRequestHandler<PriceListDetailsRequest, PriceListDetails>
{
    public async Task<PriceListDetails> Handle(PriceListDetailsRequest request, CancellationToken cancellationToken)
    {
        //получаем прайслисты со всеми нужными полями прогруженными из бд
        var priceList = await context.PriceLists
            .Include(p => p.Columns)
            .Include(p => p.Products
                    //не удаленные продукты с ордером и пагинацией
                .Where(pd => !pd.IsDeleted)
                .OrderByDescending(pl => pl.Id)
                .Skip(request.Page * 10).Take(10))
            .ThenInclude(p => p.ColumnValues)
            .ThenInclude(p => p.Column)
            .FirstAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
        
        //это колонки без значений(без Value) которые используются как заголовки в таблице
        var headerColumns = priceList.Columns.Select(c => new ColumnHeaderData
        {
            ColumnName = c.PropName,
            ColumnTypeId = c.PropTypeId
        }).OrderBy(c => c.ColumnName).ToList();
        //продукты относящиеся к прайс-листу
        var products = priceList.Products.Select(c => new ProductData
        {
            Articul = c.Articul,
            Id = c.Id,
            Name = c.Name,
            //значение привязанны к колонке
            ColumnValues = c.ColumnValues.Select(s => new ProductColumnData
            {
                //колнка
                Column = new ColumnHeaderData
                {
                    ColumnName = s.Column.PropName,
                    ColumnTypeId = s.Column.PropTypeId
                },
                //значение колонки
                Value = s.ColumnValue
            }).OrderBy(d => d.Column.ColumnName).ToList()
        }).OrderBy(c => c.Name).ToList();
        var result = new PriceListDetails
        {
            Name = priceList.Name,
            Products = products,
            HeadersData = headerColumns
        };
        return result;
    }
}