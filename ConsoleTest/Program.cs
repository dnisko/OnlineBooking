using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TestWS;

namespace ConsoleTest
{
    class Program
    {
        //OBContext context = new OBContext();
        static void Main(string[] args)
        {
            OBModel context = new OBModel();

            Console.OutputEncoding = Encoding.UTF8;

            var nas = (from nastan in context.nastans

                       join komintent in context.komintents
                       on nastan.fk_id_komintent equals komintent.id_komintent

                       join objekt in context.objekts
                       on nastan.o_id_objekt equals objekt.id_objekt

                       join karti in context.kartis
                       on nastan.id_nastan equals karti.n_id_nastan into eKarti

                       //group karti by new MyGrouping(
                       //{
                       //    karti.n_id_nastan,
                       //    nastan.data,
                       //    nastan.naziv,
                       //    komintent.opis,
                       //    objekt.ime,
                       //    objekt.grad,
                       //    nastan.vreme,
                       //    //karti.lager

                       //}) into g
                       select new
                       {
                           //n_id_nastan = eKarti,//.Select(x => x.n_id_nastan),
                           Data = nastan.data,
                           Naziv = nastan.naziv,
                           Opis = komintent.opis,
                           Ime = objekt.ime,
                           Grad = objekt.grad,
                           Vreme = nastan.vreme,
                           Lager = eKarti.Sum(x => x.lager)
                       });//.Distinct();

            foreach (var nastan in nas)
            {
                Console.WriteLine(nastan.Ime);

                //foreach (var item in  nastan.Naziv)
                //{
                //    Console.WriteLine(//item.n_id_nastan + "\t" +
                //                      " " + item.Data + "\t" +
                //                      " " + item.Naziv + "\t" +
                //                      //" " + 
                //                      " " + item.Ime + "\t" +
                //                      " " + item.Grad + "\t" +
                //                      " " + item.Vreme + "\t" +
                //                      " " + item.Lager);
                //}
            }
        }
    }
}
