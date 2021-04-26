using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveSampleInCSharp
{
    public class DishDataSource
    {
        static DishDataSource()
        {
            Dishes = new List<string>
            {
                "Viennese Apfelstrudel",
                "Wiener Schnitzel",
                "Vienna Sausage",
                "Knödel",
                "Tafelspitz",
                "Tiroler Gröstl",
                "Käsespätzle",
                "Potato Gulasch",
                "Kaiserschmarrn",
                "Buchteln",
                "Brettljause",
                "Sachertorte",
                "Kardinalschnitte",
                "Fiakergulasch",
                "Martinigans",
                "Mondseer",
                "Spargel",
                "Powidltascherl",
                "Belegte Brote",
                "Topfentascherl"
            }.AsReadOnly();
        }

        public static IReadOnlyList<string> Dishes { get; }
    }
}
