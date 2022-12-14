using System.Xml.Linq;
using System.Xml.Serialization;

namespace Laba10.Imp;

[XmlType("Card")]
public class Картка
{
    [XmlElement("Id")]
    public int Айди { get; set; }
    [XmlElement("Score")]
    public int Баллы { get; set; }

    public void МинусБаллы(int списатьБаллыСкока)
    {
        Баллы -= списатьБаллыСкока;
    }
    
    public void ПлюсБаллы(int добавитьБаллыСкока)
    {
        Баллы += добавитьБаллыСкока;
    }

    public static Картка СюдаБыстро(int айдишка)
    {
        FileStream стрим = null;
        
        try
        {
            XmlSerializer сериализатор = new XmlSerializer(typeof(List<Картка>));
            стрим = new FileStream("картки.xml", FileMode.OpenOrCreate);
            List<Картка> десериализируемое = new List<Картка>();
            десериализируемое = сериализатор.Deserialize(стрим) as List<Картка>;
            стрим.Close();
            return десериализируемое.FirstOrDefault(x => x.Айди == айдишка) ?? new Картка();
        }
        catch
        {
            стрим?.Close();
            return new Картка();
        }
    }

    public static List<Картка> СюдаБыстроВсеДон()
    {
        FileStream стрим = null;
        
        try
        {
            XmlSerializer сериализатор = new XmlSerializer(typeof(List<Картка>));
            стрим = new FileStream("картки.xml", FileMode.OpenOrCreate);
            List<Картка> десериализируемое = new List<Картка>();
            десериализируемое = сериализатор.Deserialize(стрим) as List<Картка>;
            стрим.Close();
            return десериализируемое ?? new List<Картка>();
        }
        catch
        {
            стрим?.Close();
            return new List<Картка>();
        }
    }

    public void СеризироватьТуИксСэМэль()
    {
        XmlSerializer сериализатор = new XmlSerializer(typeof(List<Картка>));
        FileStream стрим = null;
        List<Картка> десериализируемое = new List<Картка>();
        
        try
        {
            стрим = new FileStream("картки.xml", FileMode.OpenOrCreate);
            десериализируемое = сериализатор.Deserialize(стрим) as List<Картка>;
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
        стрим = new FileStream("картки.xml", FileMode.OpenOrCreate);
        сериализатор.Serialize(стрим, десериализируемое);
        стрим.Close();
    }
}