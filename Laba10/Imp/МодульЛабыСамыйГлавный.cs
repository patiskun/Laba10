using LanguageExt.Pretty;
using Spectre.Console;
using System.Data.Common;
using System.Reflection;

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
            
            typeof(AnsiConsole).GetMethod("Write", BindingFlags.Public | BindingFlags.Static, null, new Type[] {typeof(Panel)}, null)!
                .Invoke(null, new object[] {menuPanel});

            var выборМеню = string.Empty;

            while (ВсеЛиВерно(выборМеню) is not true)
            {
                typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                    .Invoke(null, new object[] { "Выберите нужный номер из меню." });
                выборМеню = (string)typeof(Console).GetMethod("ReadLine", new Type[] {}, null)!
                    .Invoke(null, new object[] {})?.ToString().Trim();
            }

            switch (выборМеню)
            {
                case "1":
                {
                    var табличка = Activator.CreateInstance<Table>();

                    табличка.AddColumn(new TableColumn("Айди").LeftAligned().Width(20));
                    табличка.AddColumn(new TableColumn("ФИО").LeftAligned());
                    табличка.AddColumn(new TableColumn("Айди карточки").LeftAligned());
                    табличка.AddColumn(new TableColumn("Баллы").LeftAligned());
                    var челы = ПользовательДТО_АДО.СюдаВсе();

                    foreach (var чел in челы)
                    {
                       табличка.AddRow(чел.Айди.ToString(), чел.Фио, чел.АйдиКартка.ToString(), Картка.СюдаБыстро(чел.АйдиКартка).Баллы.ToString());
                    }

                    typeof(AnsiConsole).GetMethod("Write", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(Table) }, null)
                            .Invoke(null, new object[] { табличка });
                    }
                    break;
                case "2":
                {
                    var табличка = Activator.CreateInstance<Table>();

                    табличка.AddColumn(new TableColumn("Айди").LeftAligned().Width(20));
                    табличка.AddColumn(new TableColumn("Баллы").LeftAligned());
                    var картки = Картка.СюдаБыстроВсеДон();

                    foreach (var картка in картки)
                    {
                        табличка.AddRow(картка.Айди.ToString(), картка.Баллы.ToString());
                    }

                    typeof(AnsiConsole).GetMethod("Write", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(Table) }, null)
                            .Invoke(null, new object[] { табличка });
                }
                    break;
                case "3":
                {
                    var фио = string.Empty;

                    while (string.IsNullOrWhiteSpace(фио))
                    {

                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Введите ФИО пользователя:" });

                        фио = (string)typeof(Console).GetMethod("ReadLine", new Type[] {}, null)!
                                .Invoke(null, new object[] {})?.ToString().Trim();
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
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Введите Айди пользователя:" });
                        узерАйди = (string)typeof(Console).GetMethod("ReadLine", new Type[] { }, null)!
                                .Invoke(null, new object[] { })?.ToString().Trim();

                    }

                    var пользователь = ПользовательДТО_АДО.СюдаВсе().FirstOrDefault(x => x.Айди == узерАйдиИнт);
                    
                    if (пользователь == null)
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Такого пользователя нету!" });
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
                        
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Карточка добавлена!" });
                    }
                    else
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Карточка уже существует!" });
                    }
                }
                    break;
                case "5":
                {
                    var карткаАйди = string.Empty;
                    int карткаАйдиИнт;
                    while (!int.TryParse(карткаАйди, out карткаАйдиИнт))
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Введите Айди карточки:" });
                        карткаАйди = (string)typeof(Console).GetMethod("ReadLine", new Type[] { }, null)!
                                .Invoke(null, new object[] { })?.ToString().Trim();
                    }

                    var картка = Картка.СюдаБыстроВсеДон().FirstOrDefault(x => x.Айди == карткаАйдиИнт);
                    
                    if (картка == null)
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Такой карточки нет!" });
                        continue;
                    }
                    
                    typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                            .Invoke(null, new object[] { "Введите баллы:" });
                    if (int.TryParse(Console.ReadLine(), out var баллы))
                    {
                        картка.МинусБаллы(баллы);
                        картка.СеризироватьТуИксСэМэль();
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Баллы успешно списаны!" });
                    }
                    else
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Вы ввели неверное значение баллов!" });
                    }
                }
                    break;
                case "6":
                {
                    var карткаАйди = string.Empty;
                    int карткаАйдиИнт;
                    while (!int.TryParse(карткаАйди, out карткаАйдиИнт))
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Введите Айди карточки:" });
                        карткаАйди = (string)typeof(Console).GetMethod("ReadLine", new Type[] { }, null)!
                                .Invoke(null, new object[] { })?.ToString().Trim();
                    }

                    var картка = Картка.СюдаБыстроВсеДон().FirstOrDefault(x => x.Айди == карткаАйдиИнт);
                    
                    if (картка == null)
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Такой карточки нет!" });
                        continue;
                    }

                    typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                            .Invoke(null, new object[] { "Введите баллы:" });

                    if (int.TryParse(Console.ReadLine(), out var баллы))
                    {
                        картка.ПлюсБаллы(баллы);
                        картка.СеризироватьТуИксСэМэль();
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Баллы успешно списаны!" });
                    }
                    else
                    {
                        typeof(AnsiConsole).GetMethod("WriteLine", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null)!
                                .Invoke(null, new object[] { "Вы ввели неверное значение баллов!" });
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