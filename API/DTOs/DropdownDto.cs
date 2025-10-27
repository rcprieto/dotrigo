
namespace API.Domain.DTOs
{
    public class DropdownDto
    {
        public string Id { get; set; } = "";
        public string IdFk { get; set; } = "";
        public string Nome { get; set; } = "";
        public string NomeFk { get; set; } = "";
        public bool Selected { get; set; } = false;
    }
}
