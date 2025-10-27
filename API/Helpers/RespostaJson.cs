namespace API.Domain.Helpers
{
    public class RespostaJson
    {
        public string Mensagem { get; set; }
        public bool Error { get; set; }
        public bool AtualizarJqGrid { get; set; }
        public string RodarFuncao { get; set; }
        public RespostaJson(string mensagem = "OK", bool error = false, bool atualizarJqGrid = false, string rodarFuncao = "")
        {
            Mensagem = mensagem;
            Error = error;
            AtualizarJqGrid = atualizarJqGrid;
            RodarFuncao = rodarFuncao;
        }
    }

    public class RespostaTreatFiles
    {

        public string? Erro { get; set; }
        public string? FileName { get; set; }
        public bool TemErro { get; set; } = false;
        public int Index { get; set; } = 0;
    }

}
