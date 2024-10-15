using System.ComponentModel.DataAnnotations;

namespace BuisnessLibrary.Dto.SalesInvoice
{
    public class SalesInvoiceAddDto
    {

        [Required]
        public DateTime InvoiceDate { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; } = null!;
        [Required]
        public int CurrentState { get; set; }
        public string? Notes{ get; set; }
    
        [Required]
        public string CustomerId { get; set; }=null!;

    }
}
