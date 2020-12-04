using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;

namespace Domain
{
    public class UserModel
    {
        UserDao userDao = new UserDao();
        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }
        private int idUser;
        private string loginName;
        private string password;
        private string firstName;
        private string lastName;
        private string position;
        private string email;
        //Constructors
        public UserModel(int idUser, string loginName, string password, string firstName, string lastName, string position, string email)
        {
            this.idUser = idUser;
            this.loginName = loginName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.email = email;
        }
        public UserModel()
        {
        }
        //Methods
        public string editUserProfile()
        {
            try
            {
                userDao.editProfile(idUser, loginName, password, firstName, lastName, email);
                LoginUser(loginName, password);
                return "Tú perfil ha sido actualizado satisfactoriamente";
            }
            catch (Exception ex)
            {
                return "El nombre de usuario ya está registrado, pruebe con otro";
            }
        }
    }
}
