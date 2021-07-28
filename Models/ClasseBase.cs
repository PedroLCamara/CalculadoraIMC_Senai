using System.Collections.Generic;
using System.IO;

namespace CIMCMVC.Models
{
    public class ClasseBase
    {
                public void CriarPastaEArquivo(string _caminho)
        {
            string Pasta = _caminho.Split("/")[0];
            string Arquivo = _caminho.Split("/")[1];
            if (!Directory.Exists(Pasta))
            {
                Directory.CreateDirectory(Pasta);
            }
            if (!File.Exists(_caminho))
            {
                File.Create(_caminho).Close();
            }
        }
        public List<string> LerTodasLinhasCSV(string _caminho)
        {
            List<string> Linhas = new List<string>();
            string Linha;
            using (StreamReader file = new StreamReader(_caminho))
            {
                while ((Linha = file.ReadLine()) != null)
                {
                    Linhas.Add(Linha);
                }
            }
            return Linhas;
        }
        public void ReescreverCSV(string _caminho, List<string> Linhas)
        {
            using (StreamWriter output = new StreamWriter(_caminho))
            {
                foreach (var item in Linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
        public int GerarID(string _caminho)
        {
            bool Condicao = false;
            int Id = 0;
            List<string> Linhas = LerTodasLinhasCSV(_caminho);
            List<string> Ids = new List<string>();
            if (Linhas != null)
            {
                foreach (var item in Linhas)
                {
                    Ids.Add(item.Split(";")[0]);
                    Id++;
                }
                do
                {
                    foreach (var item in Ids)
                    {
                        if (item == Id.ToString())
                        {
                            Id++;
                            Condicao = true;
                        }
                        else
                        {
                            Condicao = false;
                        }
                    }
                } while (Condicao == true);
            }
            else
            {
                Id = 1;
            }
            return Id;
        }
    }
}