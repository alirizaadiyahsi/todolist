using Todo.Core.Database.Tables;
using System;
using System.Linq;
using Todo.Data;
using System.Data.Entity;

namespace Todo.Service.MembershipService
{
    public class UserService : BaseService
    {
        public UserService(TodoContext context)
        {
            _context = context;
        }

        public tblUser FindUserByUserNameAndPassword(string userName, string password)
        {
            return _context.tblUsers
                .FirstOrDefault(x => x.Name == userName && x.Password == password);
        }

        public IQueryable<tblUser> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void InsertUser(tblUser user)
        {
            _context.tblUsers.Add(user);
        }

        public tblUser FindUserById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(tblUser user)
        {
            _context.tblUsers.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(tblUser user)
        {
            _context.tblUsers.Attach(user);
            _context.Entry(user).State = EntityState.Deleted;
        }

        public tblUser FindUserByName(string userName)
        {
            return _context.tblUsers.FirstOrDefault(x => x.Name == userName);
        }

        public bool ValidateName(string name)
        {
            return _context.tblUsers.Any(x => x.Name == name);
        }

        public bool ValidateEmail(string email)
        {
            return _context.tblUsers.Any(x => x.Email == email);
        }
    }
}
