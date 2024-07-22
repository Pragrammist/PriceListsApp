using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore;
using WebApplication1.EfCore.Models;

namespace WebApplication1.Features.CreatePriceListFeature;

public class CreatePriceListRequest : IRequest<long>
{
    public string Name { get; set; } = null!;
    public List<PriceColumn> NewPriceColumns { get; set; } = [];
}

public class PriceColumn
{
    public long PropTypeId { get; set; }
    public string PropName { get; set; } = null!;
}

public class CreatePriceListHandler(PriceListDbContext dbContext) : IRequestHandler<CreatePriceListRequest, long>
{
    async Task<List<PriceColumnModel>> GetPriceColumns(CreatePriceListRequest request)
    {
        //идея в том чтобы сначала загрузить все колонки которые есть в таблице,
        //а если пользователь впервые добавляет какуе-то колонку(уникальность колонки определяется по сочетанию названия и типа)
        var result = new List<PriceColumnModel>();
        foreach (var newColumn in request.NewPriceColumns)
        {
            //прогрузили возможно существую колонку
            var dbColumn = await dbContext.PriceColumns.FirstOrDefaultAsync(c =>
                c.PropName == newColumn.PropName && c.PropTypeId == newColumn.PropTypeId);
            if (dbColumn is not null)
            {
                result.Add(dbColumn);
            }
            else
            {
                //а если колонки нет, то просто добавляем модель в результат
                result.Add(new PriceColumnModel
                {
                    PropName = newColumn.PropName,
                    PropTypeId = newColumn.PropTypeId
                });
            }
        }

        return result;
    }
    
    public async Task<long> Handle(CreatePriceListRequest request, CancellationToken cancellationToken)
    {
        var priceList = new PriceListModel
        {
            Columns = await GetPriceColumns(request),
            Name = request.Name
        };
        var entry = await dbContext.PriceLists.AddAsync(priceList, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }
}