using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHangman {
    class Program {
        static void Main (string[] args) {
            //-----------------------------------------------------ETAPA DE INICIALIZACION
            //Lectura de la palabra del input de usuario
            string palabra = Console.ReadLine ();
            //Creando la palabra escondida y llenandola de *s
            string palabraEscondida = "";
            foreach (char c in palabra) {
                palabraEscondida += "*";
            }
            //--------------------------------------------------------------------------//

            //---------------------------------------------------------ETAPA DE DESARROLLO
            while (palabraEscondida.Contains('*')) {
                //Limpio pantalla y muestro la palabra escondida
                Console.Clear ();
                Console.WriteLine (palabraEscondida);
                char letra = Console.ReadLine ()[0];
                if (palabra.Contains (letra)) {
                    string palabraTemporal = "";
                    for (int i = 0; i < palabra.Length; i++) {
                        if (palabra[i] == letra) {
                            palabraTemporal += letra;
                        } else {
                            palabraTemporal += palabraEscondida[i];
                        }
                    }
                    palabraEscondida = palabraTemporal;
                }
            }
            //Limpio pantalla y muestro la palabra escondida
            Console.Clear ();
            Console.WriteLine (palabraEscondida);
            Console.WriteLine ("FELICIDADES!");
            Console.ReadKey ();
            //---------------------------------------------------------------------------//
        }
    }
}
