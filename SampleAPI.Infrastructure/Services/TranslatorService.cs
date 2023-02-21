using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleAPI.Core.Constants;
using SampleAPI.Infrastructure.DAO;
using SampleAPI.Infrastructure.Endpoints.Translate;
using SampleAPI.Infrastructure.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace SampleAPI.Infrastructure.Services;

//FOR SMALL INTERFACES ONLYI CREATE INTERFACE IN THE SERVICE
public interface ITranslatorService
{
    public Task<TranslateResponse> TranslateToLanguageAsync(TranslateRequest request);
}

internal sealed class TranslatorService : ITranslatorService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;
    public TranslatorService(IConfiguration configuration, IHttpClientFactory httpClientFactory, ApplicationDbContext context)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient(LibreTranslateEndpoints.TRANSLATE_API_CLIENT);
        _context = context;
    }

    public async Task<TranslateResponse> TranslateToLanguageAsync(TranslateRequest request)
    {
        if (string.IsNullOrEmpty(request.Text))
            return null;

        var code = await _context.Languages.FirstOrDefaultAsync(c => c.Code.ToLower().Equals(request.TargetLanguage.ToLower()));

        if (code is null)
            return null;

        string serializedRequest = JsonSerializer.Serialize(new
        {
            q = request.Text,
            source = TranslationEndpointData.SOURCE,
            target = request.TargetLanguage,
            format = TranslationEndpointData.FORMAT,
            api_key = _configuration.GetSection(SectionNameConstants.APP_SETTINGS_SECTION)[LibreTranslateEndpoints.TRANSLATE_API_PASSWORD_KEY ]
        });

        var response = await _httpClient.PostAsync
        (
            LibreTranslateEndpoints.TRANSLATE_URL,
            new StringContent(serializedRequest, Encoding.UTF8,
            MimeTypesConstants.APPLICATION_JSON)
        );

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            //PLACE FOR THE LOG ABOUT WRONG CREDENTIALS
            throw new UnauthorizeConsumerException("Niepoprawne dane autoryzacyjne");
        }

        if (!response.IsSuccessStatusCode)
            throw new CannotTranslateTextException("Błąd tłumaczenia tekstu");

        return JsonSerializer.Deserialize<TranslateResponse>(await response.Content.ReadAsStringAsync());
    }
}
