using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScriptureJournal.Models
{
    public class Journal
    {
        public int ID { get; set; }



        [StringLength(25, MinimumLength = 3)]
        [Required]
        public string Book { get; set; }
        [Range(1, 150)]
        [Required]
        public int Chapter { get; set; }
        [Range(1, 150)]
        [Required]
        public int Verse { get; set; }
        [StringLength(254)]
        [Required]
        public string Note { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }


      
    }
}
