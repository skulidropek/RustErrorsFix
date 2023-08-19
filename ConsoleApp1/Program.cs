using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using RustErrorsFixLibrary.Core;
using RustErrorsFixLibrary.Core.Interface;
using System;
using System.Text.RegularExpressions;

string input = @"
var nw = Net.sv.StartWrite();
nw.Sv();

var nw = Net.sv.StartWrite();
nw.Test();
";

string pattern = @"var nw = Net.sv.StartWrite\(\);";

string output = "var";

foreach(var text in input.Split("var"))
{
    output += text + "\n";
}

//string output = Regex.Replace(input, pattern, "", RegexOptions.Multiline);

Console.WriteLine(output.Replace("\n\n", "\n"));
Console.ReadKey();