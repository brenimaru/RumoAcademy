using ResponsabilidadesClasse.Modelos;
using ResponsabilidadesClasse.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsabilidadesClasse.Servicos
{
    class ProdutoServico
    {
        private readonly ProdutoRepositorio _repositorio;
        public ProdutoServico()
        {
            _repositorio = new Repositorios.ProdutoRepositorio();
        }
        public void Cadastrar()
        {

            var produto = ColetarDadosProduto();
            _repositorio.Inserir(produto);
           
            Console.WriteLine($"{produto.Nome} cadastrado com sucesso!");
            Console.WriteLine($"Aperte uma tecla para prosseguir!");
            Console.ReadKey();
        }

        public void Listar()
        {
            var produtos = _repositorio.Listar();

            Console.WriteLine("Deseja também listar produtos inativos? S/N");
            if (Console.ReadLine() == "N")
                produtos = produtos.Where(x => x.Situacao == true).ToList();

            Console.Clear();

            foreach (var p in produtos)
            {
                Console.WriteLine($"Identificar => {p.IdentificadorProduto};Nome => {p.Nome};Valor => {p.Valor};{(p.Situacao ? "Ativo" : "")}");
            }
            //produtos.ForEach(p => Console.WriteLine($"Identificar => {p.IdentificadorProduto};Nome => {p.Nome};Valor => {p.Valor};{(p.Situacao ? "Ativo" : "")}"))
        }

        public void Atualizar()
        {
            Console.WriteLine("Por favor, forneca o identificador do produto para atualizar");
            int identificadorInformado = Convert.ToInt32(Console.ReadLine());
            if (!_repositorio.SeExiste(identificadorInformado))
            {
                Console.WriteLine("Este identificador nao existe... Tente novamente...");
                return;
            }

            var produto = ColetarDadosProduto();
            produto.IdentificadorProduto = identificadorInformado;
            _repositorio.Atualizar(produto);
        }
            
        public produto ColetarDadosProduto()
        {
            Console.WriteLine("Qual o nome do produto que voce quer cadastrar?");
            var nomeProduto = Console.ReadLine();

            Console.WriteLine($"Qual o valor do produto {nomeProduto}?");
            var valor = Convert.ToDecimal(Console.ReadLine());

            return new produto()
            {
                Nome = nomeProduto,
                Valor = valor,
                Situacao = true
            };
        }
    }
}
