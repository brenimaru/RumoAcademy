using ExemploTemplateConsole.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExemploTemplateConsole
{
    public class Fluxo
    {
        public void Executar()
        {
            //declarei uma variavel do tipo Pessoa
            //coloquei o nome da variavel como pessoa
            //inicializei usando o igual
            //recebendo o valor instanciado atáves do new
            //que o new instanciou a classe Pessoa
            //fiz com que a variavel pessoa tenha todos os atributos da classe
            Pessoa pessoa = new Pessoa();

            //quando eu coloco o nome da variavel que eu intanciei um objeto(classe)
            //atraves do . eu consigo acessar e modificar os atributos que tem na classe
            //ou seja o nome, está dentro de class Pessoa()
            //public string Nome { get; set; }
            //porque ele está public eu consigo acessar ele aqui
            pessoa.Nome = "brenin";

            //aqui eu converti uma data para datetime
            //vou informar uma data fake
            //para fingir ser meu aniversario
            pessoa.Nascimento = DateTime.Parse("2000-01-01");

            int quantosAnos = ObterAnosEntreDuasDatas(pessoa.Nascimento, DateTime.Now);

            if (quantosAnos < 18)
                {
                Console.WriteLine("Menor de idade");

            }else
            {
                Console.WriteLine("Maior de idade");
            }
        }

        private  int ObterAnosEntreDuasDatas(DateTime startDate, DateTime endDate)
        {
            //Excel documentation says "COMPLETE calendar years in between dates"
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day// AND the end day is less than the start day
                || endDate.Month < startDate.Month)// OR if the end month is less than the start month
            {
                years--;
            }

            return years;
        }

    }
    }




