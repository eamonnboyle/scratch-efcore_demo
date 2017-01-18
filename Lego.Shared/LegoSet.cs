namespace Lego.Shared {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LegoSet {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(32)]
        public string Code { get; set; }

        public int Year { get; set; }

        public int Pieces { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public Theme Theme1 { get; set; }

        public Theme Theme2 { get; set; }

        public Theme Theme3 { get; set; }

        public LegoSet(string code, int year, string description, Theme theme) {
            Code = code;
            Description = description;
            Year = year;
            Theme1 = theme;
        }

        public LegoSet() {
        }
    }
}