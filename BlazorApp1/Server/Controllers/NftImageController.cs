using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;
using NftUtility;
using System.Numerics;

namespace BlazorApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class NftImageController : ControllerBase
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _config;
    public NftImageController(IHttpClientFactory clientFactory, IConfiguration config)
    {
        _clientFactory = clientFactory;
        _config = config;
    }

    [HttpGet]
    public async Task<NftImage> Get(string contractAddress, int tokenId)
    {
        var infuraApiKey = _config["Settings:InfuraApiKey"];
        var client = _clientFactory.CreateClient();
        var uri = await contractAddress.ToImageUrlAsync(tokenId, infuraApiKey, client);
        return new NftImage() { ImageUrl = uri.AbsoluteUri };
    }
}