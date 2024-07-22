using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfCore;

namespace WebApplication1.Features.ShowPriceListsFeature;

public class DeleteProductRequest : IRequest
{
    public long ProductId { get; set; }
}

public class DeleteProductHandler(PriceListDbContext dbContext) : IRequestHandler<DeleteProductRequest>
{
    public async Task Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product =
            await dbContext.Products.FirstAsync(p => p.Id == request.ProductId, cancellationToken: cancellationToken);
        product.IsDeleted = true;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
