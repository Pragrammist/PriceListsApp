using MediatR;
using Microsoft.AspNetCore.SignalR;
using WebApplication1.Features.CreatePriceListFeature;
using WebApplication1.Features.CreateProductFeature;
using WebApplication1.Features.ShowPriceListsFeature;

namespace WebApplication1.Hubs;


//Была бы возможность я бы каждый метод бы в свой feature бы расположил
//Но SignalR такой возможности не обладает,
//поэтому используется MediatR
public class PriceListHub(IMediator mediator) : Hub
{
    #region CreatePriceListFeature
    public async Task<long> CreatePriceList(CreatePriceListRequest createPriceListData)
    {
        var result = await mediator.Send(createPriceListData, Context.ConnectionAborted);
        
        return result;
    }
    #endregion

    #region ShopPriceListsFeature

    public async Task<List<PriceList>> ShowPriceLists(ShowPriceListsRequest showPriceLists)
    {
        var result = await mediator.Send(showPriceLists, Context.ConnectionAborted);
        return result;
    }
    #endregion

    // #region PriceListDetails
    //
    //
    // #endregion

    #region CreateProductFeature

    public async Task CreateProduct(CreateProductRequest request)
    {
        await mediator.Send(request, Context.ConnectionAborted);
    }
    
    #endregion

    #region ShowPriceListDetailsFeature

    public async Task DeleteProduct(DeleteProductRequest deleteProductRequest)
    {
        await mediator.Send(deleteProductRequest, Context.ConnectionAborted);
    }

    #endregion
}