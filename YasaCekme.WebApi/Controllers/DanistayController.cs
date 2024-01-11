using Microsoft.AspNetCore.Mvc;

namespace YasaCekme.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DanistayController(HttpClient httpClient) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> GetDanistayKarar(DanistayKararSearchRoot request)
    {
        HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("https://karararama.danistay.gov.tr/aramalist", request);

        if(responseMessage.IsSuccessStatusCode)
        {
            DanistayKararResultRoot? result = await responseMessage.Content.ReadFromJsonAsync<DanistayKararResultRoot>();
            //var result = await responseMessage.Content.ReadAsStringAsync();
            return Ok(result);
        }

        return NotFound();
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetDanistayKararDocumentById(string id)
    {
        HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://karararama.danistay.gov.tr/getDokuman?id={id}");

        if(responseMessage.IsSuccessStatusCode)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();
            return Ok(result);
        }

        return NotFound();
    }
}

public record DanistayKararSearchRoot(
    DanistayKararSearchData Data);

public record DanistayKararSearchData(
    List<string> AndKelimeler,
    List<string> OrKelimeler,
    List<string> NotAndKelimeler,
    List<string> NotOrKelimeler,
    int PageSize = 10,
    int PageNumber = 1);

public record DanistayKararResultRoot(
    DanistayKararResultRootData Data,
    Metadata Metadata,
    bool New);

public record DanistayKararResultRootData(
    List<DanistayKararResultData> Data);

public record DanistayKararResultData(
    string Id,
    string DaireKurul,
    string EsasNo,
    string KararNo,
    string KararTarihi,
    string ArananKelime,
    int Index);
