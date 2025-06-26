#region Menü Başlangıcı

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text;

Console.BackgroundColor = ConsoleColor.Yellow;
Console.ForegroundColor = ConsoleColor.Red;
Console.Clear();

Console.WriteLine("Api Consume İşlemine Hoş Geldiniz");
Console.WriteLine();
Console.WriteLine("### Yapmak İstediğiniz İşlemi Seçin ###");
Console.WriteLine();
Console.WriteLine("1-Şehir Listesini Getirin");
Console.WriteLine("2-Şehir ve Hava Durumu Listesini Getirin");
Console.WriteLine("3-Yeni Şehir Ekleme");
Console.WriteLine("4-Şehir Silme İşlemi");
Console.WriteLine("5-Şehir Güncelleme İşlemi");
Console.WriteLine("6-ID'ye Göre Şehir Getirme");
Console.WriteLine("7-En Sıcak Şehri Getirme:");
Console.WriteLine("8-En Soğuk Şehri Getirme:");
Console.WriteLine("9-Güneşli Şehir Listesi Getirme:");
Console.WriteLine("10-Çıkış Yap");
Console.WriteLine();

#endregion


string number;

Console.Write("Tercihiniz: ");
number = Console.ReadLine();
Console.WriteLine();

if (number == "1")
{
    string url = "https://localhost:7184/api/Weathers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        foreach (var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            Console.WriteLine($"Şehir: {cityName}");
        }
    }
}
if (number == "2")
{
    string url = "https://localhost:7184/api/Weathers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        foreach (var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            string temp = item["temp"].ToString();
            string country = item["country"].ToString();
            Console.WriteLine(cityName + "-" + country + "-->" + temp + "°C");
            Console.WriteLine("-------------------------------------------------------");
        }
    }
}
if (number == "3")
{
    Console.WriteLine("### Yeni Veri Girişi ###");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;

    Console.Write("Şehir Adı: ");
    cityName = Console.ReadLine();

    Console.Write("Ülke Adı: ");
    country = Console.ReadLine();

    Console.Write("Detay: ");
    detail = Console.ReadLine();

    Console.Write("Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine());

    string url = "https://localhost:7184/api/Weathers";
    var newWeatherCity = new
    {
        CityName = cityName,
        Country = country,
        Detail = detail,
        Temp = temp
    };

    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(newWeatherCity);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
    }
}
if (number == "4")
{
    Console.WriteLine("### Şehir Silme İşlemi ###");
    Console.WriteLine();
    Console.Write("Silmek istediğiniz Şehir ID: ");
    int id = int.Parse(Console.ReadLine());
    string url = $"https://localhost:7184/api/Weathers?id=";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.DeleteAsync(url + id);
        response.EnsureSuccessStatusCode();
        Console.WriteLine("Şehir başarıyla silindi.");
    }
}
if (number == "5")
{
    Console.WriteLine("### Şehir Güncelleme İşlemi ###");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;
    int cityId;

    Console.Write("Şehir Adı: ");
    cityName = Console.ReadLine();

    Console.Write("Ülke Adı: ");
    country = Console.ReadLine();

    Console.Write("Detay: ");
    detail = Console.ReadLine();

    Console.Write("Sıcaklık: ");
    temp = decimal.Parse(Console.ReadLine());

    Console.Write("Şehir Id: ");
    cityId = int.Parse(Console.ReadLine());

    string url = "https://localhost:7184/api/Weathers";
    var updatedWeatherValues = new
    {
        CityId = cityId,
        CityName = cityName,
        Country = country,
        Detail = detail,
        Temp = temp
    };
    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(updatedWeatherValues);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
        Console.WriteLine("Şehir başarıyla güncellendi.");
    }
}
if (number == "6")
{
    string url = "https://localhost:7184/api/Weathers/GetByIdWeatherCity?id=";

    Console.Write("Bilgilerini Getirmek İstediğiniz Id Değeri: ");
    int id = int.Parse(Console.ReadLine());
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url + id);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject jObject = JObject.Parse(responseBody);
        string cityName = jObject["cityName"].ToString();
        string country = jObject["country"].ToString();
        string detail = jObject["detail"].ToString();
        decimal temp = decimal.Parse(jObject["temp"].ToString());
        Console.WriteLine();
        Console.WriteLine("Girmiş olduğunuz Id değerine ait bilgiler");
        Console.WriteLine();
        Console.WriteLine($"Şehir: {cityName}, Ülke: {country}, Detay: {detail}, Sıcaklık: {temp}°C");
        
       
    }
}
if (number == "7")
{
    string url = "https://localhost:7184/api/Weathers/MaxTempCityName";
   
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("En Sıcak Şehir: " + responseBody);
    }
}
if (number == "8")
{
    string url = "https://localhost:7184/api/Weathers/MinTempCityName";

    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("En Soğuk Şehir: " + responseBody);
    }
}
if (number == "9")
{
    string url = "https://localhost:7184/api/Weathers/SunnyCities";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        JArray jArray = JArray.Parse(responseBody);

        foreach (var item in jArray)
        {
            string cityName = item.ToString();
            Console.WriteLine(cityName);
        }
    }
}
if (number == "10")
{
    Console.WriteLine("Çıkış Yapılıyor...");
    Console.WriteLine("İyi Günler Dileriz...");
    return;
}
else
{
    Console.WriteLine("Geçersiz bir işlem yaptınız. Lütfen tekrar deneyin.");
}

Console.WriteLine();
Console.WriteLine();