using System;
using System.Collections.Generic;
using System.IO;

namespace CIMCMVC.Models
{
    public class IMC : ClasseBase
    {
        public int IDIMC { get; set; }
        
        public int IDUsuario { get; set; }
        
        public float Peso { get; set; }

        public float Altura { get; set; }
        
        public float ValorIMC { get; set; }

        public string ClassificacaoIMC { get; set; }
        
        private const string CAMINHO = "Database/IMC.csv";

        public IMC()
        {
            CriarPastaEArquivo(CAMINHO);
        }

        private string PrepararLinha(IMC i)
        {
        return $"{i.IDIMC};{i.IDUsuario};{i.Peso};{i.Altura};{i.ValorIMC};{i.ClassificacaoIMC}";
        }

        public void Cadastrar(IMC i)
        {
            string[] linha = { PrepararLinha(i) };
            File.AppendAllLines(CAMINHO, linha);
        }

        public List<IMC> Listar()
        {
            List<IMC> imcs = new List<IMC>();
            string[] linhas = File.ReadAllLines(CAMINHO);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                IMC imc = new IMC();
                imc.IDIMC = Int32.Parse(linha[0]);
                imc.IDUsuario = Int32.Parse(linha[1]);
                imc.Peso = float.Parse(linha[2]);
                imc.Altura = float.Parse(linha[3]);
                imc.ValorIMC = float.Parse(linha[4]);
                imc.ClassificacaoIMC = linha[5];
                imcs.Add(imc);
            }
            return imcs;
        }

        public void Editar(IMC i)
        {
            List<string> linhas = LerTodasLinhasCSV(CAMINHO);
            linhas.RemoveAll(x => x.Split(";")[0] == i.IDIMC.ToString());
            linhas.Add(PrepararLinha(i));
            ReescreverCSV(CAMINHO, linhas);
        }

        public void Deletar(int id)
        {
            List<string> linhas = LerTodasLinhasCSV(CAMINHO);
            linhas.RemoveAll(x => x.Split(";")[1] == id.ToString());
            ReescreverCSV(CAMINHO, linhas);
        }
    }
}