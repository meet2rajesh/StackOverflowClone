using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;


namespace StackOverflowProject.Repositories
{

    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetail(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int uid);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string Email, string Password);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserId(int UserId);
        int GetLatestUserID();

    }

    public class UsersRepository:IUsersRepository
    {
        StackOverflowDatabaseDbContext db;

        public UsersRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetail(User u)
        {
            User us = db.Users.Where(temp=>temp.UserID == u.UserID).FirstOrDefault();
            if( us!= null)
            {
                us.Name = u.Name;
                us.Mobile = u.Mobile;
                db.SaveChanges();

            }

        }

        public void UpdateUserPassword(User u)
        {
            User us = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (us != null)
            {
                us.PasswordHash = u.PasswordHash;
                db.SaveChanges();

            }
        }

        public List<User> GetUsers()
        {
            List<User> us = db.Users.Where(temp=> temp.IsAdmin==false).OrderBy(temp=> temp.Name).ToList();
            return us;
        }

        public List<User> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<User> us = db.Users.Where(temp => temp.Email == Email && temp.PasswordHash==PasswordHash).ToList();
            return us;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> us = db.Users.Where(temp => temp.Email == Email).ToList();
            return us;
        }



        public List<User> GetUsersByUserId(int UserId)
        {
            List<User> us = db.Users.Where(temp => temp.UserID == UserId).ToList();
            return us;
        }


        public int GetLatestUserID()
        {
            int uid = db.Users.Select(temp => temp.UserID).Max();
            return uid;
        }

        public void DeleteUser(int uid)
        {
            User us = db.Users.Where(temp => temp.UserID == uid).FirstOrDefault();
            if (us != null)
            {
               db.Users.Remove(us);
               db.SaveChanges();

            }

        }





    }

}
