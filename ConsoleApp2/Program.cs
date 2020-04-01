using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            valida();
        }

        public static void valida()
        {
            string salida = string.Empty;
            try
            {
                //salida = compress("aaaabbdxy$$$&&");
                //salida = encode("Hello my friend funsy");
                //salida = findSum("magicSquare([[40,9,2],[3,5,7],[8,1,6]])");
                //salida = cipher(" lazy dog 12345 :) * zzzZZZAAAaaa");
                //salida = simpleSerie("12345");
                //salida = magicSquare(new int[,] { { 4, 9, 2} ,{ 3,5,7}, { 8,1,6}}).ToString();
                salida = specialMerge( new int [] { 4, 9, -30, 12, 6, 3, 2 }, new int[] { 3, -1, 7, 100, 1599 }).ToString();
                Console.WriteLine(salida);
                Console.ReadLine();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static string compress(string entrada)
        {
            string respuesta = string.Empty;
            int contador = 0;

            try
            {
                #region solucion1
                //List<string> listCadena = new List<string>();

                //HashSet<char> obj = new HashSet<char>();
                //foreach (char character in entrada.ToCharArray())
                //    obj.Add(character);

                //foreach (char character in obj)
                //{
                //    contador = 0;
                //    foreach (char charAux in entrada.ToCharArray())
                //    {
                //        if (character.Equals(charAux))
                //            contador++;
                //    }
                //    listCadena.Add(contador.ToString() + character);
                //}
                //respuesta = string.Join("", listCadena.ToArray());
                #endregion solucion1

                List<string> listCadena = new List<string>();

                foreach (char character in entrada.ToCharArray())
                    listCadena.Add(character.ToString());

                //var result =  listCadena.GroupBy(x => x).Select((item, indice) => new {indice, text = item.Key, cantidad = item.Count() }).ToList() ;
                //result.ForEach(x => respuesta += x.text + x.cantidad);

                foreach (var items in listCadena.GroupBy(x => x))
                    respuesta += items.Count() + items.Key;

            }
            catch (Exception ex)
            {

                throw;
            }
            return respuesta;
        }

        public static string encode(string entrada)
        {
            string respuesta = string.Empty;
            List<string> lsWords = new List<string>();
            List<string> lsWordsResult = new List<string>();
            string strPalabra = string.Empty;
            int numItem = 0;
            string nuevaPalabra = string.Empty;
            try
            {
                //1 separar las palabras de la cadena sin split
                foreach (char c in entrada)
                {
                    //2 concatenar los caracrteres hasta encontrar un espacio
                    if (c.Equals(' '))
                    {
                        lsWords.Add(strPalabra);//3 agregar la palabra a la lista
                        strPalabra = string.Empty;
                    }
                    else
                        strPalabra += c;    
                }
                lsWords.Add(strPalabra);
                //4 iterar la lista y cada palabra empezar por el ulitmo caracter y concatenar para atras

                foreach (string item in lsWords)
                {
                    
                    var itempalabra = item.ToList().Select((chars,index) => new { index, text = chars.ToString() }).ToList();
                    numItem = itempalabra.Count - 1;
                    while (numItem>=0)
                    {
                        nuevaPalabra += itempalabra[numItem].text;
                        numItem--;
                    }
                    lsWordsResult.Add(nuevaPalabra);
                    nuevaPalabra = string.Empty;
                }

                lsWords.Clear();
                 
                respuesta = string.Join(" ", lsWordsResult);
                lsWordsResult.Clear();
                respuesta = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(respuesta);

            }
            catch (Exception)
            {

                throw;
            }
            return respuesta;
        }

        public static string findSum(string entrada)
        {
            string respuesta = string.Empty;
            int suma = 0;
            int indice = -1;
            string sumando = "0";
            List<char> lsEntrada = new List<char>();
            try
            {
                var items = entrada.ToList().Select((item, index) => new { index, text = item.ToString() });

                foreach (var c in items)
                {

                    if (char.IsDigit(c.text, 0))
                    {
                        if (indice == c.index - 1 && char.IsDigit(items.Where(x => x.index == indice).FirstOrDefault().text, 0))
                            sumando += c.text;
                        else
                            sumando = c.text;
                    }
                    else
                    {
                        suma += int.Parse(sumando);
                        sumando = "0";
                    }
                    
                    indice = c.index;
                }
                respuesta = suma.ToString();
            }
            catch (Exception)      
            {
                respuesta = "0";
            }
            return respuesta;
        }

        public static string cipher(string entrada)
        {
            string respuesta = string.Empty;
            List<string> abcMin = new List<string>();
            List<string> abcMay = new List<string>();
            string letterCipher = string.Empty;

            try
            {
                //obetener el abecedario
                char letra = 'a';
                do
                {
                    abcMin.Add(letra.ToString());
                    letra++;
                }
                while(letra <= 'z');

                letra = 'A';
                do
                {
                    abcMay.Add(letra.ToString());
                    letra++;
                }
                while (letra <= 'Z');


                var minisculas = abcMin.Select((item, indidce) => new { index = indidce + 1, text = item });
                var mayusculas = abcMay.Select((item, indidce) => new { index = indidce + 1, text = item });

                foreach (var c in entrada.ToList().Select((item, index) => new { indice = index + 1, text = item.ToString() }).ToList())
                {

                    if (c.indice % 2 == 0)
                    {

                        if (minisculas.Any(x => x.text.Equals(c.text)))
                        {
                            letterCipher = minisculas.Any(y => y.index == minisculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index - 1) ? minisculas.Where(y => y.index == minisculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index - 1).FirstOrDefault().text : null;
                            letterCipher = letterCipher == null ? (c.text == "a"? "z": "a" ) : letterCipher;
                        }
                        else if (mayusculas.Any(x => x.text.Equals(c.text)))
                        {
                            letterCipher = mayusculas.Any(y => y.index == mayusculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index - 1) ? mayusculas.Where(y => y.index == mayusculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index - 1).FirstOrDefault().text : null;
                            letterCipher = letterCipher == null ? (c.text == "A" ? "Z" : "A") : letterCipher;
                        }
                        else
                            letterCipher = c.text;
                    }
                    else
                    {
                        if (minisculas.Any(x => x.text.Equals(c.text)))
                        {
                            letterCipher = minisculas.Any(y => y.index == minisculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index + 1) ? minisculas.Where(y => y.index == minisculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index + 1).FirstOrDefault().text : null;
                            letterCipher = letterCipher == null ? (c.text == "a" ? "z" : "a") : letterCipher;
                        }
                        else if (mayusculas.Any(x => x.text.Equals(c.text)))
                        {
                            letterCipher = mayusculas.Any(y => y.index == mayusculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index + 1) ? mayusculas.Where(y => y.index == mayusculas.Where(x => x.text.Equals(c.text)).FirstOrDefault().index + 1).FirstOrDefault().text : null;
                            letterCipher = letterCipher == null ? (c.text == "A" ? "Z" : "A") : letterCipher;
                        }
                        else
                            letterCipher = c.text;
                    }
                    respuesta += letterCipher;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return respuesta;
        }

        public static string simpleSerie(string entrada)
        {
            string respuesta = string.Empty;
            try
            {
                respuesta = (Math.Pow(Int32.Parse(entrada), 2) + 1).ToString();
            }
            catch (Exception)
            {

                throw;
            }
            return respuesta;
        }

        public static bool magicSquare(int[,] matriz)
        {
            bool resultado = false;
            List<int> resultadoFilas = new List<int>();
            List<int> resultadoColumnas = new List<int>();
            List<int> resultadoDiagonal = new List<int>();
            int sumFila=0, sumCol = 0, sumDiag = 0;

            try
            {
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    for (int j = 0; j < matriz.GetLength(1); j++)
                    {
                        sumFila += matriz[i, j];
                        sumCol  += matriz[j, i];
                        if (i == j)
                            sumDiag += matriz[i, j];
                    }
                    resultadoFilas.Add(sumFila);
                    resultadoColumnas.Add(sumCol);
                    sumFila = 0;
                    sumCol = 0;
                }

                resultadoDiagonal.Add(sumDiag);

                if (resultadoFilas[0] == resultadoFilas[1] && resultadoFilas[0] == resultadoFilas[2])
                    if (resultadoFilas[0] == resultadoColumnas[0])
                        if (resultadoColumnas[0] == resultadoColumnas[1] && resultadoColumnas[0] == resultadoColumnas[2])                              
                            if (resultadoDiagonal[0] == resultadoColumnas[0])
                                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }


        public static int[] specialMerge(int[] arrayOne, int[] arrayTwo)
        {
            int[] arrayResult = new int[(arrayOne != null && arrayTwo != null) ? arrayOne.Count() + arrayTwo.Count() : 0 ];
            try
            {
                if (arrayOne != null && arrayTwo != null)
                {
                    var Result = arrayOne.Concat(arrayTwo).Select((item, index) => new { index, valor = item});
                    
                    foreach (var item in Result)
                        arrayResult[item.index] = item.valor;


                }
                
            }
            catch (Exception)
            {

                throw;
            }
            return arrayResult;
        }
    }
}
