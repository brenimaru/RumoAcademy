using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PetShopClientManager
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime Nascimento { get; set; }

        public Cliente(string nome, string cpf, DateTime nascimento)
        {
            Nome = nome;
            CPF = cpf;
            Nascimento = nascimento;
        }

        public override string ToString()
        {
            return $"Nome: {Nome.ToUpper()}, CPF: {FormatarCPF(CPF)}, Nascimento: {Nascimento.ToString("dd/MM/yyyy")}";
        }

        // Formatar CPF para a máscara
        public static string FormatarCPF(string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }

        // Validar CPF
        public static bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", ""); // Remover máscara
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;

            // Validação de CPF utilizando o algoritmo do CPF
            int[] numeros = cpf.Select(c => int.Parse(c.ToString())).ToArray();

            int soma1 = 0, soma2 = 0;
            int[] peso1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] peso2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            for (int i = 0; i < 9; i++)
                soma1 += numeros[i] * peso1[i];

            int resto1 = soma1 % 11;
            int digito1 = resto1 < 2 ? 0 : 11 - resto1;

            soma2 += digito1 * peso2[0];
            for (int i = 1; i < 10; i++)
                soma2 += numeros[i] * peso2[i];

            int resto2 = soma2 % 11;
            int digito2 = resto2 < 2 ? 0 : 11 - resto2;

            return numeros[9] == digito1 && numeros[10] == digito2;
        }

        // Validar nome
        public static bool ValidarNome(string nome)
        {
            return !string.IsNullOrWhiteSpace(nome) && nome.Length >= 3 && nome.Length <= 80;
        }

        // Validar data de nascimento
        public static bool ValidarNascimento(DateTime nascimento)
        {
            int idade = DateTime.Now.Year - nascimento.Year;
            if (DateTime.Now.DayOfYear < nascimento.DayOfYear)
                idade--;

            return idade >= 16 && idade <= 120;
        }
    }

    class Program
    {
        static List<Cliente> clientes = new List<Cliente>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gerenciamento de Clientes - PetShop");
                Console.WriteLine("1. Cadastrar Cliente");
                Console.WriteLine("2. Listar Clientes");
                Console.WriteLine("3. Buscar Cliente por CPF");
                Console.WriteLine("4. Listar Aniversariantes do Mês");
                Console.WriteLine("5. Sair");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarCliente();
                        break;
                    case "2":
                        ListarClientes();
                        break;
                    case "3":
                        BuscarClientePorCPF();
                        break;
                    case "4":
                        ListarAniversariantesDoMes();
                        break;
                    case "5":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida! Pressione uma tecla para tentar novamente.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void CadastrarCliente()
        {
            Console.Clear();
            Console.WriteLine("Cadastrar Novo Cliente");

            string nome;
            string cpf;
            DateTime nascimento;

            // Validar nome
            while (true)
            {
                Console.Write("Digite o nome: ");
                nome = Console.ReadLine();
                if (Cliente.ValidarNome(nome))
                    break;
                else
                    Console.WriteLine("Nome inválido. Deve ter entre 3 e 80 caracteres e não pode ser vazio.");
            }

            // Validar CPF
            while (true)
            {
                Console.Write("Digite o CPF (somente números): ");
                cpf = Console.ReadLine();
                if (Cliente.ValidarCPF(cpf))
                    break;
                else
                    Console.WriteLine("CPF inválido. Por favor, insira um CPF válido.");
            }

            // Validar data de nascimento
            while (true)
            {
                Console.Write("Digite a data de nascimento (dd/MM/yyyy): ");
                string nascimentoStr = Console.ReadLine();
                if (DateTime.TryParseExact(nascimentoStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out nascimento) && Cliente.ValidarNascimento(nascimento))
                    break;
                else
                    Console.WriteLine("Data de nascimento inválida. A pessoa deve ter entre 16 e 120 anos.");
            }

            Cliente cliente = new Cliente(nome, cpf, nascimento);
            clientes.Add(cliente);
            Console.WriteLine("Cliente cadastrado com sucesso! Pressione uma tecla para voltar.");
            Console.ReadKey();
        }

        static void ListarClientes()
        {
            Console.Clear();
            Console.WriteLine("Lista de Clientes");

            if (clientes.Count == 0)
            {
                Console.WriteLine("Não há clientes cadastrados.");
            }
            else
            {
                foreach (var cliente in clientes)
                {
                    Console.WriteLine(cliente);
                }
            }

            Console.WriteLine("Pressione uma tecla para voltar.");
            Console.ReadKey();
        }

        static void BuscarClientePorCPF()
        {
            Console.Clear();
            Console.WriteLine("Buscar Cliente por CPF");
            Console.Write("Digite o CPF (somente números): ");
            string cpfBusca = Console.ReadLine();

            var cliente = clientes.FirstOrDefault(c => c.CPF.Replace(".", "").Replace("-", "") == cpfBusca.Replace(".", "").Replace("-", ""));

            if (cliente != null)
            {
                Console.WriteLine(cliente);
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }

            Console.WriteLine("Pressione uma tecla para voltar.");
            Console.ReadKey();
        }

        static void ListarAniversariantesDoMes()
        {
            Console.Clear();
            Console.WriteLine("Aniversariantes do Mês");

            var aniversarioMes = clientes.Where(c => c.Nascimento.Month == DateTime.Now.Month).ToList();

            if (aniversarioMes.Any())
            {
                foreach (var cliente in aniversarioMes)
                {
                    Console.WriteLine(cliente);
                }
            }
            else
            {
                Console.WriteLine("Nenhum aniversário neste mês.");
            }

            Console.WriteLine("Pressione uma tecla para voltar.");
            Console.ReadKey();
        }
    }
}
