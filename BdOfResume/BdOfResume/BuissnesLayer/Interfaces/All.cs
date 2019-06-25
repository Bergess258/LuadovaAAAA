using BdOfResume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = BdOfResume.Models.Task;

namespace BdOfResume.BuissnesLayer.Interfaces
{
    public interface IEmp
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int Id);
        void SaveChanges(Employee emp);
        void Delete(Employee emp);
    }
    public interface IAutorization
    {
        IEnumerable<Autorization> GetAll();
        Autorization GetById(int Id);
        void SaveChanges(Autorization emp);
        void Delete(Autorization emp);
    }
    public interface IDep
    {
        IEnumerable<Department> GetAll();
        Department GetById(int Id);
        void SaveChanges(Department emp);
        void Delete(Department emp);
    }
    public interface IEdu
    {
        IEnumerable<Education> GetAll();
        Education GetById(int Id);
        void SaveChanges(Education emp);
        void Delete(Education emp);
    }
    public interface IEmpEdu
    {
        IEnumerable<EmployeeEducation> GetAll();
        EmployeeEducation GetById(int Id);
        void SaveChanges(EmployeeEducation emp);
        void Delete(EmployeeEducation emp);
    }
    public interface IEmpSkills
    {
        IEnumerable<EmpSkills> GetAll();
        EmpSkills GetById(int Id);
        void SaveChanges(EmpSkills emp);
        void Delete(EmpSkills emp);
    }
    public interface IEmpTask
    {
        IEnumerable<EmpTask> GetAll();
        EmpTask GetById(int Id);
        void SaveChanges(EmpTask emp);
        void Delete(EmpTask emp);
    }
    public interface IParticipation
    {
        IEnumerable<Participation> GetAll();
        Participation GetById(int Id);
        void SaveChanges(Participation emp);
        void Delete(Participation emp);
    }
    public interface IPost
    {
        IEnumerable<Post> GetAll();
        Post GetById(int Id);
        void SaveChanges(Post emp);
        void Delete(Post emp);
    }
    public interface IProject
    {
        IEnumerable<Project> GetAll();
        Project GetById(int Id);
        void SaveChanges(Project emp);
        void Delete(Project emp);
    }
    public interface IRole
    {
        IEnumerable<Role> GetAll();
        Role GetById(int Id);
        void SaveChanges(Role emp);
        void Delete(Role emp);
    }
    public interface ISkill
    {
        IEnumerable<Skill> GetAll();
        Skill GetById(int Id);
        void SaveChanges(Skill emp);
        void Delete(Skill emp);
    }
    public interface ISpeciality
    {
        IEnumerable<Speciality> GetAll();
        Speciality GetById(int Id);
        void SaveChanges(Speciality emp);
        void Delete(Speciality emp);
    }
    public interface ITask
    {
        IEnumerable<Task> GetAll();
        Task GetById(int Id);
        void SaveChanges(Task emp);
        void Delete(Task emp);
    }
    public interface IUniversity
    {
        IEnumerable<University> GetAll();
        University GetById(int Id);
        void SaveChanges(University emp);
        void Delete(University emp);
    }
}
