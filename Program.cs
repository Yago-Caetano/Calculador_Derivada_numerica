using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Derivada_Numerica
{
    class Program
    {
        public static List<String> fatores;
        public static int [] inParentPosi; //array para capturar as posicoes onde ocorre a abertura dos parenteses
        public static int[] fimParentPosi; //array para capturar as posicoes onde ocorre o fechamento dos parenteses

        static void Main(string[] args)
        {
            String funcao;

            Console.WriteLine("Insira a String");
            funcao = Console.ReadLine();

            if(VerificaString(funcao))
            {
                Console.WriteLine("String Correta");
                organizaConta(funcao);
            }
            else
            {
                Console.WriteLine("String Incorreta");
            }

            Console.ReadKey();
        }

        static void organizaConta(String mensagem)
        {
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
                Console.WriteLine("{0}", mensagem.Substring(inParentPosi[i], (fimParentPosi[c1]) - inParentPosi[i]));
                c1++;
            }
   

        }

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
    }
   
}
