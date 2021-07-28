using System;
using System.Collections.Generic;
using System.IO;

namespace CIMCMVC.Models
{
    public class Usuario : ClasseBase
    {
        public int IDUsuario { get; set; }
        
        public string Nome { get; set; }
        
        public string Email { get; set; }
        
        public string Senha { get; set; }
        
        private const string CAMINHO = "Database/Usuario.csv";

        public Usuario()
        {
            CriarPastaEArquivo(CAMINHO);
        }

        private string PrepararLinha(Usuario u)
        {
        return $"{u.IDUsuario};{u.Nome};{u.Email};{u.Senha}";
        }

        public void Cadastrar(Usuario u)
        {
            string[] linha = { PrepararLinha(u) };
            File.AppendAllLines(CAMINHO, linha);
        }

        public List<Usuario> Listar()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string[] linhas = File.ReadAllLines(CAMINHO);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Usuario usuario = new Usuario();
                usuario.IDUsuario = Int32.Parse(linha[0]);
                usuario.Nome = linha[1];
                usuario.Email = linha[2];
                usuario.Senha = linha[3];

                usuarios.Add(usuario);
            }
            return usuarios;
        }

        public void Editar(Usuario u)
        {
            List<string> linhas = LerTodasLinhasCSV(CAMINHO);
            linhas.RemoveAll(x => x.Split(";")[0] == u.IDUsuario.ToString());
            linhas.Add(PrepararLinha(u));
            ReescreverCSV(CAMINHO, linhas);
        }

        public void Deletar(int id)
        {
            List<string> linhas = LerTodasLinhasCSV(CAMINHO);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            ReescreverCSV(CAMINHO, linhas);
        }

        public void RetornarSenha(string _senha){
            Senha = _senha;
        }
    }
}