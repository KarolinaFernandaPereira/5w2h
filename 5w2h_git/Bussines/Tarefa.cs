using System.ComponentModel.DataAnnotations;

namespace Bussines
{
    public class Tarefa
    {
        public int Codigo { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Nome { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Descricao { set; get; }

        // 5w2H
        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Oque { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Onde { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public DateTime Quando { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Como { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Quanto { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string Quem { set; get; }

        [Required(ErrorMessage = "Campo nome obrigatório")]
        public string PorQue { set; get; }

        public virtual ICollection<Funcionario> Funcionario { set; get; } = new List<Funcionario>();

        public int CodigoQuadro { set; get; }

        public virtual  Quadro Quadro { set; get; }

        public int Ativo { set; get; }
    }
}
