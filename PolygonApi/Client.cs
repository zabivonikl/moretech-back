﻿using System.Text.Json;
using MoretechBack.PolygonApi.Models;

namespace MoretechBack.PolygonApi;

public class Client
{
    private readonly HttpClient client;
    
    private const string BaseUrl = "https://hackathon.lsp.team/hk";

    private readonly string polygonPublic;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
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
        var wallet = await JsonSerializer.DeserializeAsync<Wallet>(walletJson);
        return (wallet!.PublicKey, wallet.PrivateKey);
    }

    private async Task<string> TransferMatic(string fromPrivateKey, string toPublicKey, double amount)
    {
        var transaction = new { FromPrivateKey = fromPrivateKey, ToPublicKey = toPublicKey, Amount = amount };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/transfers/matic", transaction, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<string> TransferRubble(string fromPrivateKey, string toPublicKey, double amount)
    {
        var transaction = new { FromPrivateKey = fromPrivateKey, ToPublicKey = toPublicKey, Amount = amount };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/transfers/ruble", transaction, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<string> TransferNft(string fromPrivateKey, string toPublicKey, long tokenId)
    {
        var transaction = new { FromPrivateKey = fromPrivateKey, ToPublicKey = toPublicKey, TokenId = tokenId };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/transfers/nft", transaction, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<string> GetTransferStatus(string transactionHash)
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/transfers/status/{transactionHash}", null);
        var statusJson = await response.Content.ReadAsStreamAsync();
        var status = await JsonSerializer.DeserializeAsync<TransactionStatus>(statusJson);
        return status!.Status;
    }
    
    private async Task<(double MaticAmount, double CoinsAmount)> GetWalletsBalance(string publicKey)
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/wallets/{publicKey}/balance", null);
        var amountsJson = await response.Content.ReadAsStreamAsync();
        var amounts = await JsonSerializer.DeserializeAsync<Balances>(amountsJson);
        return (amounts!.MaticAmount, amounts.CoinsAmount);
    }
    
    private async Task<List<(string URI, List<long> tokens)>> GetNftBalance(string publicKey)
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/wallets/{publicKey}/nft/balance", null);
        var balanceJson = await response.Content.ReadAsStreamAsync();
        var balance = await JsonSerializer.DeserializeAsync<NftBalance>(balanceJson);
        return balance!.Balance.Select(nft => (nft.Uri, nft.Tokens.ToList())).ToList();
    }
    
    private async Task<string> GenerateNft(string toPublicKey, string uri, int nftCount)
    {
        var generateNftRequest = new NewNft(toPublicKey, uri, nftCount);
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/nft/generate", generateNftRequest, JsonOptions);
        var transactionResponseJson = await response.Content.ReadAsStreamAsync();
        var transactionResponse = await JsonSerializer.DeserializeAsync<TransactionResponse>(transactionResponseJson);
        return transactionResponse!.TransactionHash;
    }
    
    private async Task<(long TokenId, string URI, string PublicKey)> GetNft(string tokenId)
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/nft/{tokenId}", null);
        var nftJson = await response.Content.ReadAsStreamAsync();
        var nft = await JsonSerializer.DeserializeAsync<Nft>(nftJson);
        return (nft!.TokenId, nft.Uri, nft.PublicKey);
    }

    private async Task<(string WalletId, List<long> Tokens)> GetGeneratedNft(string hash)
    {
        var response = await client.PostAsync($"{BaseUrl}/v1/nft/generate/{hash}", null);
        var generatedNftJson = await response.Content.ReadAsStreamAsync();
        var generatedNft = await JsonSerializer.DeserializeAsync<GeneratedNftTransaction>(generatedNftJson);
        return (generatedNft!.WalletId, generatedNft.Tokens);
    }

    private async Task GetTransactionHistory(string publicKey, int page, int offset, string sort)
    {
        var requestParams = new { Page = page, Offset = offset, Sort = sort };
        var response = await client.PostAsJsonAsync($"{BaseUrl}/v1/wallets/{publicKey}/history", requestParams, JsonOptions);
        throw new NotImplementedException();
    }
}