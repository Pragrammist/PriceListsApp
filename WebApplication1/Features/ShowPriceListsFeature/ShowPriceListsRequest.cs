using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore;

namespace WebApplication1.Features.ShowPriceListsFeature;

public class ShowPriceListsRequest : IRequest<List<PriceList>>
{
    public int Page { get; set; }
}

public class PriceList
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ShowPriceListsHandler(PriceListDbContext dbContext) : IRequestHandler<ShowPriceListsRequest, List<PriceList>>
{
    public async Task<List<PriceList>> Handle(ShowPriceListsRequest request, CancellationToken cancellationToken)
    {
        var priceLists = await dbContext.PriceLists.Select(p => new PriceList
        {
            Id = p.Id,
            Name = p.Name
        })
        .OrderByDescending(pl => pl.Id)
        .Skip(request.Page * 10)
        .Take(10)
        .ToListAsync(cancellationToken: cancellationToken);
        return priceLists;

    }
}