using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(80, MinimumLength =3, ErrorMessage = "O nome deve ter entre 3 e 80 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O slug é obrigatório.")]
        public string Slug { get; set; }
    }
}
