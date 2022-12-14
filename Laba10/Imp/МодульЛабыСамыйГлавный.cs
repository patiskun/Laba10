using LanguageExt.Pretty;
using Spectre.Console;

namespace Laba10.Imp;

public class МодульЛабыСамыйГлавный
{
    public static void ПрограммаРаботайДон()
    {
        while (true)
        {
            var menuPanel = new Panel("1) Показать всех пользователей\n2) Показать все карточки\n3) Добавить пользователя\n4) Добавить карточку\n5) Списать баллы\n6) Добавить баллы\n7) Выход")
            {
                Border = BoxBorder.Double,
                Padding = new Padding(1, 1, 1, 1),
                Header = new PanelHeader("Меню", Justify.Center)
            };
            
            AnsiConsole.Write(menuPanel);
    
            var выборМеню = string.Empty;

            while (ВсеЛиВерно(выборМеню) is not true)
            {
                AnsiConsole.WriteLine("Выберите нужный номер из меню.");
                выборМеню = Console.ReadLine()?.Trim();
            }

            switch (выборМеню)
            {
                case "1":
                {
                    var табличка = new Table();

                    табличка.AddColumn(new TableColumn("Айди").LeftAligned().Width(20));
                    табличка.AddColumn(new TableColumn("ФИО").LeftAligned());
                    табличка.AddColumn(new TableColumn("Айди карточки").LeftAligned());
                    табличка.AddColumn(new TableColumn("Баллы").LeftAligned());
                    var челы = ПользовательДТО_АДО.СюдаВсе();

                    foreach (var чел in челы)
                    {
                        табличка.AddRow(чел.Айди.ToString(), чел.Фио, чел.АйдиКартка.ToString(), Картка.СюдаБыстро(чел.АйдиКартка).Баллы.ToString());
                    }

                    AnsiConsole.Write(табличка);
                }
                    break;
                case "2":
                {
                    var табличка = new Table();

                    табличка.AddColumn(new TableColumn("Айди").LeftAligned().Width(20));
                    табличка.AddColumn(new TableColumn("Баллы").LeftAligned());
                    var картки = Картка.СюдаБыстроВсеДон();

                    foreach (var картка in картки)
                    {
                        табличка.AddRow(картка.Айди.ToString(), картка.Баллы.ToString());
                    }

                    AnsiConsole.Write(табличка);
                }
                    break;
                case "3":
                {
                    var фио = string.Empty;

                    while (string.IsNullOrWhiteSpace(фио))
                    {
                        AnsiConsole.WriteLine("Введите ФИО пользователя:");
                        фио = Console.ReadLine()?.Trim();
                    }
                    
                    new ПользовательДТО_АДО { Айди = Random.Shared.Next(), Фио = фио }.СеризироватьТуИксСэМэль();
                }
                    break;
                case "4":
                {
                    var узерАйди = string.Empty;
                    int узерАйдиИнт;
                    while (!int.TryParse(узерАйди, out узерАйдиИнт))
                    {
                        AnsiConsole.WriteLine("Введите Айди пользователя:");
                        узерАйди = Console.ReadLine()?.Trim();
                    }

                    var пользователь = ПользовательДТО_АДО.СюдаВсе().FirstOrDefault(x => x.Айди == узерАйдиИнт);
                    
                    if (пользователь == null)
                    {
                        AnsiConsole.WriteLine("Такого пользователя нету!");
                        continue;
                    }

                    var картки = Картка.СюдаБыстроВсеДон();
                    var карткаНайтиЧиНетуЕе = картки.FirstOrDefault(x => x.Айди == пользователь.АйдиКартка);
                    
                    if (карткаНайтиЧиНетуЕе == null)
                    {
                        var карткаНовая = new Картка { Айди = Random.Shared.Next(), Баллы = 0 };
                        пользователь.АйдиКартка = карткаНовая.Айди;
                        
                        пользователь.СеризироватьТуИксСэМэль();
                        карткаНовая.СеризироватьТуИксСэМэль();
                        
                        AnsiConsole.WriteLine("Карточка добавлена!");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Карточка уже существует!");
                    }
                }
                    break;
                case "5":
                {
                    var карткаАйди = string.Empty;
                    int карткаАйдиИнт;
                    while (!int.TryParse(карткаАйди, out карткаАйдиИнт))
                    {
                        AnsiConsole.WriteLine("Введите Айди карточки:");
                        карткаАйди = Console.ReadLine()?.Trim();
                    }

                    var картка = Картка.СюдаБыстроВсеДон().FirstOrDefault(x => x.Айди == карткаАйдиИнт);
                    
                    if (картка == null)
                    {
                        AnsiConsole.WriteLine("Такой карточки нет!");
                        continue;
                    }
                    
                    AnsiConsole.WriteLine("Введите баллы:");
                    if (int.TryParse(Console.ReadLine(), out var баллы))
                    {
                        картка.МинусБаллы(баллы);
                        картка.СеризироватьТуИксСэМэль();
                        AnsiConsole.WriteLine("Баллы успешно списаны!");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Вы ввели неверное значение баллов!");
                    }
                }
                    break;
                case "6":
                {
                    var карткаАйди = string.Empty;
                    int карткаАйдиИнт;
                    while (!int.TryParse(карткаАйди, out карткаАйдиИнт))
                    {
                        AnsiConsole.WriteLine("Введите Айди карточки:");
                        карткаАйди = Console.ReadLine()?.Trim();
                    }

                    var картка = Картка.СюдаБыстроВсеДон().FirstOrDefault(x => x.Айди == карткаАйдиИнт);
                    
                    if (картка == null)
                    {
                        AnsiConsole.WriteLine("Такой карточки нет!");
                        continue;
                    }

                    AnsiConsole.WriteLine("Введите баллы:");
                    if (int.TryParse(Console.ReadLine(), out var баллы))
                    {
                        картка.ПлюсБаллы(баллы);
                        картка.СеризироватьТуИксСэМэль();
                        AnsiConsole.WriteLine("Баллы успешно списаны!");
                    }
                    else
                    {
                        AnsiConsole.WriteLine("Вы ввели неверное значение баллов!");
                    }
                }
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                
            }
        }
    }
    
    static bool ВсеЛиВерно(string input) => input switch { "1" or "2" or "3" or "4" or "5" or "6" or "7" => true, _ => false };
}