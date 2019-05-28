using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPesquisa
{
    class AgendaPesquisa
    {
        public class contatos
        {
            public int Código { get; set; }
            public string Nome { get; set; }
            public DateTime DataNascimento { get; set; }
            public string Telefone { get; set; }
            public string email { get; set; }
        }

        public class ArvoreBinaria
        {
            class No
            {
                public contatos Dado;
                public No Esq;
                public No Dir;

                public No(contatos _dado)
                {
                    Dado = _dado;
                    Esq = null;
                    Dir = null;
                }
            }

            private No Raiz;

            public ArvoreBinaria()
            {
                Raiz = null;
            }

            //Verifica se Arvore Vazia
            public bool Vazia()
            {
                return Raiz == null;
            }

            //Adiciona NO na arvore
            public void Inserir(contatos _dado)
            {
                Raiz = Inserir(Raiz, _dado);
            }
            private No Inserir(No no, contatos _dado)
            {

                if (no == null)
                {
                    no = new No(_dado);
                }
                else if (_dado.Código < no.Dado.Código)
                {
                    no.Esq = Inserir(no.Esq, _dado);
                }

                else if (_dado.Código > no.Dado.Código)
                {
                    no.Dir = Inserir(no.Dir, _dado);
                }

                else
                    Console.WriteLine("Erro: Registro ja existente");
                return no;
            }

            //Remover NO da arvore
            private No Remover(No no, int _chave)
            {
                if (no == null)
                    Console.WriteLine("Erro: Registro nao encontrado");

                else if (_chave < no.Dado.Código)
                    no.Esq = Remover(no.Esq, _chave);

                else if (_chave > no.Dado.Código)
                    no.Dir = Remover(no.Dir, _chave);

                else
                {
                    if (no.Dir == null)
                        no = no.Esq;

                    else if (no.Esq == null)
                        no = no.Dir;

                    else
                        no.Esq = Antecessor(no, no.Esq);
                }
                return no;
            }
            private No Antecessor(No no, No ant)
            {
                if (ant.Dir != null) ant.Dir = Antecessor(no, ant.Dir);
                else
                {
                    no.Dado = ant.Dado;
                    ant = ant.Esq;
                }
                return ant;
            }

            //Pesquisar na arvore binaria
            public contatos Pesquisar(int chave)
            {
                return Pesquisar(Raiz, chave);
            }
            private contatos Pesquisar(No no, int _chave)
            {
                if (no == null)
                {
                    return null;
                }
                if (_chave < no.Dado.Código)
                {
                    return Pesquisar(no.Esq, _chave);
                }

                else if (_chave > no.Dado.Código)
                {
                    return Pesquisar(no.Dir, _chave);
                }

                else if (_chave == no.Dado.Código)
                {
                    return no.Dado;
                }
                else
                    return null;

            }
        }

        static void Main(string[] args)
        {
            
    
            string x;
            int codigo = 0;
            ArvoreBinaria ArvoreContatos;
            DateTime dt;
            contatos[] contatos = Ler("contatos.txt");
            int Aux;
            string nome;
            int pes;

            if (contatos != null)
                codigo = contatos[contatos.Length - 1].Código;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Inserir um novo Contato");
                Console.WriteLine("2 - Remover um contato existente");
                Console.WriteLine("3 - Imprimir os contatos ordenados por nome");
                Console.WriteLine("4 - Imprimir os contatos ordenador por data de nascimento");
                Console.WriteLine("5 - Pesquisar um contato na lista usando o código (Arvore Binária)");
                Console.WriteLine("6 - Pesquisar um contato na lista usando o nome (Arvore Binária)");
                Console.WriteLine("7 - Pesquisar um contato na lista usando o data de nascimento (Arvore Binária)");
                Console.WriteLine("8 - Pesquisar um contato na lista usando o e-mail (Arvore Binária)");
                Console.WriteLine("9 - Sair");

                x = Console.ReadLine();

                switch (x)
                {
                    case "1": //1 - Inserir um novo Contato
                        codigo++;

                        Console.Clear();
                        contatos NovoContato = new contatos();

                        NovoContato.Código = codigo;

                        Console.WriteLine("Digite o nome do contato");
                        NovoContato.Nome = Console.ReadLine();

                        Console.WriteLine("Digite a data de nascimento dd/mm/aaaa");
                        string data = Console.ReadLine();

                        Console.WriteLine("Digite o telefone");
                        NovoContato.Telefone = Console.ReadLine();

                        Console.WriteLine("Digite o e-mail");
                        NovoContato.email = Console.ReadLine();

                        //Manda Escrever
                        Escrever("contatos.txt", $"{NovoContato.Código}|{NovoContato.Nome}|{data}|{NovoContato.Telefone}|{ NovoContato.email}");

                        break;

                    case "2": //2 - Remover um contato existente
                        Console.Clear();
                        Console.WriteLine("Digite o codigo a ser deletado");
                        DeletarLinha("contatos.txt", Console.ReadLine());

                        break;

                    case "3": //3 - Imprimir os contatos ordenados por nome
                        Console.Clear();
                        contatos = Ler("contatos.txt");
                        int QtnChar = Menor(contatos);
                        QuickSortAlfabetica(contatos,0, contatos.Length-1, QtnChar);

                        foreach (contatos item in contatos)
                            imprimir(item);

                        Console.ReadKey();

                        break;

                    case "4": //4 - Imprimir os contatos ordenador por data de nascimento
                        Console.Clear();
                        contatos = Ler("contatos.txt");
                        dt = DateTime.Now;

                        QuickSortDataNarcimento(contatos, 0, contatos.Length - 1, dt);

                        foreach (contatos item in contatos)
                            imprimir(item);

                        Console.ReadKey();

                        break;

                    case "5"://5 - Pesquisar um contato na lista usando o código (Árvore Binária e Tabela Hash)

                        ArvoreContatos = new ArvoreBinaria();
                        Console.Clear();
                        contatos = Ler("contatos.txt");
                        dt = DateTime.Now;

                        QuickSortDataNarcimento(contatos, 0, contatos.Length - 1, dt);

                        foreach (contatos item in contatos)
                            ArvoreContatos.Inserir(item);

                        Console.WriteLine("Digite o codigo a ser deletado");

                        imprimir(ArvoreContatos.Pesquisar(int.Parse(Console.ReadLine())));
                        Console.ReadKey();

                        break;

                    case "6"://6 - Pesquisar um contato na lista usando o nome (Árvore Binária e Tabela Hash)
                        ArvoreContatos = new ArvoreBinaria();
                        Console.Clear();
                        contatos = Ler("contatos.txt");
                        Aux = Menor(contatos);

                        for (int i = 0; i < contatos.Length; i++)
                        {
                            contatos[i].Código = ConverterInt(contatos[i].Nome, Aux);
                            ArvoreContatos.Inserir(contatos[i]);
                        }

                        Console.WriteLine("Digite o nome");
                        nome = Console.ReadLine();
                        pes = ConverterInt(nome, Aux);
                        imprimir(ArvoreContatos.Pesquisar(pes));
                        Console.ReadKey();
                        break;

                    case "7"://7 - Pesquisar um contato na lista usando o data de nascimento (Árvore Binária e Tabela Hash)
                        ArvoreContatos = new ArvoreBinaria();
                        Console.Clear();
                        contatos = Ler("contatos.txt");
                        dt = DateTime.Now;

                        for (int i = 0; i < contatos.Length; i++)
                        {
                            contatos[i].Código = (dt - contatos[i].DataNascimento).Days;
                            ArvoreContatos.Inserir(contatos[i]);
                        }

                        Console.WriteLine("Digite a data dd/mm/aaaa");
                        string[] dat1 = Console.ReadLine().Split('/');
                        DateTime pesq = new DateTime(int.Parse(dat1[2]), int.Parse(dat1[1]),int.Parse(dat1[0]));
                        int dataPes = (dt - pesq).Days;
                        imprimir(ArvoreContatos.Pesquisar(dataPes));
                        Console.ReadKey();
                        break;

                    case "8"://8 - Pesquisar um contato na lista usando o e-mail (Árvore Binária e Tabela Hash)
                        ArvoreContatos = new ArvoreBinaria();
                        Console.Clear();
                        contatos = Ler("contatos.txt");
                        Aux = Menor(contatos);

                        for (int i = 0; i < contatos.Length; i++)
                        {
                            contatos[i].Código = ConverterInt(contatos[i].email, Aux);
                            ArvoreContatos.Inserir(contatos[i]);
                        }

                        Console.WriteLine("Digite o nome");
                        nome = Console.ReadLine();
                        pes = ConverterInt(nome, Aux);
                        imprimir(ArvoreContatos.Pesquisar(pes));
                        Console.ReadKey();
                        break;

                    case "9"://sair
                        return;

                    default:
                        Console.WriteLine("Invalido");
                        break;
                }

            } while (true);
        }

        //Converte nome para interio - Soma dos valores char
        private static int ConverterInt(string nome, int menor)
        {
            string dado = nome.Trim().ToLower();
            int soma = 0;
            int x;
           
            for (int i = 0; i < menor; i++)
            {
                if (i > nome.Length - 1)
                    x = 0;
                else if(dado[i] == 'ç')
                    x = 'c' - 96;
                else if (dado[i] == 'ã')
                    x = 'a' - 96;
                else if (dado[i] == 'õ')
                    x = 'o' - 96;
                
                else
                    x = dado[i] - 96;

                soma += x * (int)Math.Pow(26,(menor-i-1)) ;
            }
            return soma;
        }

        //Gerar arquivos de log
        public static void Escrever(string nomeArq, string texto)
        {
            // Cria um novo arquivo
            FileStream arq1 = new FileStream(nomeArq, FileMode.OpenOrCreate);

            //faz uma leitura do arquivo para ir ao fina do arquivo e assim nhão sobreescrever nenhum dado
            StreamReader ler = new StreamReader(arq1);
            ler.ReadToEnd();

            // declara instancia do StreamWriter 
            StreamWriter escreve = new StreamWriter(arq1);

            //obtem o texto no parametro da chamada da função
            escreve.WriteLine(texto);
            escreve.Close();
        }

        //Retorna Lista de contatos lidos dos arquivos
        public static contatos[] Ler(string nomeArq)
        {
            //Arquivo não existe
            if (!File.Exists(nomeArq))
                return null;

            contatos[] Contatos;

            string[] lerArquivo = File.ReadAllLines(nomeArq);
            int Qtn = lerArquivo.Length;

            Contatos = new contatos[Qtn];

            if (lerArquivo.Length < 1)
            {
                return null;
            }

            for (int i = 0; i < Qtn; i++)
            {
                Contatos[i] = new contatos();

                string[] linha = lerArquivo[i].Split('|');
                string[] DataNacimento = linha[2].Split('/');

                Contatos[i].Código = int.Parse(linha[0]);
                Contatos[i].Nome = linha[1];
                Contatos[i].DataNascimento = new DateTime(int.Parse(DataNacimento[2]), int.Parse(DataNacimento[1]), int.Parse(DataNacimento[0]));
                Contatos[i].Telefone = linha[3];
                Contatos[i].email = linha[4];
            }

            return Contatos;

        }

        //Deleta a linha em que o cidigo é igual a o parametro "codigo" passado na chamada da função
        public static void DeletarLinha(string path, string codigo)
        {
            //obtem a leitura do arquivo
            //cada posição do vetor corresponde a uma linha do arquivo
            string[] lerArquivo = File.ReadAllLines(path);


            //Declara vertor de string dados, cada posição do vetor sera um dado
            string[] dados = null;

            //declara string para novo texto
            string novoTexo = null;

            //faz a busca do codigo
            for (int i = 0; i < lerArquivo.Length - 1; i++)
            {
                // Ex: se for um cliente:
                //dados[0] = Codigo
                //dados[1] = Nome
                //dados[2] = Endereço
                //dados[3] = Telefone
                dados = lerArquivo[i].Split('|');

                //Em todas os itens dados[0] será referente ao codigo
                //Se o codigo for igual ele não acressenta o a posição do vetor 'lerArquivo[i]' a string 'novoTexo'
                if (dados[0] != codigo)
                {
                    // monta o novo texto que terá o arquivo, agora com a lunha deletada
                    novoTexo += lerArquivo[i] + "\r\n"; //Acressenta '/r/n' para tabular e pular a linha
                }

            }

            // atualizar o arquivo
            StreamWriter escreve = new StreamWriter(path, false);
            escreve.Write(novoTexo);
            escreve.Close();
        }

        //imprimir
        public static void imprimir(contatos contato)
        {
            if (contato != null)
            {
            Console.WriteLine($"Codigo: {contato.Código}");
            Console.WriteLine($"Nome: {contato.Nome}");
            Console.WriteLine($"Data de Nascimento: {contato.DataNascimento}");
            Console.WriteLine($"Telefone: {contato.Telefone}");
            Console.WriteLine($"E-mail: {contato.email}");
            Console.WriteLine();
            }
            else
                Console.WriteLine("-");
        }

        //Ordenação Rapida
        static void QuickSortAlfabetica(contatos[] A, int esquerda, int direita, int menor)
        {

            int pivo;
            contatos temp;
            int i, j;
            i = esquerda;
            j = direita;


            pivo = ConverterInt(A[(esquerda + direita) / 2].Nome, menor);

            while (i <= j)
            {

                while (ConverterInt(A[i].Nome, menor) < pivo && i < direita)
                    i++;
                while (ConverterInt(A[j].Nome, menor) > pivo && j > esquerda)
                    j--;

                if (i <= j)
                {
                    temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                    i++;
                    j--;
                }
            }

            if (j > esquerda)
                QuickSortAlfabetica(A, esquerda, j, menor);

            if (i < direita)
                QuickSortAlfabetica(A, i, direita, menor);
        }

        //Ordenação Rapida
        static void QuickSortDataNarcimento(contatos[] A, int esquerda, int direita, DateTime Data)
        {

            int pivo;
            contatos temp;
            int i, j;
            i = esquerda;
            j = direita;

            pivo = (Data - A[(esquerda + direita) / 2].DataNascimento).Days ;

            while (i <= j)
            {
                while ((Data - A[i].DataNascimento).Days < pivo && i < direita)
                    i++;
                while ((Data - A[j].DataNascimento).Days > pivo && j > esquerda)
                    j--;

                if (i <= j)
                {
                    temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                    i++;
                    j--;
                }
            }

            if (j > esquerda)
                QuickSortDataNarcimento(A, esquerda, j, Data);

            if (i < direita)
                QuickSortDataNarcimento(A, i, direita, Data);
        }

        //Achar o menor numeor de caracteres na string
        public static int Menor(contatos[] contatos)
        {
            int menor = contatos[0].Nome.Length;
            for (int i = 1; i < contatos.Length; i++)
            {
                if (contatos[i].Nome.Length < menor)
                {
                    menor = contatos[i].Nome.Length;
                }
            }
            if (menor < 4)
                menor = 4;
            if (menor > 7)
                menor = 7;

            return menor;
        }



    }
}
