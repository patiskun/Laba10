using System.Xml.Linq;
using System.Xml.Serialization;

namespace Laba10.Imp;

public class ПользовательДТО_АДО
{
    public int Айди { get; set; }
    public string Фио { get; set; }
    public int АйдиКартка { get; set; }

    public Картка ПолучитьКартку(int айдиКартка)
    {
        return Картка.СюдаБыстро(айдиКартка);
    }
    
    public void СеризироватьТуИксСэМэль()
    {
        XmlSerializer сериализатор = new XmlSerializer(typeof(List<ПользовательДТО_АДО>));
        FileStream стрим = null;
        List<ПользовательДТО_АДО> десериализируемое = new List<ПользовательДТО_АДО>();
        
        try
        {
            стрим = new FileStream("челы.xml", FileMode.OpenOrCreate);
            десериализируемое = сериализатор.Deserialize(стрим) as List<ПользовательДТО_АДО>;
            стрим.Close();
        }
        catch
        { 
            //не ну а хули нам русским
            стрим?.Close();
        }

        var индексАло = десериализируемое.FindIndex(x => x.Айди == Айди);
        
        if (индексАло != -1)
        {
            десериализируемое.RemoveAt(индексАло);
        }
        
        десериализируемое.Add(this);
        стрим = new FileStream("челы.xml", FileMode.OpenOrCreate);
        сериализатор.Serialize(стрим, десериализируемое);
        стрим.Close();
    }

    public static List<ПользовательДТО_АДО> СюдаВсе()
    {
        var сериализатор = new XmlSerializer(typeof(List<ПользовательДТО_АДО>));
        
        if (!File.Exists("челы.xml"))
        {
            var стрим = File.CreateText("челы.xml");
            сериализатор.Serialize(стрим, new List<ПользовательДТО_АДО>());
            стрим.Close();
        }
        
        var документ = XDocument.Load("челы.xml");
        return (List<ПользовательДТО_АДО>)сериализатор.Deserialize(документ.CreateReader())!;
    }
}