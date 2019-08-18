

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
        
        
        public static List<String> fatores = new List<string>();
        public static int [] inParentPosi; //array para capturar as posicoes onde ocorre a abertura dos parenteses
        public static int[] fimParentPosi; //array para capturar as posicoes onde ocorre o fechamento dos parenteses
        public static List<String> termos = new List<string>(); //armazena os termos

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
        
        
        /// <summary>
        /// Recebe um termo gráfica  as operações na lista funções
        /// </summary>
        /// <param name="mensagem"></param>
        /// 
        static void organizaConta(String mensagem)
        {
            String aux;
           
            bool firstscan = false;
            int c1,c2;
            c1 = 0;
            c2 = 0;
            
           

           //função para encontrar todos os parenteses e salvar suas posições

            for (int i = 0; i<(mensagem.Length);i++)
            {
                
                if (mensagem[i] == '(')
                {
                    inParentPosi[c1] = i;
                    c1++;
                }
                else if (mensagem[i] == ')')
                {
                    fimParentPosi[c2] = i;
                    c2++;
                }

            }
            c1 = 0;

            //TESTE EXIBIR AS STRINGS CORTADAS
            for(int i = (inParentPosi.Length - 1); i>=0;i--)
            {

                if(!firstscan)
                {

                    fatores.Add(mensagem.Substring(inParentPosi[i], ((fimParentPosi[c1]) - inParentPosi[i]) + 1));

                    //Console.WriteLine("{0}", mensagem.Substring(inParentPosi[i], ((fimParentPosi[c1]) - inParentPosi[i]) + 1));
                    firstscan = true;
                }
                else
                {
                    //Console.WriteLine("{0}", mensagem.Substring(fimParentPosi[c1-1], ((fimParentPosi[c1]) - fimParentPosi[c1 - 1]) + 1));
                    aux = mensagem.Substring(fimParentPosi[c1 - 1], ((fimParentPosi[c1]) - fimParentPosi[c1 - 1]) + 1);

                   
                    if (aux[0] == ')')
                    {
                       aux = "(" + aux.Remove(0,1);
                        
                    }
                  
                    fatores.Add(aux);
     
                }

                c1++;
            }
            
            for(int i = 0; i <fatores.Count;i++)
            {
               
                Console.WriteLine("{0}", fatores[i]);
            }

        }

        /// <summary>
        /// Método reposável por verificar se a String obtida pela interface gráfica está correta 
        /// Retorna verdadeiro se a mensagem estiver de acordo
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        static bool VerificaString(String mensagem)
        {
            //verificação do número de parenteses
            int aux1, aux2,cont;
            aux1 = 0;
            aux2 = 0;
         
            foreach (char c in mensagem) //faz a varredura em cada elemento da String
            {
                if (c == '(')
                {
                    aux1++;
                }
                else if (c == ')')
                {
                    aux2++;
                }
              
            }

            //verifica se todos os parenteses estão sendo fechados
            if (aux1 == aux2)
            {
                inParentPosi = new int[aux1]; //define o tamanho do vetor 
                fimParentPosi = new int[aux2]; //define o tamanho do vetor
                return true;
            }
            else
            {
           
                return false;
            }



        }


        static void SeparaTermos(String mensagem)
        {
            //encontra os termos e faz a substring
            int inicio, fim,count;
            count = 0;
            inicio = 0;
            fim = 0;
            while(mensagem.Contains("[") && (mensagem.Contains("]")))
            {
                foreach (char c in mensagem)
                {

                    if (c == '[')
                    {
                        inicio = count;
                    }
                    else if (c == ']')
                    {
                        fim = count;
                        count = 0;
                        break;
                    }

                    count++;
                }

                termos.Add(mensagem.Substring(inicio, (fim - inicio)+1)); //adciona o termo na lista
                mensagem = mensagem.Substring(fim+1); //retira da string o termo adicionado na lista

            }

         /*   for(int i=0; i<termos.Count;i++)
            {
                Console.WriteLine("{0}", termos[i]);
            
            }*/
        }
    }
}
