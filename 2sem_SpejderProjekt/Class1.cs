namespace _2sem_SpejderProjekt
{
    public class User
    {
        private int _userId;
        private string _name;
        private string _email;
        private string _password;
        private bool _isAdmin;

        public int UserId
        {
            get { return _userId; } 
            set { _userId = value; } 
        }
    }
}
