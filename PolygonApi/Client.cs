using System.Text.Json;
using MoretechBack.Database.Models;
using MoretechBack.PolygonApi.Models;

namespace MoretechBack.PolygonApi;

public class Client : IPolygonApiClient
{
    private readonly HttpClient client;
    
    private const string BaseUrl = "https://hackathon.lsp.team/hk";

    private readonly string polygonPublic;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public Client(string polygonPublic, HttpClient? client = default)
    {
        this.polygonPublic = polygonPublic;
        this.client = client ?? new HttpClient();
    }

    private async Task<(string PublicKey, string PrivateKey)> GenerateWallet()
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/wallets/new", null);
        var walletJson = await response.Content.ReadAsStreamAsync();
        var wallet = await JsonSerializer.DeserializeAsync<Wallet>(walletJson, JsonOptions);
        return (wallet!.PublicKey, wallet.PrivateKey);
    }

    private async Task<string> TransferMatic(string fromPrivateKey, string toPublicKey, double amount)
    {
        var transaction = new { FromPrivateKey = fromPrivateKey, ToPublicKey = toPublicKey, Amount = amount };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/transfers/matic", transaction, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson, JsonOptions);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<string> TransferRubble(string fromPrivateKey, string toPublicKey, double amount)
    {
        var transaction = new { FromPrivateKey = fromPrivateKey, ToPublicKey = toPublicKey, Amount = amount };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/transfers/ruble", transaction, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson, JsonOptions);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<string> TransferNft(string fromPrivateKey, string toPublicKey, long tokenId)
    {
        var transaction = new { FromPrivateKey = fromPrivateKey, ToPublicKey = toPublicKey, TokenId = tokenId };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/transfers/nft", transaction, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson, JsonOptions);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<string> GetTransferStatus(string transactionHash)
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/transfers/status/{transactionHash}", null);
        var statusJson = await response.Content.ReadAsStreamAsync();
        var status = await JsonSerializer.DeserializeAsync<TransactionStatus>(statusJson, JsonOptions);
        return status!.Status;
    }
    
    private async Task<(double MaticAmount, double RublesAmount)> GetWalletsBalance(string publicKey)
    {
        var response = await client.GetStreamAsync($"{BaseUrl}/v1/wallets/{publicKey}/balance");
        var amounts = await JsonSerializer.DeserializeAsync<Balances>(response, JsonOptions);
        return (amounts!.MaticAmount, amounts.CoinsAmount);
    }
    
    private async Task<List<(string URI, List<long> tokens)>> GetNftBalance(string publicKey)
    {
        var balanceJson = await client.GetStreamAsync($"{BaseUrl}/v1/wallets/{publicKey}/nft/balance");
        var balance = await JsonSerializer.DeserializeAsync<NftBalance>(balanceJson, JsonOptions);
        return balance!.Balance.Select(nft => (nft.Uri, nft.Tokens.ToList())).ToList();
    }
    
    private async Task<string> GenerateNft(string toPublicKey, string uri, int nftCount)
    {
        var generateNftRequest = new NewNft(toPublicKey, uri, nftCount);
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/nft/generate", generateNftRequest, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson, JsonOptions);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<(long TokenId, string URI, string PublicKey)> GetNft(string tokenId)
    {
        var nftJson = await client.GetStreamAsync($"{BaseUrl}/v1/nft/{tokenId}");
        var nft = await JsonSerializer.DeserializeAsync<Nft>(nftJson, JsonOptions);
        return (nft!.TokenId, nft.Uri, nft.PublicKey);
    }

    private async Task<(string WalletId, List<long> Tokens)> GetGeneratedNft(string hash)
    {
        var generatedNftJson = await client.GetStreamAsync($"{BaseUrl}/v1/nft/generate/{hash}");
        var generatedNft = await JsonSerializer.DeserializeAsync<GeneratedNftTransaction>(generatedNftJson, JsonOptions);
        return (generatedNft!.WalletId, generatedNft.Tokens);
    }

    private async Task<List<HistoryRecord>> GetTransactionHistory(string publicKey, int page, int offset, string sort)
    {
        var requestParams = new { Page = page, Offset = offset, Sort = sort };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/wallets/{publicKey}/history", requestParams, JsonOptions);
        var historyJson = await response.Content.ReadAsStreamAsync();
        var history = await JsonSerializer.DeserializeAsync<HistoryResponse>(historyJson, JsonOptions);
        return history!.History;
    }

    public async Task<List<HistoryRecord>> GetHistory(User user) =>
        await GetTransactionHistory(user.PublicKey, 0, 20, "dsc");

    public async Task<double> GetRubleBalance(User user) => (await GetWalletsBalance(user.PublicKey)).RublesAmount;
}