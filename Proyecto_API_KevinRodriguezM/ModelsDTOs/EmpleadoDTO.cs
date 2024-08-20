using Proyecto_API_KevinRodriguezM.Models;

namespace Proyecto_API_KevinRodriguezM.ModelsDTOs
{
    public class EmpleadoDTO
    {
        public int EmployeeId { get; set; }

        public string Name { get; set; } = null!;

        public string Last_name { get; set; } = null!;

        public string Post { get; set; } = null!;

        public int Phone { get; set; }

        public string E_mail { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
