namespace Lego.Shared {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class Theme {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Description { get; set; }

        public Theme(string description) {
            this.Description = description;
        }

        public Theme() {
        }
    }
}