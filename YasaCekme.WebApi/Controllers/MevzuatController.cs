using Microsoft.AspNetCore.Mvc;

namespace YasaCekme.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MevzuatController(HttpClient httpClient) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> GetKanunlar(MevzuatKanunlarSearchRoot request)
    {
        HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync("https://www.mevzuat.gov.tr/Anasayfa/MevzuatDatatable", request);

        if (responseMessage.IsSuccessStatusCode)
        {
            //MevzuatKanunlarResult? result = await responseMessage.Content.ReadFromJsonAsync<MevzuatKanunlarResult>();
            var result = await responseMessage.Content.ReadAsStringAsync();
            return Ok(result);
        }

        return NotFound();
    }
}


public class MevzuatKanunlarResultData
{
    public string MevzuatNo { get; set; } = string.Empty;
    public string MevAdi { get; set; } = string.Empty;
    public string KabulTarih { get; set; } = string.Empty;
    public object? KabulTarihDosyaPath { get; set; }
    public string ResmiGazeteTarihi { get; set; } = string.Empty;
    public string ResmiGazeteSayisi { get; set; } = string.Empty;
    public string MevzuatTertip { get; set; } = string.Empty;
    public object? Nitelik { get; set; }
    public string Mukerrer { get; set; } = string.Empty;
    public object? ResmiGazeteTarihiYilBaslangic { get; set; }
    public object? ResmiGazeteTarihiYilBitis { get; set; }
    public int AranacakYer { get; set; }
    public int MevzuatTur { get; set; }
    public int TuzukMevzuatTur { get; set; }
    public int FileType { get; set; }
    public string MevzuatTurEnumString { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public object? DocUrl { get; set; }
    public object? PdfUrl { get; set; }
    public int Tur { get; set; }
    public bool IsPlainTextBlank { get; set; }
    public string ResmiGazeteTarihiGun { get; set; } = string.Empty;
    public string ResmiGazeteTarihiAy { get; set; } = string.Empty;
    public string ResmiGazeteTarihiYil { get; set; } = string.Empty;
    public object? ResmiGazeteUrl { get; set; }
    public bool HasDoc { get; set; }
    public bool HasOldLaw { get; set; }
    public object? HighlightText { get; set; }
    public object? MevzuatNoCombine { get; set; }
}

public class MevzuatKanunlarResult
{
    public int Draw { get; set; }
    public int RecordsTotal { get; set; }
    public int RecordsFiltered { get; set; }
    //public List<MevzuatKanunlarResultData> Data { get; set; }
    public object? Word { get; set; }
}

public class MevzuatKanunlarSearchColumn
{
    public object? Data;
    public string Name = string.Empty;
    public bool Searchable;
    public bool Orderable;
    public MevzuatKanunlarSearch? Search;
}

public class MevzuatKanunlarSearchParameters
{
    public string MevzuatTur = "Kanun";
    public string YonetmelikMevzuatTur = "OsmanliKanunu";
    public string AranacakIfade = string.Empty;
    public string AranacakYer = "2";
    public string MevzuatNo = string.Empty;
    public string BaslangicTarihi = string.Empty;
    public string BitisTarihi = string.Empty;
    public string Antiforgerytoken = string.Empty;
}

public class MevzuatKanunlarSearchRoot
{
    public int Draw = 1;
    public List<MevzuatKanunlarSearchColumn>? Columns;
    public List<object>? Order;
    public int Start;
    public int Length = 10;
    public MevzuatKanunlarSearch? Search;
    public MevzuatKanunlarSearchParameters? Parameters;
}

public class MevzuatKanunlarSearch
{
    public string Value = string.Empty;
    public bool Regex;
}