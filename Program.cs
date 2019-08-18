

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_marcio_Calcula_Lista_string
{
    class Program
    {
        /*
         * ((raiz(2^4))+5)
            pos[0]=(2^4)
            pos[1]=(2R) // o indice da raiz vem antes
            pos[2]=(+5)
         */

        static void Main(string[] args)
        {
            List<string> teste = new List<string>();
            teste.Add("(10L5)");
            teste.Add("(*X)");
            teste.Add("(+1.4)");
            teste.Add("(/X)");
            teste.Add("(C)");

            Console.WriteLine(CalcularLista(SubstituirNaFuncao(teste, 2.5)).ToString());

            Console.ReadKey();


        }
        /// <summary>
        /// Substitui X na função por um numero especifico
        /// </summary>
        /// <param name="list"> lista com as operações do termo separadas e ainda com a variavel X</param>
        /// <param name="numero">variavel double que irá substituir o x na função</param>
        /// <returns>retorna uma lista no mesmo formato que a primeira porém com o x já substituido por um numero</returns>
        static List<string> SubstituirNaFuncao(List<string> list,double numero)
        {
            List<string> lista = new List<string>();

            string aux;
            lista = list;

           for(int n=0;n<=lista.Count-1; n++)
            {
                for(int n2=0;n2<lista[n].Length;n2++)
                {
                    if (lista[n][n2] == 'X')
                    {
                        aux = lista[n].Substring(0, n2) + numero.ToString()+ lista[n].Substring(n2+1, lista[n].Length-n2-1);
                        lista[n] = aux;
                    }
                }
            }
           
            return lista;
        }
        /// <summary>
        /// Calcula o resultado de todas as operações juntas da lista
        /// </summary>
        /// <param name="lista"> é a lista com todas as strings de acordo com o protocolo acordado</param>
        /// <returns> retorna o valor (double) das expressões daquela lista</returns>
        static double CalcularLista (List<string> lista)
        {
           

            char[] Operadores = new char[] { '^','R','+','-','*','/','C','c','S','s','T','t','L'};

            char operacao;

            string[] vetString = new string[2];

            List<double> resultados = new List<double>();

            resultados.Capacity = lista.Capacity;

            for(int posLista=0;posLista<=lista.Count-1;posLista++)
            {
                for (int pos=0;pos<lista[posLista].Length;pos++)
                {
                    foreach(char oper in Operadores) // procura dentre todos os operadores em todas as posições da string
                    {
                        if(lista[posLista][pos]==oper) //achou a operação
                        {
                            operacao = oper; // guarda a operação

                            vetString = lista[posLista].Split(oper); // separa os termos

                            //Cosseno só tem um termo que vem depois
                            //Seno só tem um termo que vem depois
                            //Tangente só tem um termo que vem depois
                            //secante só tem um termo que vem depois
                            //cossecante só tem um termo que vem depois
                            //cotangente só tem um termo que vem depois

                            if (vetString[0]=="(" && oper!='C' && oper != 'S' && oper != 'T'
                                && oper != 'c' && oper != 's' && oper != 't')
                            //Não tem termo "numero" portanto o programa irá inserir o 
                            //resultado já calculado armazenado na lista de double
                            //na posição anterior 
                            {
                                vetString[0] = "("+resultados[posLista - 1].ToString();
                            }
                            if (vetString[1] == ")")
                            {
                                vetString[1] = resultados[posLista - 1].ToString()+")";
                            }

                           resultados.Add(CalcularOperacao(vetString, operacao)); // calcula a expressão e atribui na lista de resultado
                        }
                    }
                }
            }

            return resultados[resultados.Count-1];
        }
        /// <summary>
        /// Calcula e retorna o valor da operação entre 2 numeros (uma parte da lista)
        /// </summary>
        /// <param name="termos">um vetor de string com os 2 termos</param>
        /// <param name="operador">o operador indicando a operação</param>
        /// <returns>retorna o valor numerico da lista</returns>
        static double CalcularOperacao(string[]termos,char operador)
        {
            double resultado = 0,termo1=1,termo2=1;

            termos[0] = termos[0].Substring(1, termos[0].Length - 1); // tira o parenteses (

            if (termos[0] == "E") termo1 = Math.E;
            else if (termos[0] == "P") termo1 = Math.PI;
            else if (termos[0]!="")termo1 = Convert.ToDouble(termos[0]); //converte o primeiro termo para double

            if (termos[1] == "E") termo2 = Math.E;
            else if (termos[1] == "P") termo2 = Math.PI;
            else if (termos[1] != "") termos[1] = termos[1].Substring(0, termos[1].Length - 1); // tira o parenteses )

            termo2 = Convert.ToDouble(termos[1]); //converte o segundo termo para double

            switch (operador)
            {
                case '^': // potencia
                    resultado = Math.Pow(termo1, termo2);
                    break;
                case 'R': //Raiz
                    resultado = Math.Pow(termo2, 1/termo1);
                    break;
                case '+': //soma
                    resultado = termo1 + termo2;
                    break;
                case '-': //subtração
                    resultado = termo1 - termo2;
                    break;
                case '*': //multiplicação
                    resultado = termo1 * termo2;
                    break;
                case '/': //divisão
                    resultado = termo1 / termo2;
                    break;
                case 'C': //cosseno
                    resultado = Math.Cos(ConvertToRadian(termo2));
                    break;
                case 'c': //Secante
                    resultado = 1/Math.Cos(ConvertToRadian(termo2));
                    break;                  
                case 'S': //Seno
                    resultado = Math.Sin(ConvertToRadian(termo2));
                    break;
                case 's': //Seno
                    resultado = 1/Math.Sin(ConvertToRadian(termo2));
                    break;
                case 'T': //Tangente
                    resultado = Math.Tan(ConvertToRadian(termo2));
                    break;
                case 't': //Tangente
                    resultado = 1/Math.Tan(ConvertToRadian(termo2));
                    break;
                case 'L': //Logaritmo
                    if (termo1 == Math.E) resultado = Math.Log(termo2);

                    else if (termo1 == 10.0) resultado = Math.Log10(termo2);

                    else // base diferente de 10, aplicar a mudança de base de logaritmo
                    {
                        resultado = Math.Log10(termo2)/ Math.Log10(termo1);
                    }
                    break;
                


            }
            return resultado;
        }
        /// <summary>
        /// Converte um angulo em graus para radiano
        /// </summary>
        /// <param name="angle"> angulo em graus</param>
        /// <returns> retorna o valor em radianos correspondente ao parametro angle</returns>
        static double ConvertToRadian(double angle)
        {
            double resultado = 0;

            resultado = angle * Math.PI / 180.0;

            return resultado;
        }
    }
}
