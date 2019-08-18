using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivada_Numerica
{
    class Program
    {
        
        public static List<String> fatores = new List<string>();
        public static int [] inParentPosi; //array para capturar as posicoes onde ocorre a abertura dos parenteses
        public static int[] fimParentPosi; //array para capturar as posicoes onde ocorre o fechamento dos parenteses
        public static List<String> termos = new List<string>(); //armazena os termos


        static void Main(string[] args)
        {
            String funcao;

            Console.WriteLine("Insira a String");
            funcao = Console.ReadLine();

               if(VerificaString(funcao))
               {
                   Console.WriteLine("String Correta");
                // organizaConta(funcao);
                SeparaTermos(funcao);
                organizaConta(termos[0]);
            }
               else
               {
                   Console.WriteLine("String Incorreta");
               }
              
            

            Console.ReadKey();
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
