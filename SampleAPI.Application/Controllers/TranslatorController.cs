using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Infrastructure.Endpoints.Translate;
using SampleAPI.Infrastructure.Services;

namespace SampleAPI.Application.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TranslatorController : ControllerBase
{
    private readonly ITranslatorService _translatorService;
    public TranslatorController(ITranslatorService translatorService)
    {
        _translatorService = translatorService;
    }

    [HttpGet("TranslateToLanguageAsync")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TranslateToLanguageAsync([FromQuery] TranslateRequest request)
    {
        var result = await _translatorService.TranslateToLanguageAsync(request);

        if (result is null)
            return BadRequest("Niepoprawny kod języka lub wprowadzony tekst");

        return Ok(result);
    }
}
