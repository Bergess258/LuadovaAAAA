using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BdOfResume.Models
{
    public class BdCount
    {
        public int DepCount { get; set; }
        public int EduCount { get; set; }
        public int PostCount { get; set; }
        public int RoleCount { get; set; }
        public int SkillCount { get; set; }
        public int SpecialityCount { get; set; }
        public int TaskCount { get; set; }
        public int UnivCount { get; set; }
    }
    public interface ICatalog
    {
        string Name { get; set; }
        int Id { get; set; }
    }
    public class SelectedCat
    {
        public IEnumerable<ICatalog> catalog { get; set; }
        public string catalogName { get; set; }
        public SelectedCat(IEnumerable<ICatalog> _catalog,string name)
        {
            catalog = _catalog;
            catalogName = name;
        }
    }
    public class AccInfo
    {
        [Display(Name = @"Имя пользователя\Логин")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        [Display(Name ="Новый пароль")]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать не менее {2} символов.", MinimumLength = 6)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Старый пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string OldPassword { get; set; }

        public Employee emp { get; set; }
    }
}