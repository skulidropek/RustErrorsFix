using ConsoleApp1;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using RustErrorsFixLibrary.Core.Model;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

string code = File.ReadAllText("D:\\Download\\BetterResearching.cs");

HttpClient httpClient = new HttpClient();
string apiKey = "sk-95Ez2Z1rtgegrRTFFDSTVTdsdfsgdv3422kjhLghnh53QiT8F";
string endpoint = "http://localhost:5128/openai/v1/chat/completions";
List<Message> messages = new List<Message>();

//httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

AddMessage("Представь что ты разработчик плагинов для раст используя модификацию Oxide C#");

foreach(var path in Directory.GetFiles("D:\\Download\\PluginsGPT"))
{
    var file = await File.ReadAllTextAsync(path);

    AddMessage("Это код плагина " + Regex.Match(file, @"\[Info\((.+),.+,.+\)\]").Groups[1].ToString());

    //foreach (var code4090 in file.Split(4000))
    //{
    //    AddMessage(code4090);
    //}
}

var request = await Request("Проанализируй плагины которые я тебе отправил");
Console.WriteLine(request);
Console.ReadLine();
//SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
//CompilationUnitSyntax root = syntaxTree.GetCompilationUnitRoot();
//var diagnostics = syntaxTree.GetDiagnostics();

//foreach (var diagnostic in CreateCompilation(syntaxTree, "Plugin", "C:\\Server Rust\\RustDedicated_Data\\Managed").GetDiagnostics())
//{
//    if (diagnostic.DefaultSeverity == DiagnosticSeverity.Error)
//    {
//        var location = diagnostic.Location;
//        var lineSpan = location.GetLineSpan();

//        SourceText sourceText = location.SourceTree.GetText();
//        string lineText = sourceText.Lines.GetLineFromPosition(location.SourceSpan.Start).ToString();

//        Console.WriteLine(diagnostic.ToString());

//        var text = Request(
//            $"Твоя роль это разработчик плагинов для игры Rust используя модификацию Oxide и язык программирования C#" +
//            $"Что не так с {lineText}\n" +
//            $"Ошибка {diagnostic.GetMessage()}\n").GetAwaiter().GetResult();

//        Console.WriteLine(text);
//    }
//}

void AddMessage(string content)
{
    var message = new Message() { Role = "user", Content = content };
    // добавляем сообщение в список сообщений
    messages.Add(message);
}

async Task<string> Request(string content)
{

    var message = new Message() { Role = "user", Content = content };
    // добавляем сообщение в список сообщений
    messages.Add(message);

    // формируем отправляемые данные
    var requestData = new Request()
    {
        ModelId = "gpt-3.5-turbo",
        Messages = messages
    };
    // отправляем запрос
    using var response = await httpClient.PostAsJsonAsync(endpoint, requestData);

    // если произошла ошибка, выводим сообщение об ошибке на консоль
    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"{(int)response.StatusCode} {response.StatusCode}");
        return "";
    }
    // получаем данные ответа
    var responseData = await response.Content.ReadFromJsonAsync<ResponseData>();

    var choices = responseData?.choices ?? new List<Choice>();
    if (choices.Count == 0)
    {
        Console.WriteLine("No choices were returned by the API");
        return "";
    }
    var choice = choices[0];
    var responseMessage = choice.message;
    // добавляем полученное сообщение в список сообщений
    messages.Add(responseMessage);
    var responseText = responseMessage.Content.Trim();

    return responseText;
}

CSharpCompilation CreateCompilation(SyntaxTree source, string compilationName, string managedFolder)
{
    List<MetadataReference> references = new List<MetadataReference>();

    foreach (var path in Directory.GetFiles(managedFolder).Where(f => !f.Contains("Newtonsoft.Json.dll")))
    {
        references.Add(MetadataReference.CreateFromFile(path.Replace("\n", "").Replace("\r", "")));
    }

    return CSharpCompilation.Create(compilationName,
            syntaxTrees: new[]
            {
                    source
            },
            references: references,
            options: new CSharpCompilationOptions(Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary)
        );
}