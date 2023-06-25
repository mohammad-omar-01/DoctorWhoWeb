using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWho.DTOs.Models
{
    public class DoctorCreationRequestDTO
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = String.Empty;
        public int DoctorNumber { get; set; }
        public DateTime BirthDate { get; set; }
      
    }
}
