using AuctionHouseDataAnalyser;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            start();
            //end();
            //GC.Collect();
            GC.Collect();
            Console.WriteLine(GC.GetTotalMemory(true).ToString());
            Console.Read();
            //int fin = 100;
            //do
            //{
            //    fin = Console.Read();
            //} while (fin != 0);
            
        }

        static void start()
        {
            Console.WriteLine(GC.GetTotalMemory(true).ToString());
            Analyser analyse = new Analyser();
            Console.WriteLine(GC.GetTotalMemory(true).ToString());
            
        }

        static void end()
        {
            using (Test test = new Test())
            {

            }
            Console.WriteLine(GC.GetTotalMemory(true).ToString());
            Console.WriteLine("Fin");
            
        }
    }

    public class Test : IDisposable 
    {
        public Test()
        {
            Console.WriteLine("initialized");
            Analyser analyse = new Analyser();
            GC.Collect();
        }
        ~Test()
        {
            Console.WriteLine("Collected");
            Console.WriteLine(GC.GetTotalMemory(true).ToString());
            Dispose(false);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                    //GC.Collect();
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.
                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~Test() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
