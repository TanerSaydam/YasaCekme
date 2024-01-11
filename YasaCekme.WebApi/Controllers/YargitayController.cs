using Microsoft.AspNetCore.Mvc;

namespace YasaCekme.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class YargitayController(HttpClient httpClient) : ControllerBase
{
    [HttpGet("[action]/{search}")]
    public async Task<IActionResult> GetYargiyatResult(string search)
    {
        YargitaySearch yargitaySearch = new(new(search, search));
        HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync<YargitaySearch>("https://karararama.yargitay.gov.tr/aramalist", yargitaySearch);

        if (responseMessage.IsSuccessStatusCode)
        {
            YargitaySearchResult? result = await responseMessage.Content.ReadFromJsonAsync<YargitaySearchResult>();
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetYargitayDocumenById(string id)
    {
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://karararama.yargitay.gov.tr/getDokuman?id={id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            YargitayDokumanSearch? result = await responseMessage.Content.ReadFromJsonAsync<YargitayDokumanSearch>();
            return Ok(result);
        }

        return NotFound();
    }
}

public record YargitaySearch(
    YargitaySearchData Data);

public record YargitaySearchData(
    string Aranan,
    string ArananKelime,
    int PageNumber = 1,
    int PageSize = 10);

public record YargitaySearchResult(
    YargitarSearchResultDataData Data,
    Metadata Metadata
    );

public record YargitarSearchResultDataData(
    List<YargitarSearchResultData> Data);
public record YargitarSearchResultData(
    string Id,
    string Daire,
    string EsasNo,
    string KararNo,
    string KararTarihi,
    string ArananKelime,
    int Index);

public record Metadata(
    string FMTY,
    string FMC,
    string FMTE,
    string FMU,
    string PTID,
    string TID,
    string SID);

public record YargitayDokumanSearch(
    string Data,
    Metadata Metadata);