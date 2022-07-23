// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var list = new List<int>() { 4704, 4301, 450,451,473,475,47,4 };
Console.WriteLine(GetMaxNumber(list));
list = new List<int>() { 5, 555, 505, 565 };
Console.WriteLine(GetMaxNumber(list));
list = new List<int>() { 407, 3, 109, 5, 108 };
Console.WriteLine(GetMaxNumber(list));
list = new List<int>() { 5, 53, 4 };
Console.WriteLine(GetMaxNumber(list));
list = new List<int>() { 5, 56, 4 };
Console.WriteLine(GetMaxNumber(list));


static string GetMaxNumber(List<int> list)
{
    var maxDigit = list.Max(x => x.ToString().Length);
    var orderedStringNumbers = list
        .OrderByDescending(x => GetNumberWithAddedDigits(x, maxDigit))
        .Select(x => x.ToString());

    return string.Concat(orderedStringNumbers);
}

static int GetNumberWithAddedDigits(int number, int digit)
{
    var stringNumber = number.ToString();
    var headerNumber = stringNumber.First();
    while(stringNumber.Length < digit)
    {
        stringNumber += headerNumber;
    }

    return Convert.ToInt32($"{stringNumber}");
}