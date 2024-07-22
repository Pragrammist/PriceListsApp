using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore;
using WebApplication1.EfCore.Models;

namespace WebApplication1.Features.CreateProductFeature;

public class CreateProductRequest : IRequest
{
    public long PriceListId { get; set; }
    public string Name { get; set; } = null!;
    public string Articul { get; set; } = null!;
    public List<ColumnData> ColumnsData { get; set; } = [];
}

public class ColumnData
{
    public string ColumnName { get; set; } = null!;
    public long ColumnTypeId { get; set; }
    public string Value { get; set; } = null!;
}

public class CreateProductHandler(PriceListDbContext context) : IRequestHandler<CreateProductRequest>
{
    public async Task Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        List<PriceColumnModel> columnModels = [];
        //сначала просто получаем все существующие колонки
        foreach (var column in request.ColumnsData)
        {
            var columnModel = await context.PriceColumns.FirstAsync(c => 
                c.PropName == column.ColumnName 
                    && c.PropTypeId == column.ColumnTypeId, 
                        cancellationToken);
            columnModels.Add(columnModel);
        }
        
        //после связываем колнонки с значением с этой колонки
        var columnValues = request.ColumnsData.Select(c => new ProductColumnValueModel
        {
            ColumnValue = c.Value,
            //ищем из прошлого списка по названию и по типу подходящую колонку чтобы привязать к ней значние
            Column = columnModels.First(d =>
                d.PropName == c.ColumnName && c.ColumnTypeId == d.PropTypeId)
        }).ToList();
        var product = new ProductModel
        {
            Articul = request.Articul,
            Name = request.Name,
            ColumnValues = columnValues,
            PriceListId = request.PriceListId
        };
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}

