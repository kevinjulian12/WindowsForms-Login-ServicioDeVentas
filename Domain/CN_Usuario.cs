using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace Domain
{
    public class CN_Usuario
    {
        UsuarioDAO userDao = new UsuarioDAO();
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
        public CN_Usuario(int idUser, string loginName, string password, string firstName, string lastName, string position, string email)
        {
            this.idUser = idUser;
            this.loginName = loginName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.position = position;
            this.email = email;
        }
        //Es necesario, porque se invoco desde muchos formularios mediante constructor sin parámetros
        public CN_Usuario()
        {
        }
        //Methods
        public string Editar_Usuario()
        {                      
                userDao.Update(idUser, loginName, password, firstName, lastName, email);
                LoginUser(loginName, password);
                return "Tú perfil ha sido actualizado satisfactoriamente";                    
        }
    }
}
