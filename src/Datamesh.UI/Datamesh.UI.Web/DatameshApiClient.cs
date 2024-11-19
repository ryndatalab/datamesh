

using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;

namespace Datamesh.UI.Web;

public class DatameshApiClient 
{
    public readonly IGraphQLClient _client;
    public DatameshApiClient(IGraphQLClient client)
    {
        _client = client;
    } 
    public async Task<string> GetAllDatameshApiCurrency()
    {
        var query = new GraphQL.GraphQLRequest
        {
            Query = @"
          query health {
                  isLive
                }
        "
        };

        var response = await _client.SendQueryAsync<string>(query);

        return response.Data;
    }


    public async Task<List<DatameshApiCurrency>> GetAllDatameshApiCurrencyx()
    {
        var query = new GraphQL.GraphQLRequest
        {
            Query = @"
          query health {
                  isLive
                }
        "
        };

        var response = await _client.SendQueryAsync<List<DatameshApiCurrency>>(query);

        return response.Data;
    }

//    public async Task<string> LiveCheckAsync(CancellationToken cancellationToken = default)
//    {
//        List<WeatherForecast>? forecasts = null;

//        var result = await httpClient.PostAsJsonAsync<string>("", @" query health 
//{ isLive }   ", cancellationToken);
//        return await result.Content.ReadAsStringAsync();

//    }
//    public async Task<OperationResult> FetchCountries()
//    {
//        GraphQLHttpClient _graphqlClient = new GraphQLHttpClient(new("https+http://dmsapi/graphql"), new);

//        var _fetchCurrencyQuery1 = new GraphQLHttpRequest(@"
//          query health {
//                  isLive
//                }
//        ");

//        GraphQLHttpResponse fetchQuery = await _graphqlClient.SendAsync(_fetchCurrencyQuery1);// .SendQueryAsync<DatameshApiCurrency>(_fetchCurrencyQuery);
//        fetchQuery.get
//         OperationResult operationResult = await fetchQuery.ReadAsResultAsync();
//        return operationResult;
//    }
}

public record DatameshApiCurrency(Guid Id, string? Label);