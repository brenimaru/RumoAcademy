using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrizSomaNumerosPositivos
{
     internal class SomaBingo
    {
        public int QuantidadeLinhas{ get; set; }
        public int QuantidadeColunas { get; set; }
        public void Executar()
        {
            ExibirCabecalho();
            ReceberQuantidadeLinhasColunasCartela();
            var matrizNumeros = PerguntarUsuarioNumerosCartela();
            var resultadoSoma = SomarPositivosNumeros(matrizNumeros);
            ExibirResultado(resultadoSoma);

        }
        public void ExibirCabecalho()
        {

            Console.WriteLine("###############################################");
            Console.WriteLine("### Programa para soma de cartela de um bingo ###");
            Console.WriteLine("###############################################");
        }
        public void ReceberQuantidadeLinhasColunasCartela()
        {
            Console.WriteLine("Quantas linhas a sua cartela tem?");
            string? linhasInformadas = Console.ReadLine();
            QuantidadeLinhas = Convert.ToInt32(linhasInformadas);

            Console.WriteLine("Quantas colunas a sua cartela tem?");
            string? colunasInformadas = Console.ReadLine();
            QuantidadeColunas = Convert.ToInt32(colunasInformadas);
        }
        public int [,] PerguntarUsuarioNumerosCartela()
        {
            var matrizNumeros = new int[QuantidadeLinhas, QuantidadeColunas];

            for (int contadorLinhas = 0; contadorLinhas < QuantidadeLinhas; contadorLinhas++)
            {
                for (int contadorColunas = 0; contadorColunas < QuantidadeColunas; contadorColunas++)
                {
                    Console.WriteLine($"Informe o numero da {contadorLinhas + 1} linha, da coluna {contadorColunas + 1}:");
                    int numeroInformado = Convert.ToInt32(Console.ReadLine());
                    matrizNumeros[contadorLinhas, contadorColunas] = numeroInformado;
                }
            }
            return matrizNumeros;
        }
        private int SomarPositivosNumeros(int[,] matrizNumeros)
        {
            int somaNumeros = 0;

            for (int contadorLinhas = 0; contadorLinhas < QuantidadeLinhas; contadorLinhas++)
            {
                for (int contadorColunas = 0; contadorColunas < QuantidadeColunas; contadorColunas++)
                {
                    int numeroInformado = matrizNumeros[contadorLinhas, contadorColunas];
                    //se o numero for negativo, pula para a proxima
                    //registro do for que está o continue
                    if (numeroInformado< 0)
                        continue;

                    somaNumeros = somaNumeros + numeroInformado;
                }
}
        }
        public void ExibirResultado (int somaNumeros)
        {
            Console.WriteLine("A soma dos numeros positivos é igual a " + somaNumeros);
            Console.ReadKey();
        }
    }
}
