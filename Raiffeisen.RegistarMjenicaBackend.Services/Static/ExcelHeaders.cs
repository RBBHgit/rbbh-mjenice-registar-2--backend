namespace Raiffeisen.RegistarMjenicaBackend.Services.Static;

public static class ExcelHeaders
{
    public static Dictionary<string, string> GetExport()
    {
        var excelFileHeaders = new Dictionary<string, string>
        {
            { "ClientMjenicaSerialNumber", "Serijski broj mjenice klijenta" },
            { "GuarantorMjenicaSerialNumber", "Serijski broj mjenice jemca" },
            { "ClientName", "Klijent" },
            { "JMBG", "JMBG klijenta" },
            { "ContractNumber", "Broj ugovora" },
            { "ContractStatus", "Status ugovora" },
            { "GroupNumber", "Broj partije" },
            { "ContractDate", "Datum ugovora" },
            { "CreatedDate", "Datum kreiranja mjenice" },
            { "IsCreatedVerified", "Mjenica verificirana" }
        };
        return excelFileHeaders;
    }
}