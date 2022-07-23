
foreach (var item in foo(10,12))
{
    Console.WriteLine($"{item.Item1}, {item.Item2}");
}


static IEnumerable<(int, int)> foo(int n, int v)
{
    return Enumerable.Range(0, n + 1).Where((i) => Enumerable.Range(0, n + 1).Any(j => j + i == v)).Select(i => (i, v - i));
}